using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using InMobiMiniJSON;



#if UNITY_IPHONE
	
public enum InMobiAdPosition
{
	TopLeft,
	TopCenter,
	TopRight,
	Centered,
	BottomLeft,
	BottomCenter,
	BottomRight
}

public enum InMobiLogLevel
{
    None      = 0,
	Error	  = 1,
    Debug     = 2
}

public enum InMobiError
{
	NETWORK_UNREACHABLE,
	NO_FILL,
	REQUEST_INVALID,
	REQUEST_PENDING ,
	REQUEST_TIMED_OUT,
	INTERNAL_ERROR,
	SERVER_ERROR
}


public class InMobiBinding
{
	[DllImport("__Internal")]
	private static extern void _inMobiInit(string accountID,string requestParams = null );

	// Initilized the InMobi plugin. This should be called before any other methods!
	public static void initialize(string accountID,Dictionary<string,string> requestParams = null )
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_inMobiInit(accountID, requestParams != null ? Json.Serialize(requestParams) : string.Empty );
	}


	[DllImport("__Internal")]
	private static extern void _inMobiSetLogLevel( int level );

	// Sets the log level for the InMobi SDK
	public static void setLogLevel( InMobiLogLevel logLevel )
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_inMobiSetLogLevel( (int)logLevel );
	}
	

	#region Banners

	[DllImport("__Internal")]
	private static extern void _inMobiCreateBanner( string bannerParams, int width,  int height, long placementId );

	// Creates a banner
	public static void createBanner( Dictionary<string,string> bannerParams, int width,  int height, long placementId )
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_inMobiCreateBanner( Json.Serialize(bannerParams), width, height, placementId );
	}


	[DllImport("__Internal")]
	private static extern void _inMobiLoadBanner(string bannerKey);

	// Reloads the banner. Used when refreshInterval is switched off
	public static void loadBanner(string bannerKey)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_inMobiLoadBanner(bannerKey);
	}

	

	[DllImport("__Internal")]
	private static extern void _inMobiDestroyBanner(string bannerKey);

	// Destroys the banner
	public static void destroyBanner(string bannerKey)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_inMobiDestroyBanner(bannerKey);
	}

	#endregion


	#region Interstitials

	[DllImport("__Internal")]
	private static extern void _inMobiLoadInterstitial(string interstitialKey,long placementId);

	// Preloads an action
	public static void loadInterstitial(string interstitialKey, long placementId)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_inMobiLoadInterstitial(interstitialKey,placementId);
	}

	[DllImport("__Internal")]
	private static extern bool _inMobiIsInterstitialReady(string interstitialKey);
	
	// Preloads an action
	public static bool isInterstitialReady(string interstitialKey)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			return _inMobiIsInterstitialReady(interstitialKey);
		return false;
	}
	
	[DllImport("__Internal")]
	private static extern void _inMobiPresentInterstitial(string interstititalKey);

	// Presents the action if it is loaded and ready
	public static void presentInterstitial(string interstitialKey)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_inMobiPresentInterstitial(interstitialKey);
	}

	#endregion
}

#endif