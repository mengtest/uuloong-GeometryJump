using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.AdsModule.Configure
*
*	describe	:	N/A
*	class name	:	SDKAdsType
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/10/14 10:50:47				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
/// <summary>
/// 广告类型
/// </summary>
public enum SDKAdsType {

    /// <summary>
    ///  不显示广告
    /// </summary>
    NOAds = 0,
    /// <summary>
    /// 插页广告
    /// </summary>
    Interstitial = 1,

    /// <summary>
    /// banner广告
    /// </summary>
    Banner = 2,


    /// <summary>
    /// 视屏广告
    /// </summary>
    VideoAds = 3,
    /// <summary>
    /// 插页广告，包含插页视屏
    /// </summary>
    InterStitialVideo = 4

}