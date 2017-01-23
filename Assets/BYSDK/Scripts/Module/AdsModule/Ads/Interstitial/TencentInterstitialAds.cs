using UnityEngine;
using System.Collections;
using System;
using bysdk;

public class TencentInterstitialAds : InterstitialAds {
#if UNITY_ANDROID
    private static string AD = AdManager.TENCENT;
#endif
    public override event EventHandler<AdsEventArgs> AdClosed;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdOpened;


    private string appId;
    private string institialId;

    public TencentInterstitialAds(string appId,
                                  string institialId) {
        this.appId = appId;
        this.institialId = institialId;
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        ad.interstitialEventHandler += onInterstitialEvent;
        ad.init(appId);
        ad.setInstitialId(institialId);
#elif UNITY_IPHONE
        TecentGDTAds.noop();
        RegistHandle();
#endif
    }

    ~TencentInterstitialAds() {
#if UNITY_IPHONE
        UnRegistHandle();
#endif
    }

#if UNITY_ANDROID
    void onInterstitialEvent(string eventName, string msg) {

        Debug.Log("handler onTecentEvent begin---" + eventName + "   " + msg);
        if (eventName == TecentEvent.onADClosed) {
            if (AdClosed != null) {
                AdClosed(this, new AdsEventArgs());
            }
            AdClosed = null;
            Collecter.AdsClosed();
        } else if( eventName == TecentEvent.onADClicked) {
            Collecter.AdsClick();
        } else if( eventName == TecentEvent.onADOpened ) {
            if(AdOpened != null ) {
                AdOpened(this, new AdsEventArgs());
            }
            AdOpened = null;
            Collecter.AdsShow();
        }
    }
#elif UNITY_IPHONE
    void OnInterstitialClosed(object sender, EventArgs args) {
        if (AdClosed != null) {
            AdClosed(this, new AdsEventArgs());
        }
        AdClosed = null;
        Collecter.AdsClosed();
    }

    void OnInterstitialClick(object sender,EventArgs args) {
        Collecter.AdsClick();
    }

    void OnInterstitialShow(object sender,EventArgs args) {
        Collecter.AdsShow();
    }

    void OnInterstitialLoadFailed(object sender,EventArgs args) {
        Collecter.Failed("load failed");
    }

#endif

#if UNITY_IPHONE
    void RegistHandle() {
        TecentGDTAds.onInterstitialDidDismissScreen += OnInterstitialClosed;
        TecentGDTAds.onInterstitialClicked += OnInterstitialClick;
        TecentGDTAds.onInterstitialDidPresentScreen += OnInterstitialShow;
        TecentGDTAds.onInterstitialFailToLoadAd += OnInterstitialLoadFailed;
    }

    void UnRegistHandle() {
        TecentGDTAds.onInterstitialDidDismissScreen -= OnInterstitialClosed;
        TecentGDTAds.onInterstitialDidDismissScreen -= OnInterstitialClosed;
        TecentGDTAds.onInterstitialClicked -= OnInterstitialClick;
        TecentGDTAds.onInterstitialDidPresentScreen -= OnInterstitialShow;
        TecentGDTAds.onInterstitialFailToLoadAd -= OnInterstitialLoadFailed;
    }
#endif

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count, tag, isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.Interstitial);
    }
    public override void CheckAds() {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        if (!ad.isInterstitialReady()) {
            ad.loadInterstitial();
        }
#elif UNITY_IPHONE
        if (!TecentGDTAds.isInsertAdIsReady()) {
            TecentGDTAds.loadInsertAd(appId, institialId);
        }
#endif
    }

    public override void Display() {

#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        if (ad.isInterstitialReady()) {
            IndividualData.UpdateShowCount();
            ad.showInterstitial();
        }
#elif UNITY_IPHONE
        if (TecentGDTAds.isInsertAdIsReady()) {
            IndividualData.UpdateShowCount();
            TecentGDTAds.showInsertAd();
        }
#endif
    }

    public override bool IsReady() {

#if UNITY_ANDROID
        return AdManager.Instance(AD).isInterstitialReady()&&  IndividualData.IsTimeWindOpen();
#elif UNITY_IPHONE
        return TecentGDTAds.isInsertAdIsReady() && IndividualData.IsTimeWindOpen();
#endif
        return false;
    }

    public override void Load() {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        ad.loadInterstitial();
#elif UNITY_IPHONE
        TecentGDTAds.loadInsertAd(appId, institialId);
#endif
    }

    public override void ReLoad() {
        Load();
    }
}
