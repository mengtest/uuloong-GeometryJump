namespace bysdk
{
    public class DomobEvent
    {
        #region Banner
        public static readonly string onAdOverlayPresented = "onAdOverlayPresented";
        public static readonly string onAdOverlayDismissed = "onAdOverlayDismissed";
        public static readonly string onAdClicked = "onAdClicked";
        public static readonly string onLeaveApplication = "onLeaveApplication";
        public static readonly string onAdFailed = "onAdFailed";
        public static readonly string onEventAdReturned = "onEventAdReturned";

        #endregion

        #region Interstitial
        public static readonly string onLandingPageOpen = "onLandingPageOpen";
        public static readonly string onLandingPageClose = "onLandingPageClose";
        public static readonly string onInterstitialAdReady = "onInterstitialAdReady";
        public static readonly string onInterstitialAdPresent = "onInterstitialAdPresent";
        public static readonly string onInterstitialAdLeaveApplication = "onInterstitialAdLeaveApplication";
        public static readonly string onInterstitialAdFailed = "onInterstitialAdFailed";
        public static readonly string onInterstitialAdDismiss = "onInterstitialAdDismiss";
        public static readonly string onInterstitialAdClicked = "onInterstitialAdClicked";
        #endregion
    }

    // The event of baidu ad
    public class BaiduEvent
    {
        public static readonly string onAdReady = "onAdReady";
        public static readonly string onAdSwitch = "onAdSwitch";
        public static readonly string onAdShow = "onAdShow";

        public static readonly string onAdFailed = "onAdFailed";

        public static readonly string onAdClick = "onAdClick";
        public static readonly string onAdDismissed = "onAdDismissed";
        public static readonly string onAdPresent = "onAdPresent";
    }

    // The event of baidu ad
    public class TecentEvent
    {
        public static readonly string onADReceiv = "onADReceiv";
        public static readonly string onNoAD = "onNoAD";
        public static readonly string onADClicked = "onADClicked";
        public static readonly string onADClosed = "onADClosed";
        public static readonly string onADCloseOverlay = "onADCloseOverlay";
        public static readonly string onADExposure = "onADExposure";
        public static readonly string onADLeftApplication = "onADLeftApplication";
        public static readonly string onADOpenOverlay = "onADOpenOverlay";

        public static readonly string onADOpened = "onADOpened";
    }

    public class MobiSageEvent
    {
        #region Banner
        public static readonly string onMobiSageBannerSuccess = "onMobiSageBannerSuccess";
        public static readonly string onMobiSageBannerShow = "onMobiSageBannerShow";
        public static readonly string onMobiSageBannerError = "onMobiSageBannerError";
        public static readonly string onMobiSageBannerClick = "onMobiSageBannerClick";
        #endregion

        #region Interstitial
        public static readonly string onMobiSagePosterPreShow = "onMobiSagePosterPreShow";
        public static readonly string onMobiSagePosterError = "onMobiSagePosterError";
        public static readonly string onMobiSagePosterClose = "onMobiSagePosterClose";
        public static readonly string onMobiSagePosterClick = "onMobiSagePosterClick";
        #endregion
    }
}
