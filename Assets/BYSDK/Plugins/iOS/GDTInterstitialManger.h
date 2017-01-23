//
//  GDTInterstitialManger.h
//  Unity-iPhone
//
//  Created by hanJianXin on 16/9/2.
//
//

#import <Foundation/Foundation.h>
#import "GDTMobInterstitial.h"

@interface GDTInterstitialManger : NSObject<GDTMobInterstitialDelegate>{
    GDTMobInterstitial *_interstitialObj;
}

//获取单例
+ (GDTInterstitialManger *)sharedManger;
//加载广告
- (void)loadInsertAdWithAppkey:(NSString *)appKey placementId:(NSString *)placementId;
//广告是否加载完成
- (BOOL)isADLoadReady;
//显示广告
- (void)showInsertAd;

@end
