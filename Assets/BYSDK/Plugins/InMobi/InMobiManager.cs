using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using InMobiMiniJSON;


#if UNITY_IPHONE
public class InMobiManager : MonoBehaviour {
    // Fired when an interstitial ad is loaded
    public static event Action interstitialDidReceiveAdEvent;

    // Fired when an interstitial ad fails to load
    public static event Action<string> interstitialDidFailToReceiveAdWithErrorEvent;

    //
    public static event Action interstitialWillPresentScreenEvent;

    //
    public static event Action interstitialDidPresentScreenEvent;

    //
    public static event Action interstitialWillDismissScreenEvent;

    // Fired when an interstitial ad dismisses
    public static event Action interstitialDidDismissScreenEvent;

    // Fired when an interstitial ad causes control to leave the application
    public static event Action interstitialWillLeaveApplicationEvent;

    // Fired when a user interacts with an interstitial
    public static event Action<Dictionary<string,object>> interstitialDidInteractEvent;

    // Fired when an interstitial ad fails to present itself
    public static event Action<string> interstitialDidFailToPresentScreenWithErrorEvent;

    // Fired when an incentivised ad is complete
    public static event Action<Dictionary<string,object>> interstitialRewardActionCompletedWithRewardsEvent;

    // Fired when a banner receives an ad
    public static event Action bannerDidReceiveAdEvent;

    // Fired when a banner fails to receive an ad
    public static event Action<string> bannerDidFailToReceiveAdWithErrorEvent;

    // Fired when a user interacts with a banner
    public static event Action<Dictionary<string,object>> bannerDidInteractEvent;

    // Fired when banner would be presenting a full screen content
    public static event Action bannerWillPresentScreenEvent;

    // Fired when banner has finished presenting screen
    public static event Action bannerDidPresentScreenEvent;

    // Fired when banner will start dismissing the presented screen
    public static event Action bannerWillDismissScreenEvent;

    // Fired when a banner dismisses itself
    public static event Action bannerDidDismissScreenEvent;

    // Fired when a banner causes control to leave the application
    public static event Action bannerWillLeaveApplicationEvent;

    // Fired when an incentivised ad is complete
    public static event Action<Dictionary<string,object>> bannerRewardActionCompletedWithRewardsEvent;

    void Awake() {
        // Set the GameObject name to the class name for easy access from Obj-C
        gameObject.name = this.GetType().ToString();
        DontDestroyOnLoad( this );
    }

    public void interstitialDidReceiveAd( string empty ) {
        if( null != interstitialDidReceiveAdEvent )
            interstitialDidReceiveAdEvent();
    }

    public void interstitialDidFailToReceiveAdWithError( string error ) {
        if (null != interstitialDidFailToReceiveAdWithErrorEvent)
            interstitialDidFailToReceiveAdWithErrorEvent( error );
    }

    public void interstitialWillPresentScreen( string empty ) {
        if (null != interstitialWillPresentScreenEvent)
            interstitialWillPresentScreenEvent();
    }

    public void interstitialDidPresentScreen( string empty ) {
        if (null != interstitialDidPresentScreenEvent)
            interstitialDidPresentScreenEvent();
    }

    public void interstitialWillDismissScreen( string empty ) {
        if (null != interstitialWillDismissScreenEvent)
            interstitialWillDismissScreenEvent();
    }

    public void interstitialDidDismissScreen( string empty ) {
        if (null != interstitialDidDismissScreenEvent)
            interstitialDidDismissScreenEvent();
    }

    public void interstitialWillLeaveApplication( string empty ) {
        if (null != interstitialWillLeaveApplicationEvent)
            interstitialWillLeaveApplicationEvent();
    }

    public void interstitialDidInteract( string json ) {
        if (null != interstitialDidInteractEvent)
            interstitialDidInteractEvent( (Dictionary<string,object>)Json.Deserialize(json));
    }

    public void interstitialDidFailToPresentScreenWithError( string error ) {
        if (null != interstitialDidFailToPresentScreenWithErrorEvent)
            interstitialDidFailToPresentScreenWithErrorEvent( error );
    }

    public void interstitialRewardActionCompletedWithRewards( string json ) {
        if (null != interstitialRewardActionCompletedWithRewardsEvent)
            interstitialRewardActionCompletedWithRewardsEvent((Dictionary<string,object>)Json.Deserialize(json));
    }

    public void bannerDidReceiveAd( string empty ) {
        if (null != bannerDidReceiveAdEvent)
            bannerDidReceiveAdEvent();
    }

    public void bannerDidFailToReceiveAdWithError( string error ) {
        if (null != bannerDidFailToReceiveAdWithErrorEvent)
            bannerDidFailToReceiveAdWithErrorEvent( error );
    }

    public void bannerDidInteract( string json ) {
        if (null != bannerDidInteractEvent)
            bannerDidInteractEvent( (Dictionary<string,object>)Json.Deserialize(json) );
    }

    public void bannerWillPresentScreen( string empty ) {
        if (null != bannerWillPresentScreenEvent)
            bannerWillPresentScreenEvent();
    }

    public void bannerDidPresentScreen( string empty ) {
        if (null != bannerDidPresentScreenEvent)
            bannerDidPresentScreenEvent();
    }

    public void bannerWillDismissScreen( string empty ) {
        if (null != bannerWillDismissScreenEvent)
            bannerWillDismissScreenEvent();
    }

    public void bannerDidDismissScreen( string empty ) {
        if (null != bannerDidDismissScreenEvent)
            bannerDidDismissScreenEvent();
    }

    public void bannerWillLeaveApplication( string empty ) {
        if (null != bannerWillLeaveApplicationEvent)
            bannerWillLeaveApplicationEvent();
    }

    public void bannerRewardActionCompletedWithRewards( string json ) {
        if (null != bannerRewardActionCompletedWithRewardsEvent)
            bannerRewardActionCompletedWithRewardsEvent( (Dictionary<string,object>)Json.Deserialize(json) );
    }
}
#endif

