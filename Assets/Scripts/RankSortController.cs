using UnityEngine;
using UnityEngine.UI;

public class RankSortController : MonoBehaviour
{
    public FireBaseRankingManager fireBaseRankingManager;
    public RankUIManager rankUIManager;
    public RankData rankData;
    public GameObject sortByScoreButton;
    public GameObject sortByTimeButton;

    public void SortByScore()
    {
        sortByScoreButton.SetActive(false);
        sortByTimeButton.SetActive(true);
        fireBaseRankingManager.CalculateRankFromScore();
        fireBaseRankingManager.SetLocalDataToDatabase();
        rankUIManager.ClearRankData();
        rankUIManager.SortRankData();
        rankUIManager.CreateRankData();
    }   

    public void SortByTime()
    {
        sortByScoreButton.SetActive(true);
        sortByTimeButton.SetActive(false);
        fireBaseRankingManager.CalculateRankFromTime();
        fireBaseRankingManager.SetLocalDataToDatabase();
        rankUIManager.ClearRankData();
        rankUIManager.SortTimeRankData();
        rankUIManager.CreateRankData();
    }
}
