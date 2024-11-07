using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject gamePaused;
    public GameObject reviveButton;
    public TextMeshProUGUI requiredText;
    private Timer timer;
    public int score;
    private int deathTime = 0;
    private int reviveScore = 0;

    public int Score
    {
        get { return score; }
    }


    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        Application.targetFrameRate = 60;

        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        reviveButton.SetActive(false);
        timer.ResetTimer();

        deathTime = 0;

        Time.timeScale = 1f;
        player.enabled = true;

        Velocity[] pipes = FindObjectsOfType<Velocity>();

        for (int i = 0; i < pipes.Length; i++) 
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
        gamePaused.SetActive(false);
        reviveButton.SetActive(true);
        player.transform.localScale = new Vector3 (3, 3, 3);
        deathTime++;

        reviveScore = deathTime * 10;
        requiredText.text = $"Need Score {reviveScore} to Revive";
        Debug.Log($"You Need Score: {reviveScore}");

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void Revive() 
    {
        if (score >= reviveScore)
        {
            Time.timeScale = 1f;
            player.enabled = true;
            score -= reviveScore;
            scoreText.text = score.ToString();

            playButton.SetActive(false);
            gameOver.SetActive(false);
            reviveButton.SetActive(false);

            Velocity[] pipes = FindObjectsOfType<Velocity>();

            for (int i = 0; i < pipes.Length; i++)
            {
                Destroy(pipes[i].gameObject);
            }
        }
        else 
        {
            requiredText.text = $"Your Score Is Not Enough";
            Debug.Log("Your Score Is Not Enough");
        }
    }

}
