using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.AdsModule.Configure.AdsDefault
*
*	describe	:	N/A
*	class name	:	Configure
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/27 15:12:35				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

/// 广告配置数据管
/// 配置管理模块在初始话完成并发送 ALLOW 事件之后，Configure 进行数据组装
/// 组装接收后，发送complete 事件，通知广告管理模块进行广告初始化
public class Configure {
    StrategyModel _strategy;
    public Configure() {
        SDKKener.GetInstance().RegistEventHandler(this,SDKEvent.ADS_CONFIGURE,Init);
    }
    void Init(SDKEventArgs args) {
        if( args.type == SDKEvent.ADS_CONFIGURE && args.status == EventStatus.ALLOW) {
            _strategy = SDKKener.GetInstance().GetStrategy();
            if (null == _strategy) {
                _strategy = new StrategyModel();
                CreateBannerData();
                CreateInterData();
                CreateVideo();
            }
            SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.ADS_INIT, EventStatus.ALLOW, _strategy));
        }

    }

    /// <summary>
    /// _strategy 为空则使用默认配置
    /// </summary>
    void CreateBannerData() {
        var banr =  DefaultConfig.CreateAdsDataByConfigure(DefaultConfig.GetBannerConfigure(),SDKKener.GetInstance().isChina);
        _strategy.Banner = banr;
    }

    /// <summary>
    /// 创建插页广告配置数据
    /// </summary>
    void CreateInterData() {
        var Inter = DefaultConfig.CreateAdsDataByConfigure(DefaultConfig.GetInterConfigure(), SDKKener.GetInstance().isChina);
        _strategy.Interstitial = Inter;
    }

    /// <summary>
    /// 创建视屏广告配置数据
    /// </summary>
    void CreateVideo() {
        var vide = DefaultConfig.CreateAdsDataByConfigure(DefaultConfig.GetVideoConfigure(), SDKKener.GetInstance().isChina);
        _strategy.Video = vide;
    }


    /// <summary>
    /// 获取Banner策略数据
    /// </summary>
    /// <returns></returns>
    public List<AdsStage> GetBannerStage() {
        return _strategy.Banner;
    }

    /// <summary>
    /// 获取 interstatial 策略数据
    /// </summary>
    /// <returns></returns>
    public List<AdsStage> GetInterStage() {
        return _strategy.Interstitial;
    }

    /// <summary>
    /// 获取 Video策略数据
    /// </summary>
    /// <returns></returns>
    public List<AdsStage> GetVideoStage() {
        return _strategy.Video;
    }

    public StrategyModel GetStagey() {
        return _strategy;
    }
}
