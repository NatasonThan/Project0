using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
public class Analytics : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            OnbuyPromotion("Staff of Nuule", 10);
        }*/
    }

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    private void OnbuyPromotion(string promotionName, int amount)
    {
        CustomEvent exampleEvent = new CustomEvent("BuyItems")
        {
            { "itemName",promotionName},{ "itemCount",amount},
        };
        AnalyticsService.Instance.RecordEvent(exampleEvent);
        Debug.Log($"Recording Event BuyPromotion {promotionName}");
    }

    private void AnalyticsClick(int amount)
    {
        CustomEvent exampleEvent = new CustomEvent("PlayerClickPlay")
        {
            { "Clicked", amount}
        };
        AnalyticsService.Instance.RecordEvent(exampleEvent);
        Debug.Log($"Recording Event PlayerClickPlay {amount}");
    }

    private void AnalyticsBuyingPack(int amount)
    {
        CustomEvent exampleEvent = new CustomEvent("BuyItems")
        {
            { "playerBuy", amount}
        };
        AnalyticsService.Instance.RecordEvent(exampleEvent);
        Debug.Log($"Recording Event buying {amount}");
    }

    private void AnalyticsPlayerRevive(int amount)
    {
        CustomEvent exampleEvent = new CustomEvent("PlayerRevive")
        {
            { "reviveAmount", amount}
        };
        AnalyticsService.Instance.RecordEvent(exampleEvent);
        Debug.Log($"Recording Event Revive {amount}");
    }

    public void AddClickedToAnalytics() 
    {
        AnalyticsClick(1);
    }
    public void AddPlayerBuyPack() 
    {
        AnalyticsBuyingPack(1);
    }
    public void AddPlayerReviveAds() 
    {
        AnalyticsPlayerRevive(1);
    }
}
