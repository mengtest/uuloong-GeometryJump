using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_ANDROID
class InMoBiAndroid : InMoBiAds {

    public InMoBiAndroid(string accID) : base(accID) {
        InMobiAndroid.init(accontID, null);
        InMobiAndroid.setLogLevel(InMobiLogLevel.Verbose);
    }

    /// <summary>
    /// 加载静态插页广告
    /// </summary>
    public override void LoadInterstitial() {
        Debug.Log("iii " + InterstitialID + " " + InterstitialKey);
        InMobiAndroid.loadInterstitial(InterstitialID, InterstitialKey);
    }

    /// <summary>
    /// 判断插页广告是否就绪
    /// </summary>
    /// <returns>true 插页广告已经就绪，false 插页广告未就绪</returns>
    public override bool IsInterstitialReady() {
        return InMobiAndroid.getInterstitialState(InterstitialKey);
    }

    /// <summary>
    /// 显示插页广告
    /// </summary>
    public override void ShowInterstitial() {
        InMobiAndroid.showInterstitial(InterstitialKey);
    }


    /// <summary>
    /// 创建Banner对象
    /// </summary>
    public override void CreateBanner() {
        int width = Screen.width;

        Debug.Log("bbb " + BannerID + " " + BannerKey);
        InMobiAndroid.createBanner(BannerID, BannerKey, InMobiAdPosition.BottomRight, width, 50, 30);
    }


    /// <summary>
    ///  加载Banner广告并显示
    /// </summary>
    public override void LoadBanner() {
        InMobiAndroid.loadBanner(BannerKey);
    }

    /// <summary>
    /// 销毁Banner广告对象
    /// </summary>
    public override void DestroyBanner() {
        InMobiAndroid.destroyBanner(BannerKey);
    }

    /// <summary>
    /// 加载视屏广告
    /// </summary>
    public override void LoadVideo() {
        InMobiAndroid.loadInterstitial(VideoID,VideoKey);
    }

    /// <summary>
    /// 检测视屏广告是否加载完毕
    /// </summary>
    /// <returns></returns>
    public override bool IsVideoReady() {
        return InMobiAndroid.getInterstitialState(VideoKey);
    }

    /// <summary>
    /// 显示视屏广告
    /// </summary>
    public override void ShowVideo() {
        InMobiAndroid.showInterstitial(VideoKey);
    }
}

#endif