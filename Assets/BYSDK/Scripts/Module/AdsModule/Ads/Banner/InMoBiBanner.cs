using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.Ads.Banner
*
*	describe	:	N/A
*	class name	:	InMoBiBanner
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/23 14:33:38				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

class InMoBiBanner: BannerAds {
    InMoBiAds InMoBiBannerClient;
    InMoBiBannerEventHandle eventHandle;
    public override event EventHandler<AdsEventArgs> AdLoaded = delegate { };
    public override event EventHandler<AdsEventArgs> AdFailedToLoad = delegate { };
    public override event EventHandler<AdsEventArgs> AdOpened = delegate { };
    public override event EventHandler<AdsEventArgs> AdClosed = delegate { };

    public InMoBiBanner() {
        InMoBiBannerClient = InMobiInstance.GetInstance();
        InMoBiBannerClient.CreateBanner();
        eventHandle = new InMoBiBannerEventHandle();
        eventHandle.BannerLoaded +=  OnLoaded;
        eventHandle.BannerFailedLoad += OnLoadFailed;
        eventHandle.BannerOpend += OnOpend;
        eventHandle.BannerClosed += OnClose;
    }

    void RegistHandle() {

    }

    void UnRegistHandle() {

    }
    public override void SetIndividualData(long _TimeWind, int count) {
        IndividualData = new AdsIndividualData(_TimeWind, count);
    }
    public override void Destroy() {
        InMoBiBannerClient.DestroyBanner();
    }

    public override void LoadAndDisplay() {
        IndividualData.UpdateShowCount();
        InMoBiBannerClient.LoadBanner();
    }

    public override void CheckAds() {
        BYLog.Log("InMoBi check");
    }

    public override void Hide() {
        //InMoBiBannerClient.DestroyBanner();
    }

    void OnLoaded(object sender, AdsEventArgs e) {
        BYLog.Log("InMoBi Banner loaded");
        EventHandler<AdsEventArgs> handle = AdLoaded;
        if( null != handle) {
            handle(this,e);
        }
    }

    void OnLoadFailed(object sender,AdsEventArgs e) {
        BYLog.Log("InMoBi Banner load failed error: " + e.Message);
        EventHandler<AdsEventArgs> handle = AdFailedToLoad;
        if (null != handle) {
            handle(this,e );
        }
    }

    void OnOpend(object sender,AdsEventArgs e) {
        BYLog.Log("InMoBi Banner opend");
        EventHandler<AdsEventArgs> handle = AdOpened;
        if (null != handle) {
            handle(this, e);
        }
    }

    void OnClose(object sender, AdsEventArgs e) {
        BYLog.Log("InMoBi Banner Closed");
        EventHandler<AdsEventArgs> handle = AdClosed;
        if (null != handle) {
            handle(this, e);
        }
    }
}
