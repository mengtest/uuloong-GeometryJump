using UnityEngine;
using System.Collections;
using System;
using bysdk;

public class DomobInterstitial : InterstitialAds {
#if UNITY_ANDROID
    private static string AD = AdManager.DOMOB;
#endif
    public override event EventHandler<AdsEventArgs> AdClosed;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdOpened;

    public DomobInterstitial(string appId, string institialId) {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        ad.interstitialEventHandler += onInterstitialEvent;
        ad.init(appId);
        ad.setInstitialId(institialId);
#endif
    }
    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.Interstitial);
    }
    void onInterstitialEvent(string eventName, string msg) {
#if UNITY_ANDROID
        Debug.Log("handler onDomobEvent---" + eventName + "   " + msg);
        if (eventName == DomobEvent.onInterstitialAdReady) {
            if (AdLoaded != null) {
                AdLoaded(this, new AdsEventArgs());
                AdLoaded = null;
            }
        } else if (eventName == DomobEvent.onInterstitialAdDismiss) {
            Load();
            if (AdClosed != null) {
                AdClosed(this, new AdsEventArgs());
                AdClosed = null;
            }
            Collecter.AdsClosed();
            AdClosed = null;
        } else if (eventName == DomobEvent.onInterstitialAdFailed) {
            if (AdFailedToLoad != null) {
                AdFailedToLoad(this, new AdsEventArgs());
                AdFailedToLoad = null;
            }
            Collecter.Failed("load failed");
        } else if (eventName == DomobEvent.onLandingPageOpen) {
            if (AdOpened != null) {
                AdOpened(this, new AdsEventArgs());
                AdOpened = null;
            }
            Collecter.AdsShow();
        } else if( eventName == DomobEvent.onAdClicked ) {
            Collecter.AdsClick();
        }
#endif
    }

    public override void CheckAds() {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        if (!ad.isInterstitialReady()) {
            ad.loadInterstitial();
        }
#endif
    }

    public override void Display() {

#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        if (ad.isInterstitialReady()) {
            ad.showInterstitial();
            IndividualData.UpdateShowCount();
        }
#endif
    }

    public override bool IsReady() {

#if UNITY_ANDROID
        return AdManager.Instance(AD).isInterstitialReady()&&  IndividualData.IsTimeWindOpen();;
#endif
        return false;
    }

    public override void Load() {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        ad.loadInterstitial();
#endif
    }

    public override void ReLoad() {
        Load();
    }
}
