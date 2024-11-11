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
    public GameObject nameSystem;
    public TextMeshProUGUI requiredText;

    private Timer timer;
    public int score;
    [SerializeField] private int highestScore;
    private int highestTime;
    private int deathTime = 0;
    private int reviveScore = 0;

    public FireBaseRankingManager fireBaseRankingManager;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        Application.targetFrameRate = 60;

        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
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
        player.transform.localScale = new Vector3(3, 3, 3);
        deathTime++;

        reviveScore = deathTime * 10;
        requiredText.text = $"Need Score {reviveScore} to Revive";
        Debug.Log($"You Need Score: {reviveScore}");

        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("HighestScore", highestScore);
            PlayerPrefs.Save();

            SubmitScoreToLeaderboard(timer.ElapsedTime);
        }
        else
        {
            Debug.Log("Score is not high enough to submit to the leaderboard.");
        }

        Pause();
    }

    private void SubmitScoreToLeaderboard(int time)
    {
        if (fireBaseRankingManager != null)
        {
            string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
            PlayerData newPlayerData = new PlayerData(0, playerName, highestScore, time);
            fireBaseRankingManager.currentPlayerData = newPlayerData;
            fireBaseRankingManager.AddDataWithSorting();
        }
        else
        {
            Debug.LogError("FireBaseRankingManager is not set in the GameManager");
        }
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

    public void RemoveScore(int amount)
    {
        score -= amount;
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
            gamePaused.SetActive(true);

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
