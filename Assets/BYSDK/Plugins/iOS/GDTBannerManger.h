//
//  GDTBannerManger.h
//  Unity-iPhone
//
//  Created by hanJianXin on 16/9/2.
//
//

#import <Foundation/Foundation.h>

#import "GDTMobBannerView.h" //导入GDTMobBannerView.h头文件

@interface GDTBannerManger : NSObject<GDTMobBannerViewDelegate>{
        GDTMobBannerView *_bannerView;//声明一个GDTMobBannerView的实例
}

//单例生产
+ (GDTBannerManger *)sharedManger;
//生成广告条
- (void)createBannerWithFrame:(CGRect)frame appKey:(NSString *)appKey placementId:(NSString *)placementId;
//显示广告条
- (void)showBanner;
//移动广告条到屏幕底部居中显示
- (void)moveBannerToBottom;
//生成并显示广告条
- (void)createBannerAndShowWithFrame:(CGRect)frame appKey:(NSString *)appKey placementId:(NSString *)placementId;
//关闭广告条
- (void)closeBanner;

@end
