using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite jumpPrite;
    public Sprite dieSprite;
    public float gravity = 9.8f;
    public float strength = 5f;
    public AudioSource jumpAudio;
    public AudioSource landAudio;
    public AudioSource dieAudio;


    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;
    private bool jumping = false;
    private int jumpSlots = 2;
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
        bool touched = false;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && jumpSlots != 0)
        {
            Jump();
            touched = true;
        }

        if (Input.touchCount > 0 && jumpSlots != 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Jump();
                touched = true;
            }
        }
        if (jumping && !touched)
        {
            Falling();
        }
    }

    private void Falling()
    {
        direction += Vector3.down * gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void Jump()
    {
        direction += strength * Vector3.up;
        jumping = true;
        jumpSlots--;
        jumpAudio.Play();
        landAudio.Pause();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            landAudio.Play();
            jumping = false;
            jumpSlots = 2;
            direction = Vector3.zero;
        }

        if (collision.CompareTag("Cactus"))
        {
            FindObjectOfType<GameManager>().GameOver();
            spriteRenderer.sprite = dieSprite;
            landAudio.Pause();
            dieAudio.Play();
        }

        if (collision.CompareTag("Scored"))
        {
            FindAnyObjectByType<GameManager>().IncreaseScore();
        }
    }
}
