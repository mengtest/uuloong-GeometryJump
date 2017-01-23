//
//  GDTBannerManger.m
//  Unity-iPhone
//
//  Created by hanJianXin on 16/9/2.
//
//

#import "GDTBannerManger.h"

@implementation GDTBannerManger


#define     C_ADADWidth        GDTMOB_AD_SUGGEST_SIZE_320x50.width
#define     C_ADHeight         GDTMOB_AD_SUGGEST_SIZE_320x50.height

#define     ScreenWidth         [[UIScreen mainScreen] bounds].size.width
#define     ScreenHeight        [[UIScreen mainScreen] bounds].size.height

+ (GDTBannerManger *)sharedManger
{
    static GDTBannerManger *manger = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manger = [[self alloc] init];
    });
    return manger;
}

//生成广告条
- (void)createBannerWithFrame:(CGRect)frame appKey:(NSString *)appKey placementId:(NSString *)placementId{
    if (_bannerView) {
        [_bannerView removeFromSuperview];
        _bannerView = nil;
    }
    _bannerView = [[GDTMobBannerView alloc] initWithFrame:frame
                                                       appkey:appKey
                                                  placementId:placementId];
}

//显示广告条
- (void)showBanner{
    if (!_bannerView) {
        return;
    }
    _bannerView.delegate = self; // 设置Delegate
    UIViewController *viewController = UnityGetGLViewController();
    _bannerView.currentViewController = viewController; //设置当前的ViewController
    //    _bannerView.currentViewController = self; //设置当前的ViewController
    _bannerView.interval = 30; //【可选】设置刷新频率;默认30秒
    _bannerView.isGpsOn = NO; //【可选】开启GPS定位;默认关闭
    _bannerView.showCloseBtn = NO; //【可选】展示关闭按钮;默认显示
    _bannerView.isAnimationOn = NO; //【可选】开启banner轮播和展现时的动画效果;默认开启
    UIView *unityView = UnityGetGLView();
    [unityView addSubview:_bannerView]; //添加到当前的view中
    [_bannerView loadAdAndShow]; //加载广告并展示
}

- (void)moveBannerToBottom{
    if (!_bannerView) {
        return;
    }
    CGRect rect = _bannerView.frame;
    _bannerView.frame = CGRectMake((ScreenWidth-rect.size.width)/2.0, ScreenHeight-rect.size.height, rect.size.width, rect.size.height);
}

//生成并显示广告条
- (void)createBannerAndShowWithFrame:(CGRect)frame appKey:(NSString *)appKey placementId:(NSString *)placementId{
    [self createBannerWithFrame:frame appKey:appKey placementId:placementId];
    [self showBanner];
}

//关闭广告条
- (void)closeBanner{
    if (_bannerView) {
        [_bannerView removeFromSuperview];
        _bannerView = nil;
    }
}

- (void)bannerViewMemoryWarning
{
    NSLog(@"%s",__FUNCTION__);
}

// 请求广告条数据成功后调用
//
// 详解:当接收服务器返回的广告数据成功后调用该函数
- (void)bannerViewDidReceived
{
    NSLog(@"%s",__FUNCTION__);
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","bannerViewDidReceived");
}

// 请求广告条数据失败后调用
//
// 详解:当接收服务器返回的广告数据失败后调用该函数
- (void)bannerViewFailToReceived:(NSError *)error
{
    NSLog(@"%s, Error:%@",__FUNCTION__,error);
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","bannerViewFailToReceived");
}

// 应用进入后台时调用
//
// 详解:当点击下载或者地图类型广告时，会调用系统程序打开，
// 应用将被自动切换到后台
- (void)bannerViewWillLeaveApplication
{
    NSLog(@"%s",__FUNCTION__);
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","bannerViewWillLeaveApplication");
}

// banner条曝光回调
//
// 详解:banner条曝光时回调该方法
- (void)bannerViewWillExposure
{
    NSLog(@"%s",__FUNCTION__);
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","bannerViewWillExposure");
}

// banner条点击回调
//
// 详解:banner条被点击时回调该方法
- (void)bannerViewClicked
{
    NSLog(@"%s",__FUNCTION__);
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","bannerViewClicked");
}

/**
 *  banner条被用户关闭时调用
 *  详解:当打开showCloseBtn开关时，用户有可能点击关闭按钮从而把广告条关闭
 */
- (void)bannerViewWillClose
{
    NSLog(@"%s",__FUNCTION__);
    UnitySendMessage("TecentGDTAds", "onTecentGDTEventReceived","bannerViewWillClose");
}



@end
