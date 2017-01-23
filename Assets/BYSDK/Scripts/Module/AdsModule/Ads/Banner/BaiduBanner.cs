using UnityEngine;
using System.Collections;
using System;
using bysdk;

public class BaiduBanner : BannerAds {
#if UNITY_ANDROID
    private static string AD = AdManager.BAIDU;
#endif
    public override event EventHandler<AdsEventArgs> AdClosed;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdOpened;
    bool _isDestory = false;

    public BaiduBanner(string appId, string bannerId) {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        ad.bannerEventHandler += onBannerEvent;
        ad.init(appId);
        ad.setBannerId(bannerId);
        ad.createBanner();
#endif
    }

    ~BaiduBanner() {
#if UNITY_ANDROID
        AdManager.Instance(AD).removeBanner();
#endif
    }
    public override void SetIndividualData(long _TimeWind, int count) {
        IndividualData = new AdsIndividualData(_TimeWind, count);
        Collecter = new DataCollecter(MType, SDKAdsType.Banner);
    }
    void onBannerEvent(string eventName, string msg) {
        Debug.Log("handler onBaiduBannerEvent---" + eventName + "   " + msg);
        if (eventName == BaiduEvent.onAdReady) {
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
        if( !_isDestory) {
            Hide();
        }
    }

}
