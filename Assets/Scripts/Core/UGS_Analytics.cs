using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class UGS_Analytics : MonoBehaviour
{
    async void Start()
    {
#pragma warning disable CS0168 // ѕеременна€ объ€влена, но не используетс€
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent(); //Get user consent according to various legislations
        }
        catch (ConsentCheckException e)
        {
            //Debug.Log(e.ToString());
        }
#pragma warning restore CS0168 // ѕеременна€ объ€влена, но не используетс€
    }

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }
}
