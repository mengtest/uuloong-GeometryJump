using UnityEngine;
using System.Collections;
using System;
using AudienceNetwork;

public class FacebookInterstitialAds : InterstitialAds {
    public override event EventHandler<AdsEventArgs> AdClosed;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdOpened;

    private InterstitialAd interstitialAd;
    public FacebookInterstitialAds(MonoBehaviour mono, string placementId) {
        // Create the interstitial unit with a placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        InterstitialAd interstitialAd = new InterstitialAd(placementId);
        this.interstitialAd = interstitialAd;
        this.interstitialAd.Register(mono.gameObject);
        SetCallBackListener();
    }

    ~FacebookInterstitialAds() {
        // Dispose of interstitial ad when the scene is destroyed
        if (this.interstitialAd != null) {
            this.interstitialAd.Dispose();
            this.interstitialAd = null;
        }
        Debug.Log("InterstitialAdTest was destroyed!");
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.Interstitial);
    }

    private void SetCallBackListener() {
        // Set delegates to get notified on changes or when the user interacts with the ad.
        this.interstitialAd.InterstitialAdDidLoad = (delegate () {
            Debug.Log("Interstitial ad loaded.");
            if (AdLoaded != null) {
                AdLoaded(this, new AdsEventArgs());
                AdLoaded = null;
            }
        });
        interstitialAd.InterstitialAdDidFailWithError = (delegate (string error) {
            Debug.Log("Interstitial ad failed to load with error: " + error);
            if (AdFailedToLoad != null) {
                AdFailedToLoad(this, new AdsEventArgs());
                AdFailedToLoad = null;
            }
            Collecter.Failed("load failed");
        });
        interstitialAd.InterstitialAdWillLogImpression = (delegate () {
            Debug.Log("Interstitial ad logged impression.");
        });
        interstitialAd.InterstitialAdDidClick = (delegate () {
            Debug.Log("Interstitial ad clicked.");
            if (AdOpened != null) {
                AdOpened(this, new AdsEventArgs());
                AdOpened = null;
            }
            Collecter.AdsClick();
        });
        interstitialAd.InterstitialAdDidClose = (delegate () {
            Debug.Log("Interstitial ad Closed.");
            if (AdClosed != null) {
                AdClosed(this, new AdsEventArgs());
            }
            AdClosed = null;
            Collecter.AdsClosed();
        });
    }

    public override void CheckAds() {
        this.interstitialAd.LoadAd();
    }

    public override void Display() {
        if (interstitialAd.IsValid()) {
            try {
                IndividualData.UpdateShowCount();
                interstitialAd.Show();
            } catch (Exception e) {
                BYLog.Log(" FaceBook is throw: " + e.Message);
            }

        }
    }

    /// <summary>
    ///  添加时间窗条件
    /// </summary>
    /// <returns></returns>
    public override bool IsReady() {
        return interstitialAd.IsValid() && IndividualData.IsTimeWindOpen();
    }

    public override void Load() {
        this.interstitialAd.LoadAd();
    }

    public override void ReLoad() {
        this.interstitialAd.LoadAd();
    }
}
