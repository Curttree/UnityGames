using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;

public class InerstitialAdsScript : MonoBehaviour
{
    string gameId = "3914607";
    bool testMode = true;

    void Start()
    {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
        ShowInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady() && GameController.instance.GetPrevScore() > 5 )
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void ShowAd()
    {
        StartCoroutine(ShowAdWhenReady());
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
        {
            yield return new WaitForSeconds(0.25f);
        }
        Advertisement.Show();
    }
}
