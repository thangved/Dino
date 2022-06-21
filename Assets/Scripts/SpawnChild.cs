using UnityEngine;

public class SpawnChild : MonoBehaviour
{
    public float speed = .7f;

    private float leftEdge;
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge - 10)
        {
            Destroy(gameObject);
        }
    }
}
