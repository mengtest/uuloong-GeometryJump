using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.Ads.InMoBi
*
*	describe	:	N/A
*	class name	:	InMoBiBanerrEventHandle
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/23 15:49:53				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

class InMoBiBannerEventHandle {

    public event EventHandler<AdsEventArgs> BannerLoaded;
    public event EventHandler<AdsEventArgs> BannerOpend;
    public event EventHandler<AdsEventArgs> BannerFailedLoad;
    public event EventHandler<AdsEventArgs> BannerClosed;
    public InMoBiBannerEventHandle() {
        RegistHandle();
    }

    void RegistHandle() {
#if UNITY_IPHONE
        InMobiManager.bannerDidReceiveAdEvent += OnBannerLoaded;
        InMobiManager.bannerDidFailToReceiveAdWithErrorEvent += OnBannerLoadFailed;
        InMobiManager.bannerDidDismissScreenEvent += OnBannerClosed;
        InMobiManager.bannerDidInteractEvent += OnBannerOpend;
#endif
    }
    void OnBannerOpend(Dictionary<string, object> args) {
        BYLog.Log("InMoBI Banner Opend");
        EventHandler<AdsEventArgs> handle = BannerOpend;
        if (handle != null) {
            handle(this, new AdsEventArgs());
        }
    }

    void OnBannerClosed() {
        BYLog.Log("InMoBI Banner Closed");
        EventHandler<AdsEventArgs> handle = BannerClosed;
        if (handle != null) {
            handle(this, new AdsEventArgs());
        }
    }
    void OnBannerLoadFailed(string error) {
        BYLog.Log("InMoBi Banner load error: " + error);
        EventHandler<AdsEventArgs> handle = BannerFailedLoad;
        if (handle != null) {
            var e = new AdsEventArgs();
            e.Message = error;
            handle(this, e);
        }
    }
    void OnBannerLoaded() {
        BYLog.Log("InMoBI Banner loaded");
        EventHandler<AdsEventArgs> handle = BannerLoaded;
        if (handle != null) {
            handle(this, new AdsEventArgs());
        }
    }
}
