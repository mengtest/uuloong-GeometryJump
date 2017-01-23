using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class InMobiEventListener : MonoBehaviour {
#if UNITY_ANDROID
    void Start() {
        Destroy(this.gameObject);
    }
#endif
#if UNITY_IPHONE
    void OnEnable() {
        // Listen to all events for illustration purposes
        InMobiManager.interstitialDidReceiveAdEvent += interstitialDidReceiveAdEvent;
        InMobiManager.interstitialDidFailToReceiveAdWithErrorEvent += interstitialDidFailToReceiveAdWithErrorEvent;
        InMobiManager.interstitialWillPresentScreenEvent += interstitialWillPresentScreenEvent;
        InMobiManager.interstitialDidPresentScreenEvent += interstitialDidPresentScreenEvent;
        InMobiManager.interstitialWillDismissScreenEvent += interstitialWillDismissScreenEvent;
        InMobiManager.interstitialDidDismissScreenEvent += interstitialDidDismissScreenEvent;
        InMobiManager.interstitialWillLeaveApplicationEvent += interstitialWillLeaveApplicationEvent;
        InMobiManager.interstitialDidInteractEvent += interstitialDidInteractEvent;
        InMobiManager.interstitialDidFailToPresentScreenWithErrorEvent += interstitialDidFailToPresentScreenWithErrorEvent;
        InMobiManager.interstitialRewardActionCompletedWithRewardsEvent += interstitialRewardActionCompletedWithRewardsEvent;

        InMobiManager.bannerDidReceiveAdEvent += bannerDidReceiveAdEvent;
        InMobiManager.bannerDidFailToReceiveAdWithErrorEvent += bannerDidFailToReceiveAdWithErrorEvent;
        InMobiManager.bannerDidInteractEvent += bannerDidInteractEvent;
        InMobiManager.bannerWillPresentScreenEvent += bannerWillPresentScreenEvent;
        InMobiManager.bannerDidPresentScreenEvent += bannerDidPresentScreenEvent;
        InMobiManager.bannerWillDismissScreenEvent += bannerWillDismissScreenEvent;
        InMobiManager.bannerDidDismissScreenEvent += bannerDidDismissScreenEvent;
        InMobiManager.bannerWillLeaveApplicationEvent += bannerWillLeaveApplicationEvent;
        InMobiManager.bannerRewardActionCompletedWithRewardsEvent += bannerRewardActionCompletedWithRewardsEvent;
    }


    void OnDisable() {
        // Remove all event handlers
        InMobiManager.interstitialDidReceiveAdEvent -= interstitialDidReceiveAdEvent;
        InMobiManager.interstitialDidFailToReceiveAdWithErrorEvent -= interstitialDidFailToReceiveAdWithErrorEvent;
        InMobiManager.interstitialWillPresentScreenEvent -= interstitialWillPresentScreenEvent;
        InMobiManager.interstitialDidPresentScreenEvent -= interstitialDidPresentScreenEvent;
        InMobiManager.interstitialWillDismissScreenEvent -= interstitialWillDismissScreenEvent;
        InMobiManager.interstitialDidDismissScreenEvent -= interstitialDidDismissScreenEvent;
        InMobiManager.interstitialWillLeaveApplicationEvent -= interstitialWillLeaveApplicationEvent;
        InMobiManager.interstitialDidInteractEvent -= interstitialDidInteractEvent;
        InMobiManager.interstitialDidFailToPresentScreenWithErrorEvent -= interstitialDidFailToPresentScreenWithErrorEvent;
        InMobiManager.interstitialRewardActionCompletedWithRewardsEvent -= interstitialRewardActionCompletedWithRewardsEvent;

        InMobiManager.bannerDidReceiveAdEvent -= bannerDidReceiveAdEvent;
        InMobiManager.bannerDidFailToReceiveAdWithErrorEvent -= bannerDidFailToReceiveAdWithErrorEvent;
        InMobiManager.bannerDidInteractEvent -= bannerDidInteractEvent;
        InMobiManager.bannerWillPresentScreenEvent -= bannerWillPresentScreenEvent;
        InMobiManager.bannerDidPresentScreenEvent -= bannerDidPresentScreenEvent;
        InMobiManager.bannerWillDismissScreenEvent -= bannerWillDismissScreenEvent;
        InMobiManager.bannerDidDismissScreenEvent -= bannerDidDismissScreenEvent;
        InMobiManager.bannerWillLeaveApplicationEvent -= bannerWillLeaveApplicationEvent;
        InMobiManager.bannerRewardActionCompletedWithRewardsEvent += bannerRewardActionCompletedWithRewardsEvent;
    }



    void interstitialDidReceiveAdEvent() {
        Debug.Log( "interstitialDidReceiveAdEvent" );
    }


    void interstitialDidFailToReceiveAdWithErrorEvent( string error ) {
        Debug.Log( "interstitialDidFailToReceiveAdWithErrorEvent: " + error );
    }


    void interstitialWillPresentScreenEvent() {
        Debug.Log( "interstitialWillPresentScreenEvent" );
    }

    void interstitialDidPresentScreenEvent() {
        Debug.Log( "interstitialDidPresentScreenEvent" );
    }

    void interstitialWillDismissScreenEvent() {
        Debug.Log( "interstitialWillDismissScreenEvent" );
    }


    void interstitialDidDismissScreenEvent() {
        Debug.Log( "interstitialDidDismissScreenEvent" );
    }


    void interstitialWillLeaveApplicationEvent() {
        Debug.Log( "interstitialWillLeaveApplicationEvent" );
    }


    void interstitialDidInteractEvent( Dictionary<string,object> param ) {
        Debug.Log( "interstitialDidInteractEvent" );
        Debug.Log (param);
    }


    void interstitialDidFailToPresentScreenWithErrorEvent( string error ) {
        Debug.Log( "interstitialDidFailToPresentScreenWithErrorEvent: " + error );
    }

    void interstitialRewardActionCompletedWithRewardsEvent( Dictionary<string,object> param ) {
        Debug.Log( "interstitialRewardActionCompletedWithRewardsEvent" );
        Debug.Log (param);
    }

    void bannerRewardActionCompletedWithRewardsEvent( Dictionary<string,object> param ) {
        Debug.Log( "bannerRewardActionCompletedWithRewardsEvent" );
        Debug.Log (param);
    }

    void bannerDidReceiveAdEvent() {
        Debug.Log( "bannerDidReceiveAdEvent" );
    }


    void bannerDidFailToReceiveAdWithErrorEvent( string error ) {
        Debug.Log( "bannerDidFailToReceiveAdWithErrorEvent: " + error );
    }


    void bannerDidInteractEvent( Dictionary<string,object> param ) {
        Debug.Log( "bannerDidInteractEvent" );
        Debug.Log (param);
    }


    void bannerWillPresentScreenEvent() {
        Debug.Log( "bannerWillPresentScreenEvent" );
    }

    void bannerDidPresentScreenEvent() {
        Debug.Log( "bannerDidPresentScreenEvent" );
    }

    void bannerWillDismissScreenEvent() {
        Debug.Log( "bannerWillDismissScreenEvent" );
    }


    void bannerDidDismissScreenEvent() {
        Debug.Log( "bannerDidDismissScreenEvent" );
    }


    void bannerWillLeaveApplicationEvent() {
        Debug.Log( "bannerWillLeaveApplicationEvent" );
    }

#endif
}


