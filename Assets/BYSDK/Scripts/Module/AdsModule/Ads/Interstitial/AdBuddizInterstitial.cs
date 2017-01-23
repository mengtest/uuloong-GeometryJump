using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.Ads.Interstitial
*
*	describe	:	N/A
*	class name	:	AdBuddizInterstitial
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/15 11:22:53				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
class AdBuddizInterstitial : InterstitialAds {
    public override event EventHandler<AdsEventArgs> AdLoaded;
    public override event EventHandler<AdsEventArgs> AdFailedToLoad;
    public override event EventHandler<AdsEventArgs> AdOpened;
    public override event EventHandler<AdsEventArgs> AdClosed;

    bool _mutexLoad = false;
    bool _isLoad = false;
    public AdBuddizInterstitial( string android_id, string iOS_id) {
        AdBuddizAds.GetInstance().init(android_id, iOS_id);
        RegistHandle();
    }

    void RegistHandle() {
        AdBuddizManager.didFailToShowAd += DidFailToShowAd;
        AdBuddizManager.didCacheAd += DidCacheAd;
        AdBuddizManager.didShowAd += DidShowAd;
        AdBuddizManager.didClick += DidClick;
        AdBuddizManager.didHideAd += DidHideAd;
    }

    void UnRegistHandle() {
        AdBuddizManager.didFailToShowAd -= DidFailToShowAd;
        AdBuddizManager.didCacheAd -= DidCacheAd;
        AdBuddizManager.didShowAd -= DidShowAd;
        AdBuddizManager.didClick -= DidClick;
        AdBuddizManager.didHideAd -= DidHideAd;
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.Interstitial);
    }

    public override void Load() {
        if(!_mutexLoad) {
            AdBuddizBinding.CacheAds();
            _mutexLoad = true;
        } else {
            BYLog.Log("已经有一个AdBuddiz 插页广告下载任务在执行，请稍等 ");
        }

    }

    public override void CheckAds() {
        if( !_isLoad ) {
            Load();
        } else {
            BYLog.Log("AdBuddiz 插页广告下载完成 ");
        }
    }

    public override void Display() {
        IndividualData.UpdateShowCount();
        AdBuddizBinding.ShowAd();
    }

    public override bool IsReady() {
        return AdBuddizBinding.IsReadyToShowAd() && IndividualData.IsTimeWindOpen(); ;
    }

    public override void ReLoad() {
        throw new NotImplementedException();
    }

    void DidFailToShowAd(string adBuddizError) {
        BYLog.Log(" AdBuddiz Failed to show ads Error: " + adBuddizError);
        _isLoad = false;
        _mutexLoad = false;
        Collecter.Failed(adBuddizError);
    }

    void DidCacheAd() {
        //AdBuddizBinding.LogNative("DidCacheAd");
        //AdBuddizBinding.ToastNative("DidCacheAd");
        BYLog.Log(" AdBuddiz ads loaded" );
        _isLoad = true;
        _mutexLoad = false;
    }

    void DidShowAd() {
        //AdBuddizBinding.LogNative("DidShowAd");
        //AdBuddizBinding.ToastNative("DidShowAd");
        Collecter.AdsShow();
        BYLog.Log(" AdBuddiz ads showing");
    }

    void DidClick() {
        //AdBuddizBinding.LogNative("DidClick");
        //AdBuddizBinding.ToastNative("DidClick");
        Collecter.AdsClick();
        BYLog.Log(" AdBuddiz ads click");
    }


    void DidHideAd() {
        //AdBuddizBinding.LogNative("DidHideAd");
        //AdBuddizBinding.ToastNative("DidHideAd");
        EventHandler<AdsEventArgs> handler = AdClosed;
        if (handler != null) {
            handler(this, new AdsEventArgs());
        }
        AdClosed = null;
        Collecter.AdsClosed();
        BYLog.Log(" AdBuddiz ads closed");
    }
}

