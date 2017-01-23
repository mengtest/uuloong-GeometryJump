using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public  class InMoBiEventHandle  {
    public  event EventHandler<AdsEventArgs> InterLoaded;
    public  event EventHandler<AdsEventArgs> InterLoadFailed;
    public  event EventHandler<AdsEventArgs> InterOpen;
    public  event EventHandler<AdsEventArgs> InterClose;

    public  event EventHandler<AdsEventArgs> BannerLoaded;
    public  event EventHandler<AdsEventArgs> BannerOpend;
    public  event EventHandler<AdsEventArgs> BannerFailedLoad;
    public  event EventHandler<AdsEventArgs> BannerClosed;
    public InMoBiEventHandle() {
        RegistHandle();
    }

    void RegistHandle() {
#if UNITY_IPHONE
        InMobiManager.interstitialDidReceiveAdEvent += OnInterstitialLoad;
        InMobiManager.interstitialDidFailToReceiveAdWithErrorEvent += OnInterstitialLoadFailed;
        InMobiManager.interstitialDidPresentScreenEvent += OnInterstitialOpen;
        InMobiManager.interstitialWillDismissScreenEvent += OnInterstitialClosed;
        InMobiManager.bannerDidReceiveAdEvent += OnBannerLoaded;
        InMobiManager.bannerDidFailToReceiveAdWithErrorEvent += OnBannerLoadFailed ;
        InMobiManager.bannerDidDismissScreenEvent += OnBannerClosed;
        InMobiManager.bannerDidInteractEvent += OnBannerOpend;
#elif UNITY_ANDROID
        InMobiAndroidManager.onInterstitialLoadedEvent += OnInterstitialLoad;
        InMobiAndroidManager.onInterstitialFailedEvent += OnInterstitialLoadFailed;
        InMobiAndroidManager.onShowInterstitialScreenEvent += OnInterstitialOpen;
        InMobiAndroidManager.onDismissInterstitialScreenEvent += OnInterstitialClosed;
#endif
    }
    void OnBannerOpend(Dictionary<string, object> args) {
        BYLog.Log("InMoBI Banner Opend");
        EventHandler<AdsEventArgs> handle = BannerOpend;
        if (handle != null) {
            handle(this, new AdsEventArgs());
        }
    }

    void OnBannerClosed() {
        BYLog.Log("InMoBI Banner Closed");
        EventHandler<AdsEventArgs> handle = BannerClosed;
        if (handle != null) {
            handle(this, new AdsEventArgs());
        }
    }
    void OnBannerLoadFailed(string error) {
        BYLog.Log("InMoBi Banner load error: " + error);
        EventHandler<AdsEventArgs> handle = BannerFailedLoad;
        if (handle != null) {
            var e = new AdsEventArgs();
            e.Message = error;
            handle(this, e);
        }
    }
    void OnBannerLoaded() {
        BYLog.Log("InMoBI Banner loaded");
        EventHandler<AdsEventArgs> handle = BannerLoaded;
        if( handle != null ) {
            handle(this, new AdsEventArgs());
        }
    }
    void OnInterstitialLoad() {
        BYLog.Log("InMoBI Interstitial loaded");
        EventHandler<AdsEventArgs> handle = InterLoaded;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
    }

    void OnInterstitialLoadFailed(string error) {
        BYLog.Log("InMoBi Interstitial load failed: error info " + error);
        EventHandler<AdsEventArgs> handle = InterLoadFailed;
        if (null != handle) {
            AdsEventArgs e = new AdsEventArgs();
            e.Message = error;
            handle(this, e);
        }
    }

    void OnInterstitialOpen() {
        BYLog.Log("InMoBi Interstitial is Open");
        EventHandler<AdsEventArgs> handle = InterOpen;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
    }

    void OnInterstitialClosed() {
        BYLog.Log("InMoBi Interstitial is Closed");
        EventHandler<AdsEventArgs> handle = InterClose;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
    }
}
