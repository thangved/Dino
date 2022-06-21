using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 1f;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        meshRenderer.material.mainTextureOffset -= Vector2.left * speed * Time.deltaTime;
    }
}
