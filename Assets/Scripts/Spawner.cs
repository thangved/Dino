using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 5f;
    public GameObject[] prefabs;
    public float speed;
    public float maxHeight = 1f;
    public float minHeight = -1f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), 0, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        if (prefabs.Length == 0)
            return;
        int spawnIndex = Random.Range(0, prefabs.Length) % prefabs.Length;
        Instantiate(prefabs[spawnIndex], RandomPosition(), Quaternion.identity).GetComponent<SpawnChild>().speed = speed;
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(transform.position.x, Random.Range(minHeight, maxHeight) + transform.position.y, transform.position.z);
    }

}
