using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class UGS_Analytics : MonoBehaviour
{
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void GiveConsent()
    {
        AnalyticsService.Instance.StartDataCollection();
        //Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }
}
