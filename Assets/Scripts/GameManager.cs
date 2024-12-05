using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player[] player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject gamePaused;
    public GameObject reviveButton;
    public GameObject screenShotButton;
    public GameObject adsRivive;
    public GameObject capturePanel;
    public TextMeshProUGUI requiredText;

    private Timer timer;
    public int score;
    [SerializeField] private int highestScore;
    [SerializeField] private int scoreTime;
    private int deathTime = 0;
    private int reviveScore = 0;
    public bool isAdsRivive = false;

    public FireBaseRankingManager fireBaseRankingManager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        timer = FindObjectOfType<Timer>();
        Application.targetFrameRate = 60;

        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        scoreTime = (int)PlayerPrefs.GetFloat("DataTime", 0f);
        Pause();
    }

    public void Play()
    {
        audioManager.PlayBG();
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        reviveButton.SetActive(false);
        screenShotButton.SetActive(false);
        adsRivive.SetActive(false);
        timer.ResetTimer();
        capturePanel.SetActive(false);
        isAdsRivive = false;

        deathTime = 0;

        Continue();

        Velocity[] pipes = FindObjectsOfType<Velocity>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        audioManager.PlaySFX(audioManager.gameOver);
        audioManager.PauseBG();
        playButton.SetActive(true);
        gameOver.SetActive(true);
        gamePaused.SetActive(false);
        reviveButton.SetActive(true);
        screenShotButton.SetActive(true);
        adsRivive.SetActive(true);
        isAdsRivive = false;
        deathTime++;

        reviveScore = deathTime * 10;
        requiredText.text = $"Need Score {reviveScore} to Revive\n If you use Ads Revive, you'll still lose points anyway";
        Debug.Log($"You Need Score: {reviveScore}");

        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("HighestScore", highestScore);
            PlayerPrefs.Save();

            scoreTime = timer.ElapsedTime;
            PlayerPrefs.SetFloat("DataTime", scoreTime);
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
    public void FindYourRank()
    {
        Debug.Log($"DataTime: {scoreTime}");
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        PlayerData newPlayerData = new PlayerData(0, playerName, highestScore, scoreTime);
        fireBaseRankingManager.currentPlayerData = newPlayerData;
        fireBaseRankingManager.AddDataWithSorting();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        foreach (Player p in player)
        {
            p.enabled = false;
        }
        audioManager.PauseBG();
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
        audioManager.PlaySFX(audioManager.eat);
    }

    public void RemoveScore(int amount)
    {
        score -= amount;
        scoreText.text = score.ToString();
    }

    public void Revive()
    {
        if (score >= reviveScore || isAdsRivive)
        {
            Continue();
            score -= reviveScore;
            scoreText.text = score.ToString();

            playButton.SetActive(false);
            gameOver.SetActive(false);
            reviveButton.SetActive(false);
            gamePaused.SetActive(true);
            screenShotButton.SetActive(false);
            adsRivive.SetActive(false);

            Velocity[] pipes = FindObjectsOfType<Velocity>();

            for (int i = 0; i < pipes.Length; i++)
            {
                Destroy(pipes[i].gameObject);
            }
        }
        else
        {
            requiredText.text = $"Your Score Is Not Enough";
        }
    }
    public void ResetScore()
    {
        highestScore = 0;
        PlayerPrefs.SetInt("HighestScore", highestScore);
        PlayerPrefs.Save();
        scoreTime = 0;
        PlayerPrefs.SetFloat("DataTime", scoreTime);
        PlayerPrefs.Save();
        Debug.Log("Reseto");
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        foreach (Player p in player)
        {
            p.enabled = true;
        }
        audioManager.PlayBG();
    }
}
