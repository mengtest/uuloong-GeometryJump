using UnityEngine;
using System.Collections;
using System;
using bysdk;
using AudienceNetwork.Utility;
using AudienceNetwork;

public class TencentBanner : BannerAds {
#if UNITY_ANDROID
    private static string AD = AdManager.TENCENT;
#endif
    public override event EventHandler<AdsEventArgs> AdClosed;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdOpened;
    bool _isDestory = false;

    public TencentBanner(String appId, String bannerId) {
#if UNITY_ANDROID
        AdManager ad = AdManager.Instance(AD);
        ad.bannerEventHandler += onBannerEvent;
        ad.init(appId);
        ad.setBannerId(bannerId);
        ad.createBanner();
#elif UNITY_IPHONE
        TecentGDTAds.noop();
        RegistHandle();
        int bottomHeight = (int)AdUtility.convert(Screen.height) - 50;
        TecentGDTAds.createBanner(0, bottomHeight, 320, 50, appId, bannerId);
#endif
    }

    ~TencentBanner() {
#if UNITY_ANDROID
        AdManager.Instance(AD).removeBanner();
#elif UNITY_IPHONE
        UnRegistHandle();
#endif
    }

#if UNITY_IPHONE
    void RegistHandle() {
        TecentGDTAds.onBannerDidReceived += OnBannerLoad;
        TecentGDTAds.onBannerFailToReceived += OnBannerLoadFailed;
    }

    void UnRegistHandle() {
        TecentGDTAds.onBannerDidReceived -= OnBannerLoad;
        TecentGDTAds.onBannerFailToReceived -= OnBannerLoadFailed;
    }
#endif

#if UNITY_ANDROID
    void onBannerEvent(string eventName, string msg) {
        Debug.Log("handler onTencentBannerEvent---" + eventName + "   " + msg);
        if (eventName == TecentEvent.onADReceiv) {
            if (AdLoaded != null) {
                AdLoaded(this, new AdsEventArgs());
            }
        } else if( eventName == TecentEvent.onNoAD) {
            Collecter.Failed("no ads");
        }
    }
#elif UNITY_IPHONE
    void OnBannerLoad(object sender, EventArgs args) {
        if (AdLoaded != null) {
            AdLoaded(this, new AdsEventArgs());
        }
    }

    void OnBannerLoadFailed(object sender,EventArgs args) {
        Collecter.Failed("load failed");
    }
#endif


    public override void SetIndividualData(long _TimeWind, int count) {
        IndividualData = new AdsIndividualData(_TimeWind, count);
        Collecter = new DataCollecter(MType, SDKAdsType.Banner);
    }

    public override void CheckAds() {
#if UNITY_ANDROID
        AdManager.Instance(AD).showBanner();
#elif UNITY_IPHONE
        TecentGDTAds.showBanner();
#endif
    }

    public override void LoadAndDisplay() {
        IndividualData.UpdateShowCount();
#if UNITY_ANDROID
        AdManager.Instance(AD).showBanner();
#elif UNITY_IPHONE
        TecentGDTAds.showBanner();
#endif
    }

    public override void Hide() {
#if UNITY_ANDROID
        AdManager.Instance(AD).removeBanner();
#elif UNITY_IPHONE
        TecentGDTAds.closeBanner();
#endif
        _isDestory = true;
    }

    public override void Destroy() {
        if (!_isDestory) {
            Hide();
        }
    }
}
