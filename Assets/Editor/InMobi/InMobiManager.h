//
//  InMobiManager.h
//  InMobiTest
//


#import <Foundation/Foundation.h>
#import "IMInterstitial.h"
#import "IMBanner.h"

typedef enum
{
	InMobiAdPositionTopLeft,
	InMobiAdPositionTopCenter,
	InMobiAdPositionTopRight,
	InMobiAdPositionCentered,
	InMobiAdPositionBottomLeft,
	InMobiAdPositionBottomCenter,
	InMobiAdPositionBottomRight
} InMobiAdPosition;



@interface InMobiManager : NSObject <IMInterstitialDelegate, IMBannerDelegate>
{
	InMobiAdPosition bannerPosition;
    UIInterfaceOrientation requestOrientation, orientationChange;
}

@property (nonatomic, retain) IMBanner *adView;
@property (nonatomic, retain) IMInterstitial *interstitialVisible;
@property (nonatomic, retain) NSMutableDictionary *interstitialDict;
@property (nonatomic, retain) NSMutableDictionary *bannerDict;
@property (nonatomic, strong) NSString* nativeContent;


+ (InMobiManager*)sharedManger;

+ (NSDictionary*)dictFromJSON:(NSString*)json;

+ (NSString*)JSONfromObject:(id)object;


//Initialize
- (void)inMobiInit:accountID requestParams:(NSDictionary *)requestParams;

// Interstitials
- (void)loadInterstitial:(NSString*)interstitialKey placementId:(long long)placementId;

- (bool)isInterstitialReady:(NSString*)interstitialKey;

- (void)presentInterstitial:(NSString*)interstitialKey;



// Banners

- (void)createBanner:(NSDictionary *)bannerParams width:(int)width height:(int)height placementID:(long long)placementId;

- (void)loadBanner:(NSString*)bannerKey;

- (void)destroyBanner:(NSString*)bannerKey;

@end
