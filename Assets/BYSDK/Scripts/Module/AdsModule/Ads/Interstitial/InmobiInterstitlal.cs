using UnityEngine;
using System;


public class InMobiInterstitial : InterstitialAds {
    InMoBiAds InMobiClient;

    public override event EventHandler<AdsEventArgs> AdLoaded = delegate { };
    public override event EventHandler<AdsEventArgs> AdFailedToLoad = delegate { };
    public override event EventHandler<AdsEventArgs> AdOpened = delegate { };
    public override event EventHandler<AdsEventArgs> AdClosed = delegate { };

    InMoBiEventHandle _EventHandle;
    public InMobiInterstitial() {
        InMobiClient = InMobiInstance.GetInstance();
        _EventHandle = new InMoBiEventHandle();
        _EventHandle.InterLoaded        += OnLoaded;
        _EventHandle.InterLoadFailed    += OnLoadFailed;
        _EventHandle.InterOpen          += OnOpen;
        _EventHandle.InterClose         += OnClosed;
        BYLog.Log("InMoBi Interstitial ads created ");
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.Interstitial);
    }

    public override void Load() {
        BYLog.Log("InMoBi Interstitial ads loading... ");
        InMobiClient.LoadInterstitial();
    }

    public override void Display() {
        Debug.Log("Display InMobiInterstitial");
        if (InMobiClient.IsInterstitialReady()) {
            Debug.Log("IsInterstitialReady InMobiInterstitial");
            IndividualData.UpdateShowCount();
            InMobiClient.ShowInterstitial();
            InMobiClient.LoadInterstitial();
        } else {
            InMobiClient.LoadInterstitial();
        }
    }

    public override void ReLoad() {
        InMobiClient.LoadInterstitial();
    }

    public override void CheckAds() {
        if( !IsReady()) {
            Load();
        }
    }

    public override bool IsReady() {
        return InMobiClient.IsInterstitialReady() && IndividualData.IsTimeWindOpen() ;
    }

    void OnLoaded(object sender, AdsEventArgs args) {
        BYLog.Log("InMoBi Interstitial ads loaded");
        EventHandler<AdsEventArgs> handle = AdLoaded;
        if( null != handle ) {
            handle(this, args);
        }
        AdLoaded = null;
    }

    void OnLoadFailed(object sender, AdsEventArgs args) {
        BYLog.Log("InMoBi Interstitial ads loaded faile: " + args.Message);
        EventHandler<AdsEventArgs> handle = AdFailedToLoad;
        if (null != handle) {
            handle(this, args);
        }
        AdFailedToLoad = null;
        Collecter.Failed("load failed");
    }

    void OnOpen(object sender, AdsEventArgs args) {
        BYLog.Log("InMoBi Interstitial ads showing ");
        EventHandler<AdsEventArgs> handle = AdOpened;
        if (null != handle) {
            handle(this, args);
        }
        AdOpened = null;
        Collecter.AdsShow();
    }

    void OnClosed(object sender, AdsEventArgs args) {
        BYLog.Log("InMoBi Interstitial ads loaded");
        EventHandler<AdsEventArgs> handle = AdClosed;
        if (null != handle) {
            handle(this, args);
        }
        AdClosed = null;
        Collecter.AdsClosed();
    }
}