using UnityEngine;
using System.Collections;
using System;
using AudienceNetwork;
using AudienceNetwork.Utility;

public class FacebookBanner : BannerAds {
    public override event EventHandler<AdsEventArgs> AdClosed;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdOpened;

    private AdView adView;
    private double bottomHeight;
    private bool isShow;

    public FacebookBanner(MonoBehaviour mono, string placementId) {
        // Create a banner's ad view with a unique placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        adView = new AdView(placementId, AdSize.BANNER_HEIGHT_50);
        this.adView.Register(mono.gameObject);
        SetCallBackListener();
        double height = AdUtility.convert(Screen.height);
        bottomHeight = height - AdView.HeightFromType(AdSize.BANNER_HEIGHT_50);
    }

    ~FacebookBanner() {
        // Dispose of banner ad when the scene is destroyed
        if (this.adView) {
            this.adView.Dispose();
            this.adView = null;
        }
        isShow = false;
        Debug.Log("AdViewTest was destroyed!");
    }
    public override void SetIndividualData(long _TimeWind, int count) {
        IndividualData = new AdsIndividualData(_TimeWind, count);
        Collecter = new DataCollecter(MType, SDKAdsType.Banner);
    }
    private void SetCallBackListener() {
        // Set delegates to get notified on changes or when the user interacts with the ad.
        this.adView.AdViewDidLoad = (delegate () {
            Debug.Log("Facebook Banner view loaded.");
            if (isShow) {
                this.adView.Show(bottomHeight);
            }
            Collecter.AdsShow();
            if (AdLoaded != null) {
                AdLoaded(this, new AdsEventArgs());
            }
        });
        adView.AdViewDidFailWithError = (delegate (string error) {
            Debug.Log("Facebook Banner failed to load with error: " + error);
            if (AdFailedToLoad != null) {
                AdFailedToLoad(this, new AdsEventArgs());
            }
            Collecter.Failed(error);
        });
        adView.AdViewWillLogImpression = (delegate () {
            Debug.Log("Facebook Banner logged impression.");
        });
        adView.AdViewDidClick = (delegate () {
            Debug.Log("Facebook Banner clicked.");
            if (AdOpened != null) {
                AdOpened(this, new AdsEventArgs());
            }
            Collecter.AdsClick();
        });
        adView.adViewDidFinishClick = (delegate () {
            Debug.Log("Facebook Banner Finish.");
            if (AdClosed != null) {
                AdClosed(this, new AdsEventArgs());
            }
            Collecter.AdsClosed();
        });
    }


    public override void CheckAds() {
        adView.LoadAd();
    }

    public override void LoadAndDisplay() {
        isShow = true;
        IndividualData.UpdateShowCount();
        adView.LoadAd();
    }

    public override void Hide() {
        BYLog.Log("Hide Facebook banner");
        isShow = false;
        adView.Dispose();
        adView = null;
    }
    public override void Destroy() {
        if (null != adView) {
            Hide();
        }
    }

}
