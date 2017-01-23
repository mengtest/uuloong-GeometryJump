using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class AdsConfig {
    public static readonly string LOAD_PANLE_PREFAB_PATH = "Prefabs/ShareMange/Loading";
    public const int BANNER_HEIGHT = 50;
    public static readonly string EMPTY = "";

    #region  Inmobi
    /// <summary>
    ///  inMOBI账号信息
    /// </summary>
    public static readonly string ACCID = "690e8a8b9d1740f7aed047c9b07e34b3";
    /// <summary>
    /// 插页广告键值
    /// </summary>
    public static readonly string INMOBI_INSTERTITIAL_KEY = "InMoBi_InsterKey";
    /// <summary>
    /// 插页广告位置ID
    /// </summary>
    public static readonly string INMOBI_INSTERTITIAL_ID = "InMoBi_InsterID";

    /// <summary>
    /// Banner广告键值
    /// </summary>
    public static readonly string INMOBI_BANNER_KEY = "InMoBi_BannerKey";
    /// <summary>
    /// Banner 广告ID
    /// </summary>
    public static readonly string INMOBI_BANNER_ID = "InMoBi_BannerID";

    /// <summary>
    /// 视屏广告键值
    /// </summary>
    public static readonly string INMOBI_VIDEO_KEY = "InMoBi_VideoKey";
    /// <summary>
    /// 视屏广告ID
    /// </summary>
    public static readonly string INMOBI_VIDEO_ID = "InMoBi_VideoID";
    #endregion

    #region  Vungle
	public static readonly string VUNGLE_ANDROID = "5822d8db8c3c580c2e0002fb";
	public static readonly string VUNGLE_IOS = "5822d876d4de8d052e000322";
    #endregion

    #region  Adbuddiz
    public static readonly string ADBUDDIZ_ANDROID = "8f738f55-b709-45f7-8de0-565c12f2f605";
    public static readonly string ADBUDDIZ_IOS = "10394924-c7a6-48b0-98d1-20089eaa2f0b";
    #endregion

    #region  Facebook
	public static readonly string FB_BANNER = "872693642867234_872694846200447";
	public static readonly string FB_INTERSTITIAL = "872693642867234_872694936200438";
    #endregion

    #region  Applovin
    public static readonly string APPLOVIN_SDKID = "GaqxDTH_gZkuKiTs9yTOnT2q2nXxalHsK5nXe2XJcFCxyfo5bFUNkWCmIlhtUzip3ULhXt086C33ts9T5dsp92";
    #endregion

    #region Tencent ads ID
#if UNITY_IPHONE
	public static readonly string TENCENT_BANNER = "8020915656962557";
	public static readonly string TENCENT_INTERSTITIAL = "2020618646165568";
	public static readonly string TENCENT_APPID = "1105810842";
#elif UNITY_ANDROID
	public static readonly string TENCENT_BANNER = "5050210656563552";
	public static readonly string TENCENT_INTERSTITIAL = "7010514666066593";
	public static readonly string TENCENT_APPID = "1105736707";
#endif
    #endregion

    #region  Baidu
    public static readonly string BAIDU_APPID = "c686fe73";
    public static readonly string BAIDU_ABNNERID = "2811978";
    public static readonly string BAIDU_INTERSTITIAL = "2811985";
    #endregion

    #region MobiSage
    public static readonly string MOBISAGE_APPID = "X15fjDDL0kVv3BsF2A==";
    public static readonly string MOBISAGE_BANNER = "xsfGFalRS9z2RYKcQfbF+0rg";
    public static readonly string MOBISAGE_INTERSTITIAL = "iouKWeUdB5C6Cc7QDbuJtwat";
    #endregion

    #region Adconloy Ads ID
#if UNITY_ANDROID
    public static readonly string ADCONLOY_VERSION = "version:1.0,store:google";
	public static readonly string ADCONLOY_APPID = "appf19c72fa7b56455bbb";
	public static readonly string ADCONLOY_ZONEID = "vza9485c3911574361b2";
#elif UNITY_IPHONE
    public static readonly string ADCONLOY_VERSION = "version:1.0";
	public static readonly string ADCONLOY_APPID = "app0ed68760c8264fbfa2";
	public static readonly string ADCONLOY_ZONEID = "vz7f816c42fdeb4ec2ab";
#endif
    #endregion

    #region Google Ads ID
    /// <summary>
    /// google banner ID  ,Instertitial ID
    /// </summary>
#if UNITY_IPHONE
	public static readonly string GOOGLE_BANNER = "ca-app-pub-2235431687140866/2755508834";
	public static readonly string GOOGLE_INTER_PICTURE = "ca-app-pub-2235431687140866/5708975239";
	public static readonly string GOOGLE_INTER = "ca-app-pub-2235431687140866/7185708432";
	public static readonly string GOOGLE_VIDEO = "ca-app-pub-2235431687140866/7185708432";
#elif UNITY_ANDROID
	public static readonly string GOOGLE_BANNER = "ca-app-pub-2235431687140866/1139174831";
	public static readonly string GOOGLE_INTER_PICTURE = "ca-app-pub-2235431687140866/2615908036";
	public static readonly string GOOGLE_INTER = "ca-app-pub-2235431687140866/4092641239";
	public static readonly string GOOGLE_VIDEO = "ca-app-pub-2235431687140866/4092641239";
#endif
    #endregion

    #region Unity ads
#if UNITY_ANDROID
	public static readonly string UNITY_ADSID = "1199875";
#else
	public static readonly string UNITY_ADSID = "1199876";
#endif
    #endregion

    #region ChartBoost Settings

	public const string CHARTBOOS_APPID_ANDROID = "5822d525f6cd45506935e10d";
	public const string CHARTBOOS_APPSECRET_ANDROID = "555e4b2184a22e0e77c7d97bb1d8f0a5cbb3e259";

	public const string CHARTBOOS_APPID_IOS = "5822d4ee04b0167b10ace3d7";
	public const string CHARTBOOS_APPSECRET_IOS = "1d362b8553e7afe7f38ffdd0c799b2c026759052";

    #endregion

    public static Dictionary<string, string> GetInMoBiData() {
        var ret = new Dictionary<string, string>();
#if UNITY_IPHONE
        ret.Add(INMOBI_INSTERTITIAL_KEY, "36d2ed9e51f846f8bef22453eeb90f46");
        ret.Add(INMOBI_INSTERTITIAL_ID, "1469799023568");
        ret.Add(INMOBI_BANNER_KEY, "5f35da91937545ee89c35ec75da453b2");
        ret.Add(INMOBI_BANNER_ID, "1467665695648");
        ret.Add(INMOBI_VIDEO_KEY, "bede5f21b20c488396687c7ca3545aba");
        ret.Add(INMOBI_VIDEO_ID, "1468551630024");
#elif UNITY_ANDROID
        ret.Add(INMOBI_INSTERTITIAL_KEY, "263fad911666429485dee3d6e8ff040c");
        ret.Add(INMOBI_INSTERTITIAL_ID, "1471844097785");
        ret.Add(INMOBI_BANNER_KEY, "5251679d8ad14040a25fdbf55c73d440");
        ret.Add(INMOBI_BANNER_ID, "1470013413933");
        ret.Add(INMOBI_VIDEO_KEY, "f7ee04b1f85d4a82b7ea6a0f30a0fa6d");
        ret.Add(INMOBI_VIDEO_ID, "1470564634298");
#endif
        return ret;
    }

    /// <summary>
    /// 获取插页广告配置信息
    /// </summary>
    /// <returns></returns>
    /// <example> 类型,国家区分,权重,显示时长/时间窗大小(s),显示次数</example>
    public static int[,] GetInterConfigure() {
#if UNITY_IPHONE
        int[,] ret = new int[,] {
            {(int)Vendors.CHARTBOOST,(int)AdsShowStratage.ALL,10,76,0 },
            { (int)Vendors.APPLOVIN,(int)AdsShowStratage.ALL,10,76,0},
            { (int)Vendors.GOOGLEP,(int)AdsShowStratage.ALL,10,76,0},
            // { (int)Vendors.INMOBI,(int)AdsShowStratage.ALL,10,76,0},
            { (int)Vendors.FACEBOOK,(int)AdsShowStratage.ABROAD,10,76,0},
            { (int)Vendors.ADBUDDIZ,(int)AdsShowStratage.ALL,10,76,0},
            { (int)Vendors.TENCENT,(int)AdsShowStratage.CHINA,10,76,0}
        };
#elif UNITY_ANDROID
        int[,] ret = new int[,] {
            {(int)Vendors.CHARTBOOST,(int)AdsShowStratage.ABROAD,10,76,0 },
            { (int)Vendors.APPLOVIN,(int)AdsShowStratage.ABROAD,10,76,0},
            { (int)Vendors.GOOGLEP,(int)AdsShowStratage.ABROAD,10,76,0},
            // { (int)Vendors.INMOBI,(int)AdsShowStratage.ABROAD,10,76,0},
            { (int)Vendors.FACEBOOK,(int)AdsShowStratage.ABROAD,10,76,0},
            { (int)Vendors.ADBUDDIZ,(int)AdsShowStratage.ABROAD,10,76,0},
            { (int)Vendors.TENCENT,(int)AdsShowStratage.CHINA,10,76,0}
        };
#endif
        return ret;
    }

    /// <summary>
    /// 获取视屏广告配置信息
    /// </summary>
    /// <returns></returns>
    /// <example> 类型,国家区分,权重,显示时长/视屏显示间隔-时间窗大小(s),显示次数</example>
    /// <info> Google视屏与暂时接入的视屏重合</info>
    public static int[,] GetVideoConfigure() {
#if UNITY_IPHONE
        int[,] ret = new int[,] {
            { (int)Vendors.UNITY,(int)AdsShowStratage.ALL,10,2900,10},
            { (int)Vendors.CHARTBOOST,(int)AdsShowStratage.ALL,10,308,0},
            { (int)Vendors.APPLOVIN,(int)AdsShowStratage.ALL,10,308,0},
            { (int)Vendors.ADCOLONY,(int)AdsShowStratage.ALL,10,308,0},
            { (int)Vendors.VUNGLE,(int)AdsShowStratage.ALL,10,308,0},
            { (int)Vendors.FACEBOOK,(int)AdsShowStratage.ABROAD,10,308,0}
        };
#elif UNITY_ANDROID
        int[,] ret = new int[,] {
            { (int)Vendors.UNITY,(int)AdsShowStratage.ALL,10,2900,10},
            { (int)Vendors.APPLOVIN,(int)AdsShowStratage.ALL,10,308,0},
            { (int)Vendors.ADCOLONY,(int)AdsShowStratage.ALL,10,308,0},
            { (int)Vendors.VUNGLE,(int)AdsShowStratage.ALL,10,308,0},
            { (int)Vendors.FACEBOOK,(int)AdsShowStratage.ABROAD,10,308,0}
        };
#endif

        return ret;
    }


    /// <summary>
    ///  获取Banner配置信息
    /// </summary>
    /// <returns></returns>
    /// <example> 类型,国家区分,权重,显示时长(分钟),显示次数</example>
    public static int[,] GetBannerConfigure() {
#if UNITY_IPHONE
        var ret = new int[,] {
//            { (int)Vendors.FACEBOOK,(int)AdsShowStratage.ABROAD,10,330,0},
//            { (int)Vendors.TENCENT,(int)AdsShowStratage.CHINA,10,330,0},
//            { (int)Vendors.GOOGLE,(int)AdsShowStratage.ALL,10,330,0}
        };
#elif UNITY_ANDROID
        var ret = new int[,] {
            { (int)Vendors.FACEBOOK,(int)AdsShowStratage.ABROAD,10,330,0},
            { (int)Vendors.GOOGLE,(int)AdsShowStratage.ABROAD,10,330,0},
            { (int)Vendors.TENCENT,(int)AdsShowStratage.CHINA,10,330,0},
            { (int)Vendors.BAIDU,(int)AdsShowStratage.CHINA,10,330,0}
        };
#endif

        return ret;
    }

    public static List<AdsData> CreateAdsDataByConfigure(int[,] data, bool isChina) {
        var ret = new List<AdsData>();
        int length = data.Length / 5;
        for (int i = 0; i < length; i++) {
            var temp = new AdsData();
            temp._type = (Vendors)data[i, 0];
            temp._ShowStratage = (AdsShowStratage)data[i, 1];
            if (isChina && temp._ShowStratage == AdsShowStratage.ABROAD) {
                continue;
            } else if (!isChina && temp._ShowStratage == AdsShowStratage.CHINA) {
                continue;
            }
            temp._wight = data[i, 2];
            temp._mins = data[i, 3];
            temp._times = data[i, 4];
            temp._UsefullCount = 0;            // 设置广告展示次数为0
            ret.Add(temp);
        }
        return ret;
    }


    public static List<AdsData> CreateAdsListByConfigure(int[,] data, bool isChina) {
        var ret = new List<AdsData>();
        int length = data.Length / 5;
        for (int i = 0; i < length; i++) {
            var temp = new AdsData();
            temp._type = (Vendors)data[i, 0];
            temp._ShowStratage = (AdsShowStratage)data[i, 1];
            if (isChina && temp._ShowStratage == AdsShowStratage.ABROAD) {
                continue;
            } else if (!isChina && temp._ShowStratage == AdsShowStratage.CHINA) {
                continue;
            }
            temp._wight = data[i, 2];
            temp._mins = data[i, 3];
            temp._times = data[i, 4];
            ret.Add(temp);
        }

        return ret;
    }

}


