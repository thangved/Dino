using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject replayButton;
    public Player player;
    public Spawner spawner;
    private void Start()
    {
        gameOver.SetActive(false);
        Pause();
    }
    public void Play()
    {
        Time.timeScale = 1f;
        replayButton.SetActive(false);
        gameOver.SetActive(false);
        player.enabled = true;

        SpawnChild[] catus = FindObjectsOfType<SpawnChild>();

        for (int i = 0; i < catus.Length; i++)
        {
            if (catus[i].CompareTag("Cactus"))
                Destroy(catus[i].gameObject);
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        replayButton.SetActive(true);
        player.enabled = false;
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        Pause();
    }
}
