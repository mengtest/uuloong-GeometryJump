using UnityEngine;
using System.Collections;
using System;

public class AppLIAds : MonoBehaviour {

    public static bool isInit = false;
    #region Interstitial ads handle
    public static event EventHandler<EventArgs> onInterstitialLoad;
    public static event EventHandler<EventArgs> onInterstitialLoadFailed;
    public static event EventHandler<EventArgs> onInterstitialClose;
    public static event EventHandler<EventArgs> onInterstitialPlay;
    #endregion


    #region Rewarded video ads handle
    public static event EventHandler<EventArgs> onVideoLoad;
    public static event EventHandler<EventArgs> onVideoLoadFailed;
    public static event EventHandler<EventArgs> onVideoPlay;
    public static event EventHandler<EventArgs> onVideoClose;
    public static event EventHandler<EventArgs> onVideoSkip;
    public static event EventHandler<EventArgs> onVideoBeShow;
    public static event EventHandler<EventArgs> onVideoStop;
    #endregion

    #region ERROR
    public static event EventHandler<EventArgs> onTimeOut;
    #endregion

    static AppLIAds() {
        if (AppLIAds.isInit)
            return;
        try {
            var go = new GameObject("AppLIAds");
            go.AddComponent<AppLIAds>();
            DontDestroyOnLoad(go);
            init();
        } catch (UnityException) {
            Debug.LogWarning("It looks like you have the AppLIAds on a GameObject in your scene. Please remove the script from your scene.");
        }
    }

    public static void noop() { }
    public void onAppLovinEventReceived(string evn) {
        BYLog.Log("Applovin event: " + evn);
        EventHandler<EventArgs> handler = null;
        if (evn.Contains(AppLIEvents.INTERLOAD_SUCCESS)) {
            handler = onInterstitialLoad;
        } else if( evn.Contains(AppLIEvents.INTERLOAD_FAILED)) {
            handler = onInterstitialLoadFailed;
        } else if(evn.Contains(AppLIEvents.DISPLAY_INTER)) {
            handler = onInterstitialPlay;
        } else if(evn.Contains(AppLIEvents.HIDDEN_INTER)) {
            handler = onInterstitialClose;
        } else if(evn.Contains(AppLIEvents.VIDEOLOAD_SUCCESS)) {
            handler = onVideoLoad;
        } else if(evn.Contains(AppLIEvents.VIDEOLOAD_FAILED)) {
            handler = onVideoLoadFailed;
        } else if(evn.Contains(AppLIEvents.DISPLAY_VIDEO)) {
            handler = onVideoPlay;
        } else if(evn.Contains(AppLIEvents.HIDDEN_VIDEO)) {
            handler = onVideoClose;
        } else if(evn.Contains(AppLIEvents.SKIP_VIDEO)) {
            handler = onVideoSkip;
        } else if(evn.Contains(AppLIEvents.VIDEO_PLAY)) {
            handler = onVideoBeShow;
        } else if(evn.Contains(AppLIEvents.VIDEO_STOPED)) {
            handler = onVideoStop;
        } else if(evn.Contains(AppLIEvents.VIDEO_TIMEOUT)) {
            handler = onTimeOut;
        }

        if (null != handler) {
            handler(this, new EventArgs());
        }
    }


    public static void init() {
        if( isInit) {
            BYLog.Log(" AppLovIn is already init.");
            return;
        }

        BYLog.Log(" AppLovIn begin to init");
        AppLovin.SetSdkKey(AdsConfig.APPLOVIN_SDKID);
        AppLovin.InitializeSdk();
        AppLovin.SetUnityAdListener("AppLIAds");
        BYLog.Log(" set Listener is AppLIAds");
        isInit = true;
    }

    public static void LoadInterstitial() {
        if (!AppLovin.HasPreloadedInterstitial()) {
            BYLog.Log("loading Interstitial ads");
            AppLovin.PreloadInterstitial();
        } else {
            BYLog.Log("Interstitial ads had loaded not need reload");
        }
    }

    public static bool IsInterReady() {
        return AppLovin.HasPreloadedInterstitial();
    }
    public static void DisplayInterstitial() {
        if (AppLovin.HasPreloadedInterstitial()) {
            BYLog.Log("Interstitial ads had loaded ,begin to show");
            AppLovin.ShowInterstitial();
        } else {
            BYLog.Log("Interstitial ads had not loaded ,need to manually to reload it");
        }
    }

    public static void LoadVideo() {
        if (!AppLovin.IsIncentInterstitialReady()) {
            BYLog.Log("Video not ready ,to load it");
            AppLovin.LoadRewardedInterstitial();
        } else {
            BYLog.Log("Video is ready,not need to load ");
        }
    }

    public static bool IsVideoReady() {
        return AppLovin.IsIncentInterstitialReady();
    }
    public static void DisplayVideo() {
        if (AppLovin.IsIncentInterstitialReady()) {
            BYLog.Log("Video  to show");
            AppLovin.ShowRewardedInterstitial();
        } else {
            BYLog.Log("Video ads had not loaded ,need to manually to reload it");
        }
    }
}
