using UnityEngine;
using System.Collections;

public class TecentGDTEvents  {

    #region Banner events
    public const string BannerViewDidReceived = "bannerViewDidReceived";
    public const string BannerViewFailToReceived = "bannerViewFailToReceived";
    public const string BannerViewWillLeaveApplication = "bannerViewWillLeaveApplication";
    public const string BannerViewWillExposure = "bannerViewWillExposure";
    public const string BannerViewClicked = "bannerViewClicked";
    public const string BannerViewWillClose = "bannerViewWillClose";
    #endregion

    #region Inter events
    public const string InterstitialSuccessToLoadAd = "interstitialSuccessToLoadAd";
    public const string InterstitialFailToLoadAd = "interstitialFailToLoadAd";
    public const string InterstitialWillPresentScreen = "interstitialWillPresentScreen";
    public const string InterstitialDidPresentScreen = "interstitialDidPresentScreen";
    public const string InterstitialDidDismissScreen = "interstitialDidDismissScreen";
    public const string InterstitialApplicationWillEnterBackground = "interstitialApplicationWillEnterBackground";
    public const string InterstitialWillExposure = "interstitialWillExposure";
    public const string InterstitialClicked = "interstitialClicked";
    #endregion
}
