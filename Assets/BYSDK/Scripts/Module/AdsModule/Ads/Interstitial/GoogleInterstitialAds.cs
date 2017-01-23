using UnityEngine;
using System;
using GoogleMobileAds.Api;


public class GoogleInterstitialAds : InterstitialAds {
    public string admobID = "";

    InterstitialAd interstitial;

    public override event EventHandler<AdsEventArgs> AdLoaded = delegate { };
    public override event EventHandler<AdsEventArgs> AdFailedToLoad = delegate { };
    public override event EventHandler<AdsEventArgs> AdOpened = delegate { };
    public override event EventHandler<AdsEventArgs> AdClosed = delegate { };

    private bool _RequestMuxLock = false;

    public GoogleInterstitialAds() {
        createInterstitialAd();
    }

    public GoogleInterstitialAds(string aAdmobID) {
        admobID = aAdmobID;
        createInterstitialAd();
    }

    ~GoogleInterstitialAds() {
        interstitial.Destroy();
        interstitial.OnAdLoaded -= HandleAdLoaded;
        interstitial.OnAdFailedToLoad -= HandleAdFailedToLoad;
        interstitial.OnAdOpening -= HandleAdOpening;
        interstitial.OnAdClosed -= HandleAdClosed;
        interstitial.OnAdLeavingApplication -= HandleAdLeftApplication;
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.Interstitial);
    }

    private void createInterstitialAd() {
        interstitial = new InterstitialAd(admobID);
        BYLog.Log(" Create Interstitial ads with admobID: " + admobID.ToString());
        // The C# compiler's default implementation of adding an event handler calls Delegate.Combine,
        //while removing an event handler calls Delegate.Remove:
        interstitial.OnAdLoaded += HandleAdLoaded;
        interstitial.OnAdFailedToLoad += HandleAdFailedToLoad;
        interstitial.OnAdOpening += HandleAdOpening;
        interstitial.OnAdClosed += HandleAdClosed;
        interstitial.OnAdLeavingApplication += HandleAdLeftApplication;
        Load();
    }

    public override void Load() {
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        BYLog.Log(" Begin to load google Interstitial ,admobID: " + admobID.ToString());
        _RequestMuxLock = true;
    }

    public override void Display() {
        BYLog.Log(" Enter google interstitial Display,admobID: " + admobID.ToString());
        if (interstitial.IsLoaded()) {
            BYLog.Log(" Google interstitial begin to show,admobID: " + admobID.ToString());
            IndividualData.UpdateShowCount();
            interstitial.Show();
        } else {
            if (!_RequestMuxLock) {
                BYLog.Log(" Google interstitial had not load ,begin to load it,admobID: " + admobID.ToString());
                Load();
            }
        }
    }

    public override bool IsReady() {
        return interstitial.IsLoaded() && IndividualData.IsTimeWindOpen();
    }
    public override void CheckAds() {
        if (!interstitial.IsLoaded()) {
            ReLoad();
        } else {
            BYLog.Log(" Google Interstitial is Ready,admobID: " + admobID.ToString());
        }
    }
    public override void ReLoad() {
        interstitial.OnAdLoaded -= HandleAdLoaded;
        interstitial.OnAdFailedToLoad -= HandleAdFailedToLoad;
        interstitial.OnAdOpening -= HandleAdOpening;
        interstitial.OnAdClosed -= HandleAdClosed;
        interstitial.OnAdLeavingApplication -= HandleAdLeftApplication;
        interstitial.Destroy();
        interstitial = null;
        createInterstitialAd();
        Load();
    }

    public void HandleAdLoaded(object sender, EventArgs args) {
        // Process this event internally

        // unlock request lock
        _RequestMuxLock = false;
        BYLog.Log(" C# ##### Google Load Interstitial loaded Successfull ,admobID: " + admobID.ToString());
        EventHandler<AdsEventArgs> handler = AdLoaded;
        if (handler != null) {
            handler(this, new AdsEventArgs());
        }
        AdLoaded = null;
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        // Process this event internally

        // unlock request lock
        _RequestMuxLock = false;

        EventHandler<AdsEventArgs> handler = AdFailedToLoad;
        if (handler != null) {
            AdsEventArgs aAdsArgs = new AdsEventArgs();
            aAdsArgs.Message = args.Message;

            handler(this, aAdsArgs);
        }
        AdFailedToLoad = null;
        Collecter.Failed("load failed");
    }

    public void HandleAdOpening(object sender, EventArgs args) {
        // Process this event internally
        BYLog.Log(" C# ##### Google Load Interstitial loaded Successfull ,admobID: " + admobID.ToString());
        EventHandler<AdsEventArgs> handler = AdOpened;
        if (handler != null) {
            handler(this, new AdsEventArgs());
        }
        AdOpened = null;
        Collecter.AdsShow();
    }

    public void HandleAdClosed(object sender, EventArgs args) {
        // Process this event internally

        // Whenever an google interstitial ads have already been displayed
        // the interstitial object were banished by AdMob adk itself.
        // Hence, it is needed to recreate an interstitial object.
        BYLog.Log(" C# ##### Google Load Interstitial closed and begin to reload ,admobID: " + admobID.ToString());
        EventHandler<AdsEventArgs> handler = AdClosed;
        if (handler != null) {
            handler(this, new AdsEventArgs());
        }
        AdClosed = null;
        Collecter.AdsClosed();
    }

    public void HandleAdLeftApplication(object sender, EventArgs args) {
        BYLog.Log("HandleAdLeftApplication event received,admobID: " + admobID.ToString());
        Collecter.AdsClick();
    }

}