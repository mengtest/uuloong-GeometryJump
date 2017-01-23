using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

#if UNITY_IPHONE

public class TecentGDTAds : MonoBehaviour
{
    private static bool isCreate = false;

    #region Banner ads handle
    public static event EventHandler<EventArgs> onBannerDidReceived;
    public static event EventHandler<EventArgs> onBannerFailToReceived;
    public static event EventHandler<EventArgs> onBannerWillLeaveApplication;
    public static event EventHandler<EventArgs> onBannerWillExposure;
    public static event EventHandler<EventArgs> onBannerClicked;
    public static event EventHandler<EventArgs> onBannerWillClose;
    #endregion

    #region Interstitial ads handle
    public static event EventHandler<EventArgs> onInterstitialSuccessToLoadAd;
    public static event EventHandler<EventArgs> onInterstitialFailToLoadAd;
    public static event EventHandler<EventArgs> onInterstitialWillPresentScreen;
    public static event EventHandler<EventArgs> onInterstitialDidPresentScreen;
    public static event EventHandler<EventArgs> onInterstitialDidDismissScreen;
    public static event EventHandler<EventArgs> onInterstitialApplicationWillEnterBackground;
    public static event EventHandler<EventArgs> onInterstitialWillExposure;
    public static event EventHandler<EventArgs> onInterstitialClicked;
    #endregion

    static TecentGDTAds()
    {
        if (isCreate)
        {
            return;
        }
        try
        {
            var go = new GameObject("TecentGDTAds");
            go.AddComponent<TecentGDTAds>();
            DontDestroyOnLoad(go);
            isCreate = true;
        }
        catch (UnityException)
        {
            Debug.LogWarning("It looks like you have the TecentGDTAds on a GameObject in your scene. Please remove the script from your scene.");
        }
    }

    public static void noop() { }

    public void onTecentGDTEventReceived(string evn)
    {
        Debug.Log("TecentGDTAds onTecentGDTEventReceived:" + evn);
        EventHandler<EventArgs> handler = null;
        if (evn.Contains(TecentGDTEvents.BannerViewClicked))
        {
            handler = onBannerClicked;
        }
        else if (evn.Contains(TecentGDTEvents.BannerViewDidReceived))
        {
            handler = onBannerDidReceived;
        }
        else if (evn.Contains(TecentGDTEvents.BannerViewFailToReceived))
        {
            handler = onBannerFailToReceived;
        }
        else if (evn.Contains(TecentGDTEvents.BannerViewWillClose))
        {
            handler = onBannerWillClose;
        }
        else if (evn.Contains(TecentGDTEvents.BannerViewWillExposure))
        {
            handler = onBannerWillExposure;
        }
        else if (evn.Contains(TecentGDTEvents.BannerViewWillLeaveApplication))
        {
            handler = onBannerWillLeaveApplication;
        }
        else if (evn.Contains(TecentGDTEvents.InterstitialApplicationWillEnterBackground))
        {
            handler = onInterstitialApplicationWillEnterBackground;
        }
        else if (evn.Contains(TecentGDTEvents.InterstitialClicked))
        {
            handler = onInterstitialClicked;
        }
        else if (evn.Contains(TecentGDTEvents.InterstitialDidDismissScreen))
        {
            handler = onInterstitialDidDismissScreen;
        }
        else if (evn.Contains(TecentGDTEvents.InterstitialDidPresentScreen))
        {
            handler = onInterstitialDidPresentScreen;
        }
        else if (evn.Contains(TecentGDTEvents.InterstitialFailToLoadAd))
        {
            handler = onInterstitialFailToLoadAd;
        }
        else if (evn.Contains(TecentGDTEvents.InterstitialSuccessToLoadAd))
        {
            handler = onInterstitialSuccessToLoadAd;
        }
        else if (evn.Contains(TecentGDTEvents.InterstitialWillExposure))
        {
            handler = onInterstitialWillExposure;
        }
        else if (evn.Contains(TecentGDTEvents.InterstitialWillPresentScreen))
        {
            handler = onInterstitialWillPresentScreen;
        }

        if (null != handler)
        {
            handler(this, new EventArgs());
        }
    }

    #region IOS广告接口
    //引入内部Banner广告条的动态链接库
    //显示广告条
    [DllImportAttribute("__Internal")]
    public static extern void showBanner();

    //生成广告条
    [DllImportAttribute("__Internal")]
    public static extern void createBanner(int x, int y, float width, float height, string appKey, string placementId);

    //创建并显示广告条
    [DllImportAttribute("__Internal")]
    public static extern void createBannerAndShow(int x, int y, float width, float height, string appKey, string placementId);

    //移动广告条到屏幕底部居中显示
    [DllImportAttribute("__Internal")]
    public static extern void moveBannerToBottom();

    //关闭广告条
    [DllImportAttribute("__Internal")]
    public static extern void closeBanner();


    //创建插屏广告
    [DllImportAttribute("__Internal")]
    public static extern void loadInsertAd(string appKey, string placementId);

    //判断插屏广告是否加载完成
    [DllImportAttribute("__Internal")]
    public static extern bool isInsertAdIsReady();

    //显示插屏广告
    [DllImportAttribute("__Internal")]
    public static extern void showInsertAd();

    //创建并显示展屏广告
    [DllImportAttribute("__Internal")]
    public static extern void loadAndShowSplashAD(string appKey, string placementId);
    #endregion
}
#endif