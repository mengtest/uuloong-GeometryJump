using UnityEngine;
using System.Collections;
using System;
public class AppLovInInterstitial : InterstitialAds {
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdOpened;
    public override event EventHandler<AdsEventArgs> AdClosed;

    bool _loadMutex = false;
    bool _isLoadFailed = false;
    public AppLovInInterstitial() {
        AppLIAds.noop();
        RegistHandle();
    }

    ~AppLovInInterstitial() {
        UnRegistHandle();
    }


    void RegistHandle() {
        AppLIAds.onInterstitialLoad         += OnLoad;
        AppLIAds.onInterstitialLoadFailed   += OnLoadFailed;
        AppLIAds.onInterstitialClose        += OnCloseed;
        AppLIAds.onInterstitialPlay         += OnShow;
    }

    void UnRegistHandle() {
        AppLIAds.onInterstitialLoad         -= OnLoad;
        AppLIAds.onInterstitialLoadFailed   -= OnLoadFailed;
        AppLIAds.onInterstitialClose        -= OnCloseed;
        AppLIAds.onInterstitialPlay         -= OnShow;
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.Interstitial);
    }

    public override void Load() {
        AppLIAds.LoadInterstitial();
    }

    public override void ReLoad() {
        BYLog.Log("begin to reload applovin Interstitial ads");
        Load();
    }

    public override bool IsReady() {
        return AppLIAds.IsInterReady()&&IndividualData.IsTimeWindOpen();
    }

    public override void Display() {
        if(AppLIAds.IsInterReady()) {
            BYLog.Log(" AppLovIn Interstitial ads begin to show");
            IndividualData.UpdateShowCount();
            AppLIAds.DisplayInterstitial();
        } else {
            BYLog.Log(" AppLovIn Interstitial ads not loaded, begin to reload");
            OnCloseed(null,new EventArgs());
            ReLoad();
        }

    }

    public override void CheckAds() {
        if(!AppLIAds.IsInterReady()) {
            BYLog.Log("AppLovIn Interstitial not load after Checkads ,begin to load");
            Load();
        } else {
            BYLog.Log("AppLovIn Interstitial was already loaded ");
        }
    }


    public void OnLoad(object sender,EventArgs args) {
        BYLog.Log("AppLovIn Interstitial   loaded ");
        _isLoadFailed = false;
        _loadMutex = false;
    }

    public void OnLoadFailed(object sender,EventArgs args) {
        BYLog.Log("AppLovIn Interstitial ads load failed, wait reload it after check ads");
        Collecter.Failed("Load failed");
    }

    public void OnCloseed(object sender,EventArgs args) {
        EventHandler<AdsEventArgs> handler = AdClosed;
        if (handler != null) {
            handler(this, new AdsEventArgs());
        }
        AdClosed = null;
        Collecter.AdsClosed();
        BYLog.Log("AppLovIn Interstitial ads been closed ,begin to load an new one");
    }

    public void OnShow(object sender,EventArgs args) {
        BYLog.Log("AppLovIn Interstitial ads showing");
        Collecter.AdsShow();
    }
}
