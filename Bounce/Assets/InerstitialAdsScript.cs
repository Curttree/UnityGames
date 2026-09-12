using UnityEngine;
using UnityEngine.Advertisements;

public class InerstitialAdsScript : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string gameId = "3914607"; 
    [SerializeField] 
    private string interstitialAdUnitId = "video";
    private bool testMode = false;

    void Awake()
    {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void ShowInterstitialAd()
    {
        if (!GameController.instance.IsPaidUser() && GameController.instance.GetPrevScore() > 25)
        {
            Advertisement.Show(interstitialAdUnitId, this);
        }
    }
    public void OnInitializationComplete()
    {
        // Preload the first ad as soon as initialization succeeds
        LoadInterstitialAd();
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadInterstitialAd()
    {
        Advertisement.Load(interstitialAdUnitId, this);
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        // Pause game logic and audio here
        Time.timeScale = 0f;
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad closed. Resuming game.");
        // Resume game logic and audio here
        Time.timeScale = 1f;
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded Successfully: " + placementId);
        ShowInterstitialAd();
    }
}
