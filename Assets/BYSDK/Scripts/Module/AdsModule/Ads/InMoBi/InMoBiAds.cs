using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class InMoBiAds :Object {

    /// <summary>
    /// InMoBi 账号
    /// </summary>
    public string accontID;

    /// <summary>
    ///  静态插页广告属性
    /// </summary>
    public string InterstitialKey;

    /// <summary>
    /// 静态插页广告ID《位置ID》
    /// </summary>
    public long InterstitialID;

    /// <summary>
    /// Banner广告属性
    /// </summary>
    public string BannerKey;

    /// <summary>
    /// Banner广告ID
    /// </summary>
    public long BannerID;

    /// <summary>
    ///  视屏广告属性
    /// </summary>
    public string VideoKey;

    /// <summary>
    /// 视屏广告ID
    /// </summary>
    public long VideoID;

    public InMoBiAds(string accID) {
        accontID = accID;
    }


    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="Params"> 4 个元素 </param>
    public virtual void init(Dictionary<string, string> Params) {
        try {
            if (null == Params || Params.Count != 6)
                throw new System.Exception("[BYSDK ] Class " + this.name + " init value count error!,Please check");
            if (Params.ContainsKey(AdsConfig.INMOBI_INSTERTITIAL_KEY)) {
                InterstitialKey = Params[AdsConfig.INMOBI_INSTERTITIAL_KEY];
            }
            if (Params.ContainsKey(AdsConfig.INMOBI_INSTERTITIAL_ID)) {
                InterstitialID = long.Parse(Params[AdsConfig.INMOBI_INSTERTITIAL_ID]);
            }
            if (Params.ContainsKey(AdsConfig.INMOBI_BANNER_KEY)) {
                BannerKey = Params[AdsConfig.INMOBI_BANNER_KEY];
            }
            if (Params.ContainsKey(AdsConfig.INMOBI_BANNER_ID)) {
                BannerID = long.Parse(Params[AdsConfig.INMOBI_BANNER_ID]);
            }

            if (Params.ContainsKey(AdsConfig.INMOBI_VIDEO_KEY)) {
                VideoKey = Params[AdsConfig.INMOBI_VIDEO_KEY];
            }
            if (Params.ContainsKey(AdsConfig.INMOBI_VIDEO_ID)) {
                VideoID = long.Parse(Params[AdsConfig.INMOBI_VIDEO_ID]);
            }

            Debug.Log("init Success");
        } catch(System.Exception e) {
            throw e;
        }
    }

    /// <summary>
    ///   加载插页广告
    /// </summary>
    public abstract  void LoadInterstitial();

    /// <summary>
    ///  检测插页广告是否准备就绪
    /// </summary>
    /// <returns> true 就绪，false 未就绪</returns>
    public abstract bool IsInterstitialReady();


    /// <summary>
    ///  显示插页广告
    /// </summary>
    public abstract void ShowInterstitial();

    /// <summary>
    /// 创建Banner广告
    /// </summary>
    public abstract void CreateBanner();

    /// <summary>
    /// 加载Banner广告
    /// </summary>
    public abstract void LoadBanner();

    /// <summary>
    /// 销毁Banner 广告
    /// </summary>
    public abstract void DestroyBanner();

    /// <summary>
    ///  加载视屏广告
    /// </summary>
    public abstract void LoadVideo();

    /// <summary>
    /// 检测视屏是否加载完毕
    /// </summary>
    /// <returns></returns>
    public abstract bool IsVideoReady();

    /// <summary>
    ///  显示视屏广告
    /// </summary>
    public abstract void ShowVideo();
}
