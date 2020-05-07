using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public BannerView bannerView;

    // Start is called before the first frame update
    void Start()
    {
        string appId = "ca-app-pub-5349251188309902~8857854415";
        MobileAds.Initialize(appId);
        RequestBanner();
    }

    public void RequestBanner()
    {
<<<<<<< HEAD
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-5349251188309902/8812881098";
        #elif UNITY_IPHONE
                    string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                    string adUnitId = "unexpected_platform";
        #endif
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);// Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            //.AddTestDevice("06D593F35C54216A")
            .Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        //bannerView.Hide();
    }
=======
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif
        int h = 0;
        int bannerHeight = 320; //Because AdSize.Banner measures 320x50
        int w = Screen.currentResolution.width / 15;
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);// Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice("06D593F35C54216A")
            .Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        bannerView.Hide();
    }

    //public void OnDisable()
    //{
    //    bannerView.Destroy();
    //}

    //public void OnDestroy()
    //{
    //    bannerView.Destroy();
    //}

>>>>>>> f1cf2f85df54b696c8f046b3bee6d6fcb970156b
}
