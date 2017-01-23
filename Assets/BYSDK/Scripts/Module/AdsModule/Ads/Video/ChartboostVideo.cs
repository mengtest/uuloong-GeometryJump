using UnityEngine;
using System.Collections;
using System;
using ChartboostSDK;

public class ChartboostVideo : AbstractVideo {
    public override event EventHandler<AdsEventArgs> OnVoidStart;
    bool _isLoaded = false;
    bool _loadMutex = false;
    public ChartboostVideo() {
        SetupDelegates();
#if UNITY_IPHONE
        Chartboost.setShouldPauseClickForConfirmation(false);
#endif
        Chartboost.setMediation(CBMediation.AdMob, "1.0");
        Chartboost.Create();
        Chartboost.setAutoCacheAds(false);
    }

    ~ChartboostVideo() {
        RemoveDelegates();
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.VideoAds);
    }

    public override bool CheckAds() {
        BYLog.Log("CheckAds");
        if (!Chartboost.hasRewardedVideo(CBLocation.GameScreen)) {
            BYLog.Log("Chart boost CheckAds, to load ads");
            Chartboost.cacheRewardedVideo(CBLocation.GameScreen);
            return false;
        }
        return true;
    }

    public override void Display(BYVideoResulte resulte = null) {
        videoresulte = resulte;
        if (!Chartboost.hasRewardedVideo(CBLocation.GameScreen) && null != videoresulte) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Failed));
            return;
        }
        IndividualData.UpdateShowCount();
        Chartboost.showRewardedVideo(CBLocation.GameScreen);
    }

    public override bool IsReady() {
        //  return Chartboost.hasRewardedVideo(CBLocation.GameScreen);
        return _isLoaded && IndividualData.IsTimeWindOpen();
    }

    public override void Load() {
        BYLog.Log("Load");
        if( !_loadMutex) {
            Chartboost.cacheRewardedVideo(CBLocation.GameScreen);
            _loadMutex = true;
        }

    }

    public override void ReLoad() {
        BYLog.Log("ReLoad");
        Chartboost.cacheRewardedVideo(CBLocation.GameScreen);
    }

    void SetupDelegates() {
        // Listen to all impression-related events
        Chartboost.didInitialize += didInitialize;

        Chartboost.didFailToRecordClick += didFailToRecordClick;
        Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
        Chartboost.didDismissRewardedVideo += didDismissRewardedVideo;
        Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
        Chartboost.didClickRewardedVideo += didClickRewardedVideo;
        Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
        Chartboost.shouldDisplayRewardedVideo += shouldDisplayRewardedVideo;
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;

        Chartboost.didPauseClickForConfirmation += didPauseClickForConfirmation;
        Chartboost.willDisplayVideo += willDisplayVideo;
#if UNITY_IPHONE
        Chartboost.didCompleteAppStoreSheetFlow += didCompleteAppStoreSheetFlow;
#endif
    }

    void RemoveDelegates() {
        // Remove event handlers
        Chartboost.didInitialize -= didInitialize;
        Chartboost.didFailToRecordClick -= didFailToRecordClick;
        Chartboost.didFailToLoadRewardedVideo -= didFailToLoadRewardedVideo;
        Chartboost.didDismissRewardedVideo -= didDismissRewardedVideo;
        Chartboost.didCloseRewardedVideo -= didCloseRewardedVideo;
        Chartboost.didClickRewardedVideo -= didClickRewardedVideo;
        Chartboost.didCacheRewardedVideo -= didCacheRewardedVideo;
        Chartboost.shouldDisplayRewardedVideo -= shouldDisplayRewardedVideo;
        Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
        Chartboost.didDisplayRewardedVideo -= didDisplayRewardedVideo;
        Chartboost.didPauseClickForConfirmation -= didPauseClickForConfirmation;
        Chartboost.willDisplayVideo -= willDisplayVideo;
#if UNITY_IPHONE
        Chartboost.didCompleteAppStoreSheetFlow -= didCompleteAppStoreSheetFlow;
#endif
    }

    void didInitialize(bool status) {
        BYLog.Log(string.Format("didInitialize: {0}", status));
    }

    void didFailToRecordClick(CBLocation location, CBClickError error) {
        BYLog.Log(string.Format("didFailToRecordClick: {0} at location: {1}", error, location));
    }

    void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error) {
        _isLoaded = false;

        EventHandler<AdsEventArgs> handle = OnVoidStart;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
        OnVoidStart = null;
        Collecter.Failed("load failed");
        if (null != videoresulte) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Failed));
            videoresulte = null;
        }
        BYLog.Log(string.Format("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
    }

    void didDismissRewardedVideo(CBLocation location) {
        _isLoaded = false;
        BYLog.Log("didDismissRewardedVideo: " + location);
    }

    void didCloseRewardedVideo(CBLocation location) {
        OnVoidStart = null;
        _isLoaded = false;
        if( null != videoresulte ) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Success));
        }
        videoresulte = null;
        Collecter.AdsClosed();
        BYLog.Log("didCloseRewardedVideo: " + location);
    }

    void didClickRewardedVideo(CBLocation location) {
        BYLog.Log("didClickRewardedVideo: " + location);
        Collecter.AdsClick();
    }

    void didCacheRewardedVideo(CBLocation location) {
        _isLoaded = true;
        _loadMutex = false;
        BYLog.Log("didCacheRewardedVideo: " + location);
    }

    bool shouldDisplayRewardedVideo(CBLocation location) {
        BYLog.Log("shouldDisplayRewardedVideo @" + location + " : " + true);
        return true;
    }

    void didCompleteRewardedVideo(CBLocation location, int reward) {
        BYLog.Log(string.Format("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
    }

    void didDisplayRewardedVideo(CBLocation location) {
        BYLog.Log("didDisplayRewardedVideo: " + location);
        EventHandler<AdsEventArgs> handle = OnVoidStart;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
        OnVoidStart = null;
        Collecter.AdsShow();
    }

    void didPauseClickForConfirmation() {
#if UNITY_IPHONE
        BYLog.Log("didPauseClickForConfirmation called");
#endif
    }

    void willDisplayVideo(CBLocation location) {
        BYLog.Log("willDisplayVideo: " + location);
    }

#if UNITY_IPHONE
    void didCompleteAppStoreSheetFlow() {
        BYLog.Log("didCompleteAppStoreSheetFlow");
    }

    void TrackIAP() {
        // The iOS receipt data from Unibill is already base64 encoded.
        // Others store kit plugins may be different.
        // This is a sample sandbox receipt.
        string sampleReceipt = @"ewoJInNpZ25hdHVyZSIgPSAiQXBNVUJDODZBbHpOaWtWNVl0clpBTWlKUWJLOEVk
                                    ZVhrNjNrV0JBWHpsQzhkWEd1anE0N1puSVlLb0ZFMW9OL0ZTOGNYbEZmcDlZWHQ5
                                    aU1CZEwyNTBsUlJtaU5HYnloaXRyeVlWQVFvcmkzMlc5YVIwVDhML2FZVkJkZlcr
                                    T3kvUXlQWkVtb05LeGhudDJXTlNVRG9VaFo4Wis0cFA3MHBlNWtVUWxiZElWaEFB
                                    QURWekNDQTFNd2dnSTdvQU1DQVFJQ0NHVVVrVTNaV0FTMU1BMEdDU3FHU0liM0RR
                                    RUJCUVVBTUg4eEN6QUpCZ05WQkFZVEFsVlRNUk13RVFZRFZRUUtEQXBCY0hCc1pT
                                    QkpibU11TVNZd0pBWURWUVFMREIxQmNIQnNaU0JEWlhKMGFXWnBZMkYwYVc5dUlF
                                    RjFkR2h2Y21sMGVURXpNREVHQTFVRUF3d3FRWEJ3YkdVZ2FWUjFibVZ6SUZOMGIz
                                    SmxJRU5sY25ScFptbGpZWFJwYjI0Z1FYVjBhRzl5YVhSNU1CNFhEVEE1TURZeE5U
                                    SXlNRFUxTmxvWERURTBNRFl4TkRJeU1EVTFObG93WkRFak1DRUdBMVVFQXd3YVVI
                                    VnlZMmhoYzJWU1pXTmxhWEIwUTJWeWRHbG1hV05oZEdVeEd6QVpCZ05WQkFzTUVr
                                    RndjR3hsSUdsVWRXNWxjeUJUZEc5eVpURVRNQkVHQTFVRUNnd0tRWEJ3YkdVZ1NX
                                    NWpMakVMTUFrR0ExVUVCaE1DVlZNd2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZ
                                    MEFNSUdKQW9HQkFNclJqRjJjdDRJclNkaVRDaGFJMGc4cHd2L2NtSHM4cC9Sd1Yv
                                    cnQvOTFYS1ZoTmw0WElCaW1LalFRTmZnSHNEczZ5anUrK0RyS0pFN3VLc3BoTWRk
                                    S1lmRkU1ckdYc0FkQkVqQndSSXhleFRldngzSExFRkdBdDFtb0t4NTA5ZGh4dGlJ
                                    ZERnSnYyWWFWczQ5QjB1SnZOZHk2U01xTk5MSHNETHpEUzlvWkhBZ01CQUFHamNq
                                    QndNQXdHQTFVZEV3RUIvd1FDTUFBd0h3WURWUjBqQkJnd0ZvQVVOaDNvNHAyQzBn
                                    RVl0VEpyRHRkREM1RllRem93RGdZRFZSMFBBUUgvQkFRREFnZUFNQjBHQTFVZERn
                                    UVdCQlNwZzRQeUdVakZQaEpYQ0JUTXphTittVjhrOVRBUUJnb3Foa2lHOTJOa0Jn
                                    VUJCQUlGQURBTkJna3Foa2lHOXcwQkFRVUZBQU9DQVFFQUVhU2JQanRtTjRDL0lC
                                    M1FFcEszMlJ4YWNDRFhkVlhBZVZSZVM1RmFaeGMrdDg4cFFQOTNCaUF4dmRXLzNl
                                    VFNNR1k1RmJlQVlMM2V0cVA1Z204d3JGb2pYMGlreVZSU3RRKy9BUTBLRWp0cUIw
                                    N2tMczlRVWU4Y3pSOFVHZmRNMUV1bVYvVWd2RGQ0TndOWXhMUU1nNFdUUWZna1FR
                                    Vnk4R1had1ZIZ2JFL1VDNlk3MDUzcEdYQms1MU5QTTN3b3hoZDNnU1JMdlhqK2xv
                                    SHNTdGNURXFlOXBCRHBtRzUrc2s0dHcrR0szR01lRU41LytlMVFUOW5wL0tsMW5q
                                    K2FCdzdDMHhzeTBiRm5hQWQxY1NTNnhkb3J5L0NVdk02Z3RLc21uT09kcVRlc2Jw
                                    MGJzOHNuNldxczBDOWRnY3hSSHVPTVoydG04bnBMVW03YXJnT1N6UT09IjsKCSJw
                                    dXJjaGFzZS1pbmZvIiA9ICJld29KSW05eWFXZHBibUZzTFhCMWNtTm9ZWE5sTFdS
                                    aGRHVXRjSE4wSWlBOUlDSXlNREV5TFRBMExUTXdJREE0T2pBMU9qVTFJRUZ0WlhK
                                    cFkyRXZURzl6WDBGdVoyVnNaWE1pT3dvSkltOXlhV2RwYm1Gc0xYUnlZVzV6WVdO
                                    MGFXOXVMV2xrSWlBOUlDSXhNREF3TURBd01EUTJNVGM0T0RFM0lqc0tDU0ppZG5K
                                    eklpQTlJQ0l5TURFeU1EUXlOeUk3Q2draWRISmhibk5oWTNScGIyNHRhV1FpSUQw
                                    Z0lqRXdNREF3TURBd05EWXhOemc0TVRjaU93b0pJbkYxWVc1MGFYUjVJaUE5SUNJ
                                    eElqc0tDU0p2Y21sbmFXNWhiQzF3ZFhKamFHRnpaUzFrWVhSbExXMXpJaUE5SUNJ
                                    eE16TTFOems0TXpVMU9EWTRJanNLQ1NKd2NtOWtkV04wTFdsa0lpQTlJQ0pqYjIw
                                    dWJXbHVaRzF2WW1Gd2NDNWtiM2R1Ykc5aFpDSTdDZ2tpYVhSbGJTMXBaQ0lnUFNB
                                    aU5USXhNVEk1T0RFeUlqc0tDU0ppYVdRaUlEMGdJbU52YlM1dGFXNWtiVzlpWVhC
                                    d0xrMXBibVJOYjJJaU93b0pJbkIxY21Ob1lYTmxMV1JoZEdVdGJYTWlJRDBnSWpF
                                    ek16VTNPVGd6TlRVNE5qZ2lPd29KSW5CMWNtTm9ZWE5sTFdSaGRHVWlJRDBnSWpJ
                                    d01USXRNRFF0TXpBZ01UVTZNRFU2TlRVZ1JYUmpMMGROVkNJN0Nna2ljSFZ5WTJo
                                    aGMyVXRaR0YwWlMxd2MzUWlJRDBnSWpJd01USXRNRFF0TXpBZ01EZzZNRFU2TlRV
                                    Z1FXMWxjbWxqWVM5TWIzTmZRVzVuWld4bGN5STdDZ2tpYjNKcFoybHVZV3d0Y0hW
                                    eVkyaGhjMlV0WkdGMFpTSWdQU0FpTWpBeE1pMHdOQzB6TUNBeE5Ub3dOVG8xTlNC
                                    RmRHTXZSMDFVSWpzS2ZRPT0iOwoJImVudmlyb25tZW50IiA9ICJTYW5kYm94IjsK
                                    CSJwb2QiID0gIjEwMCI7Cgkic2lnbmluZy1zdGF0dXMiID0gIjAiOwp9";

        // Demonstrate Base64 encoding. Not necessary for the data above
        // If the receipt was not base64 encoded, send encodedText not sampleReceipt
        //byte[] bytesToEncode = Encoding.UTF8.GetBytes(sampleReceipt);
        //string encodedText = Convert.ToBase64String(bytesToEncode);

        // Send the receipt for track an In App Purchase Event
        Chartboost.trackInAppAppleStorePurchaseEvent(sampleReceipt,
                "sample product title", "sample product description", "1.99", "USD", "sample product identifier");
        //byte[] decodedText = Convert.FromBase64String(sampleReceipt);
        //Debug.Log("Decoded: " + System.Text.Encoding.UTF8.GetString(decodedText));
        //Debug.Log("Encoded: " + encodedText);
    }
#elif UNITY_ANDROID
    void TrackIAP() {
        Debug.Log("TrackIAP");
        // title, description, price, currency, productID, purchaseData, purchaseSignature
        // This data should be sent after handling the results from the google store.
        // This is fake data and doesn't represent a real or even imaginary purchase
        Chartboost.trackInAppGooglePlayPurchaseEvent("SampleItem", "TestPurchase", "0.99", "USD", "ProductID", "PurchaseData", "PurchaseSignature");

        // If you are using the Amazon store...
        //Chartboost.trackInAppAmazonStorePurchaseEvent("SampleItem", "TestPurchase", "0.99", "ProductID", "UserId", "PurchaseToken");
    }
#else
    void TrackIAP() {
        Debug.Log("TrackIAP on unsupported platform");
    }
#endif

}
