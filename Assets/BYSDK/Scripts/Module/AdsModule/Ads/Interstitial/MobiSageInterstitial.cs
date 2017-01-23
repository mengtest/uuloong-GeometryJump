using UnityEngine;
using System.Collections;
using bysdk;
using System;

public class MobiSageInterstitial : InterstitialAds {
#if UNITY_ANDROID
    private static string AD = AdManager.MOBISAGE;
#endif
    public override event EventHandler<AdsEventArgs> AdClosed;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdOpened;

    public MobiSageInterstitial(string appId,
                                string institialId) {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        ad.interstitialEventHandler += onInterstitialEvent;
        ad.init(appId);
        ad.setInstitialId(institialId);
#endif
    }

    void onInterstitialEvent(string eventName, string msg) {
#if UNITY_ANDROID
        Debug.Log("handler onMobiSageEvent begin---" + eventName + "   " + msg);
        if (eventName == MobiSageEvent.onMobiSagePosterClose) {
            if (AdClosed != null) {
                AdClosed(this, new AdsEventArgs());
            }
            AdLoaded = null;
        }
#endif
    }
    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
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
            IndividualData.UpdateShowCount();
            ad.showInterstitial();
        }
#endif
    }

    public override bool IsReady() {

#if UNITY_ANDROID
        return AdManager.Instance(AD).isInterstitialReady()&& IndividualData.IsTimeWindOpen();
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