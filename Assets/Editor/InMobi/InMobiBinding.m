//
//  InMobiBinding.m
//  InMobiTest
//

#import "InMobiManager.h"
#import "IMSdk.h"


// Converts NSString to C style string by way of copy (Mono will free it)
#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

// Converts C style string to NSString as long as it isnt empty
#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

#define ParseJSON( _x_ ) [InMobiManager dictFromJSON:GetStringParamOrNil( _x_ )]



void _inMobiInit(const char* accountID, const char * requestParams)
{
    [[InMobiManager sharedManger] inMobiInit:GetStringParam(accountID) requestParams:ParseJSON( requestParams )];
}


void _inMobiSetLogLevel( int level )
{
	[IMSdk setLogLevel:level];
}


// Banners

void _inMobiCreateBanner( const char* bannerParams, int width, int height, long long placementId)
{
    [[InMobiManager sharedManger] createBanner:ParseJSON(bannerParams) width:width height:height placementID:placementId];
}

void _inMobiLoadBanner(const char* bannerKey)
{
    [[InMobiManager sharedManger] loadBanner:GetStringParam(bannerKey)];
}


void _inMobiDestroyBanner(const char* bannerKey)
{
	[[InMobiManager sharedManger] destroyBanner:GetStringParam(bannerKey)];
}



// Actions (interstitials)
void _inMobiLoadInterstitial(const char* interstitialKey,long long placementId)
{
    [[InMobiManager sharedManger] loadInterstitial:GetStringParam(interstitialKey) placementId:(long long)placementId];
}

bool _inMobiIsInterstitialReady(const char* interstitialKey)
{
    return [[InMobiManager sharedManger] isInterstitialReady:GetStringParam(interstitialKey)];
}


void _inMobiPresentInterstitial(const char* interstitialKey)
{
    [[InMobiManager sharedManger] presentInterstitial:GetStringParam(interstitialKey)];
}

