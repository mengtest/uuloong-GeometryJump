using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.AdsModule.Configure.AdsDefault
*
*	describe	:	N/A
*	class name	:	DefaultConfig
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/27 15:06:46				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

// 广告默认设置
public   class DefaultConfig {


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
            { (int)Vendors.FACEBOOK,(int)AdsShowStratage.ABROAD,10,330,0},
            { (int)Vendors.TENCENT,(int)AdsShowStratage.CHINA,10,330,0},
            { (int)Vendors.GOOGLE,(int)AdsShowStratage.ALL,10,330,0}
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

    /// <summary>
    /// 根据位置信息，创建广告配置数据
    /// </summary>
    /// <param name="data"></param>
    /// <param name="isChina"></param>
    /// <returns></returns>
    public static List<AdsStage> CreateAdsDataByConfigure(int[,] data, bool isChina) {
        var ret = new List<AdsStage>();
        int length = data.Length / 5;
        for (int i = 0; i < length; i++) {
            var temp = new AdsStage();
            temp.vendors_id = data[i, 0];
            var _ShowStratage = (AdsShowStratage)data[i, 1];
            if (isChina && _ShowStratage == AdsShowStratage.ABROAD) {
                continue;
            } else if (!isChina && _ShowStratage == AdsShowStratage.CHINA) {
                continue;
            }
            temp.weight = data[i, 2];
            temp.mins = data[i, 3];
            temp.times = data[i, 4];
            temp.z_index = i;
            ret.Add(temp);
        }
        return ret;
    }
}

