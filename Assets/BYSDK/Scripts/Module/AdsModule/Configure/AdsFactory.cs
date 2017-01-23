using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BYSDKManage;
public class AdsFactory {
    /// <summary>
    /// 根据广告类型生成广告实例
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    public static InterstitialAds CreateInterstitialAd(Vendors _type) {
        InterstitialAds retAds = null;
        switch (_type) {
        case Vendors.GOOGLE:
            retAds = new GoogleInterstitialAds(AdsConfig.GOOGLE_INTER);
            break;
        case Vendors.GOOGLEP:
            retAds = new GoogleInterstitialAds(AdsConfig.GOOGLE_INTER_PICTURE);
            break;
        case Vendors.APPLOVIN:
            retAds = new AppLovInInterstitial();
            break;
        case Vendors.CHARTBOOST:
            retAds = new ChartboostInterstitialAds();
            break;
        case Vendors.TENCENT:
            retAds = new TencentInterstitialAds(AdsConfig.TENCENT_APPID,AdsConfig.TENCENT_INTERSTITIAL);
            break;
        case Vendors.BAIDU:
            retAds =  new BaiduInterstitialAds(AdsConfig.BAIDU_APPID, AdsConfig.BAIDU_INTERSTITIAL);
            break;
        case Vendors.ADBUDDIZ:
            retAds = new AdBuddizInterstitial(AdsConfig.ADBUDDIZ_ANDROID,AdsConfig.ADBUDDIZ_IOS);
            break;
        case Vendors.FACEBOOK:
            retAds = new FacebookInterstitialAds(ShareManage._instance, AdsConfig.FB_INTERSTITIAL);
            break;
        case Vendors.INMOBI:
            retAds = new InMobiInterstitial();
            break;
        }
        if (null != retAds) {
            retAds.MType = _type;
        }
        return retAds;
    }


    /// <summary>
    /// 根据广告类型生成广告实例
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    public static AbstractVideo CreateVideoAds(Vendors _type) {
        AbstractVideo retAds = null;

        switch (_type) {
        case Vendors.GOOGLE:
            retAds = new GoogleVideo(AdsConfig.GOOGLE_VIDEO);
            break;
        case Vendors.APPLOVIN:
            retAds = new AppLovInVideo();
            break;
        case Vendors.CHARTBOOST:
            retAds = new ChartboostVideo();
            break;
        case Vendors.VUNGLE:
            retAds = new VungleVideo(AdsConfig.VUNGLE_ANDROID, AdsConfig.VUNGLE_IOS);
            break;
        case Vendors.UNITY:
            retAds = new UnityVideo(AdsConfig.UNITY_ADSID);
            break;
        case Vendors.ADBUDDIZ:
            retAds = new AdBuddizVideo(AdsConfig.ADBUDDIZ_ANDROID, AdsConfig.ADBUDDIZ_IOS);
            break;
        case Vendors.ADCOLONY:
            retAds = new AdColonyVideo(AdsConfig.ADCONLOY_VERSION, AdsConfig.ADCONLOY_APPID, AdsConfig.ADCONLOY_ZONEID);
            break;
        }
        if (null != retAds) {
            retAds.MType = _type;
        }
        return retAds;
    }


    /// <summary>
    ///  根据广告上类型进行创建Banner广告
    /// </summary>
    /// <param name="_type"> 广告商类型 </param>
    /// <returns></returns>
    public static BannerAds CreateBannerAds(Vendors _type) {

        BannerAds ret = null;

        switch (_type) {
        case Vendors.BAIDU:
            ret = new BaiduBanner(AdsConfig.BAIDU_APPID, AdsConfig.BAIDU_ABNNERID);
            break;
        case Vendors.FACEBOOK:
            ret = new FacebookBanner(ShareManage._instance, AdsConfig.FB_BANNER);
            break;
        case Vendors.GOOGLE:
            ret = new GoogleBannerAds();
            break;
        case Vendors.TENCENT:
            ret = new TencentBanner(AdsConfig.TENCENT_APPID, AdsConfig.TENCENT_BANNER);
            break;
        case Vendors.INMOBI:
            ret = new InMoBiBanner();
            break;
        }
        if (null != ret) {
            ret.MType = _type;
        }
        return ret;
    }

    /// <summary>
    ///  根据给定的广告类型列表生成广告序列
    /// </summary>
    /// <param name="_list">广告类型列表</param>
    /// <returns></returns>
    public static List<InterstitialAds> GetInterList(List<AdsData> _list) {
        List<InterstitialAds> ret = new List<InterstitialAds>();
        for (int i = 0; i < _list.Count; i++) {
            var data = (AdsData)_list[i];
            var temp = CreateInterstitialAd(data._type);
            if (null != temp)
                ret.Add(temp);
        }
        return ret;
    }

    /// <summary>
    ///  根据给定的广告类型列表生成广告序列
    /// </summary>
    /// <param name="_list">广告类型列表</param>
    /// <returns></returns>
    public static List<AbstractVideo> GetVideoList(List<AdsData> _list) {
        List<AbstractVideo> ret = new List<AbstractVideo>();
        for(int i = 0; i < _list.Count; i++) {
            var data = (AdsData)_list[i];
            var temp = CreateVideoAds(data._type);
            if (null != temp)
                ret.Add(temp);
        }
        return ret;
    }

/// <summary>
///  根据给定的广告类型列表生成广告序列
/// </summary>
/// <param name="_list">广告类型列表</param>
/// <returns></returns>
    public static List<BannerAds> GetBannerList(Hashtable _list) {
        List<BannerAds> ret = new List<BannerAds>();
        foreach (string key in _list.Keys) {
            var data = (AdsData)_list[key];
            var temp = CreateBannerAds(data._type);
            if (null != temp)
                ret.Add(temp);
        }
        return ret;
    }
}
