using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_IPHONE
class InMoBiIOS : InMoBiAds {

    public InMoBiIOS(string accID) : base(accID) {
        var dict = new Dictionary<string, string>();
        InMobiBinding.initialize(accontID, dict);
        InMobiBinding.setLogLevel(InMobiLogLevel.Debug);
    }

    /// <summary>
    /// 加载静态插页广告
    /// </summary>
    public override void LoadInterstitial() {
        InMobiBinding.loadInterstitial(InterstitialKey, InterstitialID);
    }

    /// <summary>
    /// 判断插页广告是否就绪
    /// </summary>
    /// <returns>true 插页广告已经就绪，false 插页广告未就绪</returns>
    public override bool IsInterstitialReady() {
        return InMobiBinding.isInterstitialReady(InterstitialKey);
    }

    /// <summary>
    /// 显示插页广告
    /// </summary>
    public override void ShowInterstitial() {
        InMobiBinding.presentInterstitial(InterstitialKey);
    }


    /// <summary>
    /// 创建Banner对象
    /// </summary>
    public override void CreateBanner() {
        var bannerParams = new Dictionary<string, string>();
        bannerParams.Add("bannerKey", BannerKey);
        bannerParams.Add("position", ((int)InMobiAdPosition.BottomRight).ToString());
        int width = (int) AudienceNetwork.Utility.AdUtility.convert(Screen.width);
        InMobiBinding.createBanner(bannerParams, width, AdsConfig.BANNER_HEIGHT, BannerID);
    }


    /// <summary>
    ///  加载Banner广告并显示
    /// </summary>
    public override void LoadBanner() {
        InMobiBinding.loadBanner(BannerKey);
    }

    /// <summary>
    /// 销毁Banner广告对象
    /// </summary>
    public override void DestroyBanner() {
        InMobiBinding.destroyBanner(BannerKey);
    }

    /// <summary>
    /// 加载视屏广告
    /// </summary>
    public override void LoadVideo() {
        InMobiBinding.loadInterstitial(VideoKey, VideoID);
    }

    /// <summary>
    /// 检测视屏广告是否加载完毕
    /// </summary>
    /// <returns></returns>
    public override bool IsVideoReady() {
        return InMobiBinding.isInterstitialReady(VideoKey);
    }

    /// <summary>
    /// 显示视屏广告
    /// </summary>
    public override void ShowVideo() {
        InMobiBinding.presentInterstitial(VideoKey);
    }
}

#endif