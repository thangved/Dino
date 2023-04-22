using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject replayButton;
    public Player player;
    public Spawner spawner;
    public AudioSource theme;
    public AudioSource button;

    private void Start()
    {
        gameOver.SetActive(false);
        theme.loop = true;
        theme.Play();
        spawner.enabled = false;
    }
    public void Play()
    {
        Time.timeScale = 1f;
        replayButton.SetActive(false);
        gameOver.SetActive(false);
        player.enabled = true;
        spawner.enabled = true;
        button.Play();

        SpawnChild[] cactus = FindObjectsOfType<SpawnChild>();

        for (int i = 0; i < cactus.Length; i++)
        {
            if (cactus[i].CompareTag("Cactus"))
                Destroy(cactus[i].gameObject);
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        replayButton.SetActive(true);
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        Pause();
    }
}
