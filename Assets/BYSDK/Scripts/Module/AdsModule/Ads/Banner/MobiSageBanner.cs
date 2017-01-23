using UnityEngine;
using System.Collections;
using bysdk;
using System;

public class MobiSageBanner : BannerAds {
#if UNITY_ANDROID
    private static string AD = AdManager.MOBISAGE;
#endif
    public override event EventHandler<AdsEventArgs> AdClosed;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdOpened;
    bool _isDestory = false;

    public MobiSageBanner(String appId, String bannerId) {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        ad.bannerEventHandler += onBannerEvent;
        ad.init(appId);
        ad.setBannerId(bannerId);
        ad.createBanner();
#endif
    }

    ~MobiSageBanner() {
#if UNITY_ANDROID
        AdManager.Instance(AD).removeBanner();
#endif
    }
    public override void SetIndividualData(long _TimeWind, int count) {
        IndividualData = new AdsIndividualData(_TimeWind, count);
    }
    void onBannerEvent(string eventName, string msg) {
        Debug.Log("handler onMobiSageBannerEvent---" + eventName + "   " + msg);
        if (eventName == MobiSageEvent.onMobiSageBannerSuccess) {
            if (AdLoaded != null) {
                AdLoaded(this, new AdsEventArgs());
            }
        }
    }

    public override void CheckAds() {
#if UNITY_ANDROID
        AdManager.Instance(AD).showBanner();
#endif
    }

    public override void LoadAndDisplay() {

#if UNITY_ANDROID
        IndividualData.UpdateShowCount();
        AdManager.Instance(AD).showBanner();
#endif
    }

    public override void Hide() {
#if UNITY_ANDROID
        AdManager.Instance(AD).removeBanner();
        _isDestory = true;
#endif
    }

    public override void Destroy() {
        if (!_isDestory) {
            Hide();
        }
    }
}