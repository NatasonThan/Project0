using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct PlayerData
{
    public string playerName;
    public int rankNumber;
    public int playerScore;
    public int playerTime;

    public PlayerData(int rankNumber, string playerName, int playerScore, int playerTime)
    {
        this.rankNumber = rankNumber;
        this.playerName = playerName;
        this.playerScore = playerScore;
        this.playerTime = playerTime;
    }
}

public class RankData : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    public void UpdateData()
    {
        rankText.text = playerData.rankNumber.ToString();
        playerText.text = playerData.playerName;
        scoreText.text = playerData.playerScore.ToString("0");

        int minutes = playerData.playerTime / 60;
        int seconds = playerData.playerTime % 60;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

