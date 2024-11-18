using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using SimpleJSON;
using System.Linq;

[System.Serializable]
public struct Ranking
{
    public List<PlayerData> playerDatas;
}
public class FireBaseRankingManager : MonoBehaviour
{
    public const string url = "https://jellyfishleaderboard-default-rtdb.asia-southeast1.firebasedatabase.app";
    public const string secret = "bBSUDrAqZphiv5dXwzn2qOtQ0hzJwuUsgggdAeUG";

    [Header("Main")]
    public RankUIManager rankUIManager;
    public Ranking ranking;

    [Header("New Data")]
    public PlayerData currentPlayerData;
    private List<PlayerData> sortPlayerDatas = new List<PlayerData>();

    #region Test
    [Header("Test")]
    public int testNum;

    [System.Serializable]
    public struct TestData
    {
        public int num;
        public string name;
    }

    [System.Serializable]
    public struct TestDataObject
    {
        public int rank;
        public TestData TestData;
    }

    public TestData testData = new TestData();
    public TestDataObject testDataObject = new TestDataObject();

    public void TestSetData()
    {
        string urlData = $"{url}/.json?auth={secret}";

        testData.name = "A";
        testData.num = 26;

        RestClient.Put<TestData>(urlData, testData).Then(response =>
        {
            Debug.Log("Upload Data Complete");
        }).Catch(error =>
        {
            Debug.Log(error.Message);
            Debug.Log("error on set to server");
        });
    }
    public void TestSetData2()
    {
        string urlData = $"{url}/testdata.json?auth={secret}";

        testData.name = "A";
        testData.num = 26;

        RestClient.Put<TestData>(urlData, testData).Then(response =>
        {
            Debug.Log("Upload Data Complete");
        }).Catch(error =>
        {
            Debug.Log(error.Message);
            Debug.Log("error on set to server");
        });
    }
    public void TestSetData3()
    {
        string urlData = $"{url}/testDataObject.json?auth={secret}";

        RestClient.Put<TestDataObject>(urlData, testDataObject).Then(response =>
        {
            Debug.Log("Upload Data Complete");
        }).Catch(error =>
        {
            Debug.Log(error.Message);
            Debug.Log("error on set to server");
        });
    }
    public void TestGetData()
    {
        string urlData = $"{url}/.json?auth={secret}";

        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = JSONNode.Parse(response.Text);
            testNum = jsonNode["num"];
        }).Catch(error =>
        {
            Debug.Log("error");
        });
    }
    public void TestGetData2()
    {
        string urlData = $"{url}/.json?auth={secret}";

        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = JSONNode.Parse(response.Text);
            testNum = jsonNode["testdata"]["num"];
        }).Catch(error =>
        {
            Debug.Log("error");
        });
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //TestSetData();
        //TestSetData2();
        //TestSetData3();
        //TestGetData();
        //TestGetData2();
        ReloadSortingData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CalculateRankFromScore()
    {
        List<PlayerData> sortRankPlayer = new List<PlayerData>();
        sortRankPlayer = ranking.playerDatas.OrderByDescending(data => data.playerScore).ToList();

        for (int i = 0; i < sortRankPlayer.Count; i++)
        {
            PlayerData changeRankNum = sortRankPlayer[i];
            changeRankNum.rankNumber = i + 1;

            sortRankPlayer[i] = changeRankNum;
        }

        ranking.playerDatas = sortRankPlayer;
    }

    public void CalculateRankFromTime()
    {
        List<PlayerData> sortRankPlayer = ranking.playerDatas
            .OrderByDescending(data => data.playerTime)
            .ToList();

        for (int i = 0; i < sortRankPlayer.Count; i++)
        {
            PlayerData changeRankNum = sortRankPlayer[i];
            changeRankNum.rankNumber = i + 1;
            sortRankPlayer[i] = changeRankNum;
        }

        ranking.playerDatas = sortRankPlayer;
    }
    public void FindYourDataInRanking() 
    {
        rankUIManager.youRankData.playerData = ranking.playerDatas
            .FirstOrDefault(data => data.playerName == currentPlayerData.playerName);
        rankUIManager.youRankData.UpdateData();
    }

    [ContextMenu("Set Local Data to Database")]
    public void SetLocalDataToDatabase() 
    {
        string urlData = $"{url}/ranking.json?auth{secret}";
        RestClient.Put<Ranking>(urlData, ranking).Then(response =>
        {
            Debug.Log("Upload Data Complete");
        }).Catch(error =>
        {
            Debug.Log("Error to set local ranking data to database");
        });
    }
    public void ReloadSortingData()
    {
        string urlData = $"{url}/ranking/playerDatas.json?auth={secret}";

        RestClient.Get(urlData).Then(response =>
        {
            JSONNode jsonNode = JSONNode.Parse(response.Text);

            ranking = new Ranking();
            ranking.playerDatas = new List<PlayerData>();

            for (int i = 0; i < jsonNode.Count; i++)
            {
                ranking.playerDatas.Add(new PlayerData(
                    jsonNode[i]["rankNumber"],
                    jsonNode[i]["playerName"],
                    jsonNode[i]["playerScore"],
                    jsonNode[i]["playerTime"]  
                ));
            }
            CalculateRankFromTime();

            string urlPlayerData = $"{url}/ranking/.json?auth={secret}";

            RestClient.Put<Ranking>(urlPlayerData, ranking).Then(response =>
            {
                rankUIManager.playerDatas = ranking.playerDatas;
                rankUIManager.ReloadRankData();
                FindYourDataInRanking();
            }).Catch(error =>
            {
                Debug.Log("error on set to server");
            });
        }).Catch(error =>
        {
            Debug.Log("Error to get data from server");
        });
    }


    public void AddDataWithSorting()
    {
        string urlData = $"{url}/ranking/playerDatas.json?auth={secret}";

        RestClient.Get(urlData).Then(response =>
        {
            Debug.Log(response.Text);
            JSONNode jsonNode = JSONNode.Parse(response.Text);

            ranking = new Ranking();
            ranking.playerDatas = new List<PlayerData>();
            for (int i = 0; i < jsonNode.Count; i++)
            {
                ranking.playerDatas.Add(new PlayerData(
                    jsonNode[i]["rankNumber"],
                    jsonNode[i]["playerName"],
                    jsonNode[i]["playerScore"],
                    jsonNode[i]["playerTime"]
                ));
            }
            PlayerData checkPlayerData = ranking.playerDatas.FirstOrDefault(
                data => data.playerName == currentPlayerData.playerName);
            int indexOfPlayer = ranking.playerDatas.IndexOf(checkPlayerData);

            if (checkPlayerData.playerName != null)
            {
                checkPlayerData.playerScore = currentPlayerData.playerScore;
                checkPlayerData.playerTime = currentPlayerData.playerTime;
                ranking.playerDatas[indexOfPlayer] = checkPlayerData;
            }
            else
            {
                ranking.playerDatas.Add(currentPlayerData);
            }

            CalculateRankFromTime();

            string urlPlayerData = $"{url}/ranking/.json?auth={secret}";
            RestClient.Put<Ranking>(urlPlayerData, ranking).Then(response =>
            {
                Debug.Log("Upload Data Complete");
                rankUIManager.playerDatas = ranking.playerDatas;
                rankUIManager.ReloadRankData();
                FindYourDataInRanking();
            }).Catch(error =>
            {
                Debug.Log("error on set to server");
            });
        }).Catch(error =>
        {
            Debug.Log("Error to get data from server");
        });
    }

}
