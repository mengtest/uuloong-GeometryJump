//
//  GDTInterstitialManger.m
//  Unity-iPhone
//
//  Created by hanJianXin on 16/9/2.
//
//

#import "GDTInterstitialManger.h"

//#define insertAppkey        @"2211675583"
//#define insertPlacementId   @"2030814134092814"

@implementation GDTInterstitialManger

static NSString *INTERSTITIAL_STATE_TEXT = @"插屏状态";


+ (GDTInterstitialManger *)sharedManger
{
    static GDTInterstitialManger *manger = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manger = [[self alloc] init];
    });
    return manger;
}

/**
 *  在适当的时候，初始化并调用loadAd方法进行预加载
 */

- (void)loadInsertAdWithAppkey:(NSString *)appKey placementId:(NSString *)placementId {
    _interstitialObj = [[GDTMobInterstitial alloc] initWithAppkey:appKey placementId:placementId];
    
    _interstitialObj.delegate = self; //设置委托
    _interstitialObj.isGpsOn = NO; //【可选】设置GPS开关
    //预加载广告
    [_interstitialObj loadAd];
}

//广告是否加载完成
- (BOOL)isADLoadReady{
    if (_interstitialObj.isReady) {
        NSLog(@"插屏广告加载完成");
    }else{
        NSLog(@"插屏广告加载还未完成...");
    }
    return _interstitialObj.isReady;
}

/**
 *  在适当的时候，调用presentFromRootViewController来展现插屏广告
 */
- (void)showInsertAd {
    UIViewController *vc = UnityGetGLViewController();
    // UIViewController *vc = [[[UIApplication sharedApplication] keyWindow] rootViewController];
    [_interstitialObj presentFromRootViewController:vc];
}

/**
 *  广告预加载成功回调
 *  详解:当接收服务器返回的广告数据成功后调用该函数
 */
- (void)interstitialSuccessToLoadAd:(GDTMobInterstitial *)interstitial
{
    NSLog(@"%@:%@",INTERSTITIAL_STATE_TEXT,@"Success Loaded.");
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","interstitialSuccessToLoadAd");
}

/**
 *  广告预加载失败回调
 *  详解:当接收服务器返回的广告数据失败后调用该函数
 */
- (void)interstitialFailToLoadAd:(GDTMobInterstitial *)interstitial error:(NSError *)error
{
    NSLog(@"%@:%@",INTERSTITIAL_STATE_TEXT,@"Fail Loaded." );
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","interstitialFailToLoadAd");
}

/**
 *  插屏广告将要展示回调
 *  详解: 插屏广告即将展示回调该函数
 */
- (void)interstitialWillPresentScreen:(GDTMobInterstitial *)interstitial
{
    NSLog(@"%@:%@",INTERSTITIAL_STATE_TEXT,@"Going to present.");
    //  UnitySendMessage("Cube", "MoveLeft","");
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","interstitialWillPresentScreen");
}

/**
 *  插屏广告视图展示成功回调
 *  详解: 插屏广告展示成功回调该函数
 */
- (void)interstitialDidPresentScreen:(GDTMobInterstitial *)interstitial
{
    NSLog(@"%@:%@",INTERSTITIAL_STATE_TEXT,@"Success Presented." );
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","interstitialDidPresentScreen");
}

/**
 *  插屏广告展示结束回调
 *  详解: 插屏广告展示结束回调该函数
 */
- (void)interstitialDidDismissScreen:(GDTMobInterstitial *)interstitial
{
    NSLog(@"%@:%@",INTERSTITIAL_STATE_TEXT,@"Finish Presented.");
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","interstitialDidDismissScreen");
}

/**
 *  应用进入后台时回调
 *  详解: 当点击下载应用时会调用系统程序打开，应用切换到后台
 */
- (void)interstitialApplicationWillEnterBackground:(GDTMobInterstitial *)interstitial
{
    NSLog(@"%@:%@",INTERSTITIAL_STATE_TEXT,@"Application enter background.");
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","interstitialApplicationWillEnterBackground");
}

/**
 *  插屏广告曝光时回调
 *  详解: 插屏广告曝光时回调
 */
-(void)interstitialWillExposure:(GDTMobInterstitial *)interstitial
{
    NSLog(@"%@:%@",INTERSTITIAL_STATE_TEXT,@"Exposured");
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","interstitialWillExposure");
}
/**
 *  插屏广告点击时回调
 *  详解: 插屏广告点击时回调
 */
-(void)interstitialClicked:(GDTMobInterstitial *)interstitial
{
    NSLog(@"%@:%@",INTERSTITIAL_STATE_TEXT,@"Clicked");
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","interstitialClicked");
}



@end
