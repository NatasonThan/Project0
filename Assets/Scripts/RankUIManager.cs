﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RankUIManager : MonoBehaviour
{
    public GameObject rankDataPrefab;
    public Transform rankPanel;

    public List<PlayerData> playerDatas = new List<PlayerData>();
    public List<GameObject> createdPlayerDatas = new List<GameObject>();
    public RankData youRankData;

    void Start()
    {
        CreateRankData();
    }

    public void CreateRankData()
    {
        for (int i = 0; i < playerDatas.Count; i++)
        {
            GameObject rankObj = Instantiate(rankDataPrefab, rankPanel);
            RankData rankData = rankObj.GetComponent<RankData>();
            rankData.playerData = new PlayerData();
            rankData.playerData.playerName = playerDatas[i].playerName;
            rankData.playerData.rankNumber = playerDatas[i].rankNumber;
            rankData.playerData.playerScore = playerDatas[i].playerScore;
            rankData.playerData.playerTime = playerDatas[i].playerTime;
            rankData.UpdateData();
            createdPlayerDatas.Add(rankObj);
        }
    }

    public void ClearRankData()
    {
        foreach (GameObject createdData in createdPlayerDatas)
        {
            Destroy(createdData);
        }
        createdPlayerDatas.Clear();
    }

    public void SortRankData()
    {
        List<PlayerData> sortRankPlayer = playerDatas.OrderByDescending(data => data.playerScore).ToList();
        for (int i = 0; i < sortRankPlayer.Count; i++)
        {
            PlayerData changeRankNum = sortRankPlayer[i];
            changeRankNum.rankNumber = i + 1;
            sortRankPlayer[i] = changeRankNum;
        }
        playerDatas = sortRankPlayer;
    }

    public void SortTimeRankData()
    {
        List<PlayerData> sortRankPlayer = playerDatas
            .OrderByDescending(data => data.playerTime)
            .ToList();

        for (int i = 0; i < sortRankPlayer.Count; i++)
        {
            PlayerData changeRankNum = sortRankPlayer[i];
            changeRankNum.rankNumber = i + 1;
            sortRankPlayer[i] = changeRankNum;
        }
        playerDatas = sortRankPlayer;
    }

    public void BubbleSortScoreRankData()
    {
        for (int i = 0; i < playerDatas.Count - 1; i++)
        {
            for (int j = 0; j < playerDatas.Count - 1 - i; j++)
            {
                if (playerDatas[j].playerScore < playerDatas[j + 1].playerScore)
                {
                    PlayerData temp = playerDatas[j];
                    playerDatas[j] = playerDatas[j + 1];
                    playerDatas[j + 1] = temp;
                }
            }
        }

        for (int i = 0; i < playerDatas.Count; i++)
        {

            PlayerData changeRankNum = playerDatas[i];
            changeRankNum.rankNumber = i + 1; 
            playerDatas[i] = changeRankNum;
        }
    }

    public void BubbleSortTimeRankData()
    {

        for (int i = 0; i < playerDatas.Count - 1; i++)
        {
            for (int j = 0; j < playerDatas.Count - 1 - i; j++)
            {

                if (playerDatas[j].playerTime < playerDatas[j + 1].playerTime)
                {

                    PlayerData temp = playerDatas[j];
                    playerDatas[j] = playerDatas[j + 1];
                    playerDatas[j + 1] = temp;
                }
            }
        }

        for (int i = 0; i < playerDatas.Count; i++)
        {

            PlayerData changeRankNum = playerDatas[i];
            changeRankNum.rankNumber = i + 1;
            playerDatas[i] = changeRankNum;
        }
    }

    [ContextMenu("Reload")]
    public void ReloadRankData()
    {
        ClearRankData();
        CreateRankData();
    }

    public void SetPlayerName(string playerName)
    {
        PlayerData newPlayer = new PlayerData(playerDatas.Count + 1, playerName, 0, 0);
        playerDatas.Add(newPlayer);
        ReloadRankData();
    }
}
