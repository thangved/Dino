using UnityEngine;

public class Player: MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite jumpPrite;
    public Sprite dieSprite;
    public float gravity = -9.8f;
    public float strength = 5f;
    public AudioSource jumpAudio;
    public AudioSource landAudio;
    public AudioSource dieAudio;


    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;
    private bool jumping = false;
    private Vector3 direction;

    private void OnEnable()
    {
        if (!landAudio)
            return;
        landAudio.Play();
        landAudio.loop = true;
    }
    private void OnDisable()
    {
        if (!landAudio)
            return;
        landAudio.Pause();
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = Vector3.zero;
    }
    private void Awake()
    {
        InvokeRepeating(nameof(AnimateSprite), .15f, .15f);
    }

    private void AnimateSprite()
    {
        if (jumping)
        {
            spriteRenderer.sprite = jumpPrite;
            return;
        }
        spriteIndex++;
        spriteIndex %= sprites.Length;

        spriteRenderer.sprite = sprites[spriteIndex];
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !jumping)
        {
            direction += strength * Vector3.up;
            jumping = true;
            jumpAudio.Play();
            landAudio.Pause();
        }

        if (Input.touchCount > 0 && !jumping)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                direction += strength * Vector3.up;
                jumping = true;
                jumpAudio.Play();
                landAudio.Pause();
            }
        }
        if (jumping)
        {
            direction += Vector3.up * gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            landAudio.Play();
            jumping = false;
            direction = Vector3.zero;
        }

        if (collision.CompareTag("Cactus"))
        {
            FindObjectOfType<GameManager>().GameOver();
            spriteRenderer.sprite = dieSprite;
            landAudio.Pause();
            dieAudio.Play();
        }
    }
}
