using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class GoogleBannerAds : BannerAds {
    BannerView banner;

    public string admobID = "ca-app-pub-2235431687140866/3041620035";
    public override event EventHandler<AdsEventArgs> AdLoaded = delegate { };
    public override event EventHandler<AdsEventArgs> AdFailedToLoad = delegate { };
    public override event EventHandler<AdsEventArgs> AdOpened = delegate { };
    public override event EventHandler<AdsEventArgs> AdClosed = delegate { };

    private bool _LoadMutex = false;
    private bool isLoad = false;
    private bool _isDestory = false;
    public GoogleBannerAds() {
        createBannerView();
    }

    public GoogleBannerAds(string aAdmobID) {
        this.admobID = aAdmobID;
        createBannerView();
    }
    public override void SetIndividualData(long _TimeWind, int count) {
        IndividualData = new AdsIndividualData(_TimeWind, count);
        Collecter = new DataCollecter(MType, SDKAdsType.Banner);
    }
    ~GoogleBannerAds() {
        BYLog.Log("Banner Destroy ,admobID: " + admobID.ToString());
        if(!_isDestory) {
            banner.Destroy();
        }

        banner.OnAdLoaded -= HandleAdLoaded;
        banner.OnAdFailedToLoad -= HandleAdFailedToLoad;
        banner.OnAdOpening -= HandleAdOpening;
        banner.OnAdClosed -= HandleAdClosed;
        banner.OnAdLeavingApplication -= HandleAdLeftApplication;
    }

    private void createBannerView() {
        BYLog.Log("Create Banner,admobID: " + admobID.ToString());
        banner = new BannerView(admobID, AdSize.SmartBanner, AdPosition.Bottom);
        banner.OnAdLoaded += HandleAdLoaded;
        banner.OnAdFailedToLoad += HandleAdFailedToLoad;
        banner.OnAdOpening += HandleAdOpening;
        banner.OnAdClosed += HandleAdClosed;
        banner.OnAdLeavingApplication += HandleAdLeftApplication;
        _isDestory = false;
    }

    public override void LoadAndDisplay() {
        if (_LoadMutex) {
            BYLog.Log("Load Banner is loading ,not need load again,admobID: " + admobID.ToString());
            return;
        }
        BYLog.Log("Load Banner ads");
        AdRequest requester = new AdRequest.Builder().Build();
        IndividualData.UpdateShowCount();
        banner.LoadAd(requester);
        _LoadMutex = true;
    }
    public override void CheckAds() {
        if (isLoad) {
            BYLog.Log("Banner is not load ,after check .begin to reload it");
            LoadAndDisplay();
        } else {
            BYLog.Log("Banner is  loaded complete after check ads");
        }
    }

    public override void Hide() {
        banner.Hide();
    }

    public override void Destroy() {
        banner.Destroy();
    }
    public void HandleAdLoaded(object sender, EventArgs args) {
        // Process this event internally
        isLoad = true;
        BYLog.Log("Banner is loaded");
        EventHandler<AdsEventArgs> handler = AdLoaded;
        if (handler != null) {
            handler(this, new AdsEventArgs());
        }
    }


    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        // Process this event internally
        isLoad = false;
        BYLog.Log("Banner ads load failed");
        EventHandler<AdsEventArgs> handler = AdFailedToLoad;
        if (handler != null) {
            AdsEventArgs aAdsArgs = new AdsEventArgs();
            aAdsArgs.Message = args.Message;

            handler(this, aAdsArgs);
        }
        Collecter.Failed("load failed");
    }

    public void HandleAdOpening(object sender, EventArgs args) {
        // Process this event internally
        EventHandler<AdsEventArgs> handler = AdOpened;
        if (handler != null) {
            handler(this, new AdsEventArgs());
        }
    }

    public void HandleAdClosed(object sender, EventArgs args) {
        // Process this event internally
        EventHandler<AdsEventArgs> handler = AdClosed;
        if (handler != null) {
            handler(this, new AdsEventArgs());
        }
    }

    public void HandleAdLeftApplication(object sender, EventArgs args) {
        BYLog.Log("HandleAdLeftApplication event received");
    }

}