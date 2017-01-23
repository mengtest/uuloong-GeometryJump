#import <Foundation/Foundation.h>
#import "UnityAppController.h"
#import "MyViewController.h"
#import "GDTBannerManger.h"
#import "GDTInterstitialManger.h"
#import "GDTSplashManager.h"


/*

说明： 导入本包后，需要在XCode的工程中再导入libz.tbd

*/


#define     C_ADADWidth        GDTMOB_AD_SUGGEST_SIZE_320x50.width
#define     C_ADHeight         GDTMOB_AD_SUGGEST_SIZE_320x50.height

//测试用过的一个方法
extern "C" int OpenWebView(const char *url){
    //获取Unity rootviewcontroller
    UIViewController *unityRootVC = UnityGetGLViewController();
    UIView *unityView = UnityGetGLView();
    //创建我们需要打开的webview
    MyViewController *webVC = [[MyViewController alloc]init];
    //传入需要打开的网址
    webVC.url = [NSString stringWithUTF8String:url];
    //添加一个自定义导航视图把将要打开的webview作为根视图
    UINavigationController *navVC = [[UINavigationController alloc]initWithRootViewController:webVC];
    //添加到Unity场景的rootview
    [unityRootVC addChildViewController:navVC];
    [unityView addSubview:navVC.view];
    return 0;
}


//广告条方法的C语言封装
extern "C"{

        //生成广告条
    void createBanner(int x, int y,float width, float height, const char* appKey, const char *placementId){
        CGRect rect = [[UIScreen mainScreen] bounds];
        CGSize size = rect.size;
        CGFloat swidth = size.width;
        NSInteger dreamHeight = (NSInteger)(100.0/1334.0 * size.height);
        NSInteger bannerHeight = dreamHeight;
        if (dreamHeight <= 50) {
            bannerHeight = 50;
        }else if (dreamHeight > 50 && dreamHeight <=60) {
            bannerHeight = 60;
        } else if (dreamHeight > 60) {
            bannerHeight = 90;
        }
        
        CGRect frame = CGRectMake(0, size.height - bannerHeight, swidth, bannerHeight);
        
        GDTBannerManger *manger = [GDTBannerManger sharedManger];
        [manger createBannerWithFrame:frame
                               appKey:[NSString stringWithUTF8String:appKey]
                          placementId:[NSString stringWithUTF8String:placementId]];
    }

    
    //显示广告条
    void showBanner(){
        GDTBannerManger *manger = [GDTBannerManger sharedManger];
        [manger showBanner];
    }
    
    void moveBannerToBottom(){
        GDTBannerManger *manger = [GDTBannerManger sharedManger];
        [manger moveBannerToBottom];
    }
    
    //生成并显示广告条
    void createBannerAndShow(int x, int y,float width, float height, const char* appKey, const char *placementId){
        GDTBannerManger *manger = [GDTBannerManger sharedManger];
        [manger createBannerAndShowWithFrame:CGRectMake(x, y, width, height)
                                      appKey:[NSString stringWithUTF8String:appKey]
                                 placementId:[NSString stringWithUTF8String:placementId]];
    }
    
    //关闭广告条
    void closeBanner(){
        GDTBannerManger *manger = [GDTBannerManger sharedManger];
        [manger closeBanner];
    }
    
}

//插屏广告方法的C语言封装
extern "C"{
    //加载插屏广告
    void loadInsertAd(const char* appKey, const char *placementId){
        GDTInterstitialManger *manger = [GDTInterstitialManger sharedManger];
        [manger loadInsertAdWithAppkey:[NSString stringWithUTF8String:appKey]
                           placementId:[NSString stringWithUTF8String:placementId]];
    }
    
    //插屏广告是否加载完成
    BOOL isInsertAdIsReady(){
        GDTInterstitialManger *manger = [GDTInterstitialManger sharedManger];
        return [manger isADLoadReady];
    }
    
    //显示插屏广告
    void showInsertAd(){
        GDTInterstitialManger *manger = [GDTInterstitialManger sharedManger];
        [manger showInsertAd];
    }
    
}


//展屏广告方法的C语言封装
extern "C"{
    //加载并显示展评广告
    void loadAndShowSplashAD(const char* appKey, const char *placementId){
        GDTSplashManager *manger = [GDTSplashManager sharedManger];
        [manger loadAndShowADWithAppkey:[NSString stringWithUTF8String:appKey]
                            placementId:[NSString stringWithUTF8String:placementId]];
    }
}




