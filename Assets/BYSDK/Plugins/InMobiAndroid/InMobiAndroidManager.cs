using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using InMobiMiniJSON;

public class InMobiAndroidManager : MonoBehaviour {
#if UNITY_ANDROID
    // Fired when a banner loads
    public static event Action onBannerRequestSucceededEvent;

    // Fired when a banner fails to load
    public static event Action<string> onBannerRequestFailedEvent;

    // Called when ad expands on top of the screen because of user action
    public static event Action onShowBannerScreenEvent;

    // Fired when a banners full screen action is dismissed
    public static event Action onDismissBannerScreenEvent;

    // Fired when a banner is interacted with. Includes the data payload.
    public static event Action<Dictionary<string,object>> onBannerInteractionEvent;

    // Fired when user leave the application.
    public static event Action onBannerLeaveApplicationEvent;

    // Fired when banner incent callbacks completed
    public static event Action<Dictionary<string,object>> onBannerAdRewardActionCompletedEvent;


    // Fired when a interstitial loads
    public static event Action onInterstitialLoadedEvent;

    // Fired when a banner fails to load
    public static event Action<string> onInterstitialFailedEvent;

    // Called when intersitial show method is called
    public static event Action onShowInterstitialScreenEvent;

    // Fired when a interstitial full screen action is dismissed
    public static event Action onDismissInterstitialScreenEvent;

    // Fired when a banner is interacted with. Includes the data payload.
    public static event Action<Dictionary<string,object>> onInterstitialInteractionEvent;

    // Fired when user leave the application.
    public static event Action onInterstitialLeaveApplicationEvent;

    // Fired when banner incent callbacks completed
    public static event Action<Dictionary<string,object>> onInterstitialAdRewardActionCompletedEvent;


    void Awake() {
        // Set the GameObject name to the class name for easy access from Obj-C
        gameObject.name = this.GetType().ToString();
        DontDestroyOnLoad( this );
    }

    public void onBannerRequestSucceeded( string empty ) {
        if( null != onBannerRequestSucceededEvent)
            onBannerRequestSucceededEvent ();
    }

    public void onBannerRequestFailed( string error ) {
        if( null != onBannerRequestFailedEvent)
            onBannerRequestFailedEvent ( error);
    }

    public void onShowBannerScreen( string empty ) {
        if( null != onShowBannerScreenEvent)
            onShowBannerScreenEvent ();
    }

    public void onDismissBannerScreen( string empty ) {
        if( null != onDismissBannerScreenEvent)
            onDismissBannerScreenEvent ();
    }

    public void onBannerInteraction( string json ) {
        if( null != onBannerInteractionEvent)
            onBannerInteractionEvent(( Dictionary<string,object>)Json.Deserialize(json) );
    }

    public void onBannerLeaveApplication( string empty ) {
        if( null != onBannerLeaveApplicationEvent)
            onBannerLeaveApplicationEvent();
    }

    public void onBannerAdRewardActionCompleted( string json ) {
        if( null != onBannerAdRewardActionCompletedEvent)
            onBannerAdRewardActionCompletedEvent(( Dictionary<string,object>)Json.Deserialize(json));
    }

    public void onInterstitialLoaded( string empty ) {
        if( null != onInterstitialLoadedEvent)
            onInterstitialLoadedEvent ();
    }

    public void onInterstitialFailed( string error ) {
        if( null != onInterstitialFailedEvent)
            onInterstitialFailedEvent (error);
    }

    public void onShowInterstitialScreen( string empty ) {
        if( null != onShowInterstitialScreenEvent)
            onShowInterstitialScreenEvent ();
    }

    public void onDismissInterstitialScreen( string empty ) {
        if( null != onDismissInterstitialScreenEvent)
            onDismissInterstitialScreenEvent ();
    }

    public void onInterstitialInteraction( string json ) {
        if( null != onInterstitialInteractionEvent)
            onInterstitialInteractionEvent(( Dictionary<string,object>)Json.Deserialize(json) );
    }

    public void onInterstitialLeaveApplication( string empty ) {
        if( null != onInterstitialLeaveApplicationEvent)
            onInterstitialLeaveApplicationEvent();
    }

    public void onInterstitialAdRewardActionCompleted( string json ) {
        if( null != onInterstitialAdRewardActionCompletedEvent)
            onInterstitialAdRewardActionCompletedEvent(( Dictionary<string,object>)Json.Deserialize(json));
    }


#endif
}