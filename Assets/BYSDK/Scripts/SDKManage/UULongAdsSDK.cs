using UnityEngine;
using UnityEngine.Advertisements;
using System;
using System.Collections.Generic;
using BYSDKManage;

public class UULongAdsSDK : MonoBehaviour {
    InterAdsManage      _interAds;
    //BannerAdsManage     _banner;
    VideoAdsManage      _videoAds;

    StrategyModel       _strategy;
    CurrentLocation location;
    Reachability reachability;
    public UULongAdsSDK() {

    }

    void Awake() {
    }

    void Start() {
        CurrentLocationValue.LocationChina = SDKUtiles.GetLocationByLanguae();
        _interAds = new InterAdsManage();
        _videoAds = new VideoAdsManage();
        this.gameObject.AddComponent<CurrentLocation>();
        location = this.GetComponent<CurrentLocation>();
        location.FetchLocationAndCallBannerInterstitial();
        this.gameObject.AddComponent<Reachability>();
        reachability = this.GetComponent<Reachability>();
        reachability.OnNetWorkConnectingSucceeded += OnNetConnectionSucceeded;
        reachability.OnCheckAds += CheckAds;
        reachability.CheckForConnection();
        SDKKener.GetInstance().RegistEventHandler(this,SDKEvent.ADS_INIT, InitAds);
        SDKKener.GetInstance().SetOrder(this);
        // 检测游戏是否有更新
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.GAME_UPDATE, EventStatus.ALLOW));   
    }

    /// <summary>
    /// 接收到事件后对广告进行初始化
    /// </summary>
    /// <param name="args"></param>
    void InitAds(SDKEventArgs args) {
        if( args.type == SDKEvent.ADS_INIT && args.status == EventStatus.ALLOW) {
            BYLog.Log("Ads inti ");
            _strategy = (StrategyModel)args.data;
            DisplayBannerAds(_strategy.Banner);
            initVideoAds();
            InitInterstitial(_strategy.Interstitial);
            SDKKener.GetInstance().EventNotict( new SDKEventArgs(SDKEvent.ADS_CHECK,EventStatus.ALLOW));    // 通知可进行配置更新
            BYLog.Log("Ads inti Over");
        }
    }
    public void ResetSDK() {
        //_banner = null;
        location.FetchLocationAndCallBannerInterstitial();
    }

    /// <summary>
    ///  创建并显示banner
    /// </summary>
    public void DisplayBannerAds(List<AdsStage> _stage) {
//        if( null == _banner) {
//            _banner = new BannerAdsManage(location.LocationChina, _stage);
//        }
    }

    /// <summary>
    ///  初始化插页广告
    /// </summary>
    public void InitInterstitial(List<AdsStage> _stage) {
        if( null == _interAds) {
            _interAds = new InterAdsManage();
        }
        _interAds.Init(_stage);
    }


    /// <summary>
    ///  显示插页广告，包含视屏插页广告
    /// </summary>
    public void displayInterstitialAds() {
        if (_interAds == null) {
            InitInterstitial(_strategy.Interstitial);
        }
        _interAds.displayInterstitialAdsWithVideo();
    }

    /// <summary>
    /// 显示插页广告，不包括视屏插页视屏广告
    /// </summary>
	public void displayInterstitialAdsP(BYVideoResulte resulte = null) {
        if( _interAds == null ) {
            InitInterstitial(_strategy.Interstitial);
        }
		_interAds.displayInterstitialAds(resulte);
    }

    /// <summary>
    /// Getz 等系列定制接口
    /// </summary>
    /// <param name="resulte"></param>
    public void displayinterstitialGetZ(BYVideoResulte resulte) {
        if (_interAds == null) {
            InitInterstitial(_strategy.Interstitial);
        }
        _interAds.displayInterstitialAdsWithVideo(resulte);
    }

    /// <summary>
    ///  分别对Banner，插页，视屏广告进行检查确认是否有广告准备好，如果没有准备好则进行请求。每20s检测一次
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void  CheckAds(object sender,EventArgs args) {
        BYLog.Log(" ***************************  Check ads Status ******************************");
        try {
            #region Check banner
//            if ( null == _banner ) {
//                BYLog.Log("Banner广告还没有初始化");
//                return;
//            }
//            _banner.CheckAds();
            #endregion
        } catch(Exception e) {
            BYLog.Log("Check ads : " + e.Message);
        }
        BYLog.Log(" *************************** Check ads Status over! *************************");
    }

    /// <summary>
    ///  获取视屏广告状态
    /// </summary>
    /// <returns></returns>
    public bool IsVideoAdsReady() {
        if (null == _videoAds)
            return false;
        return _videoAds.IsReadyForCheck();
    }

    public bool IsInterstitialAdsReady() {
        if (null == _interAds)
            return false;
        return _interAds.IsReady();
    }
    /// <summary>
    ///  显示视屏广告,没有视屏显示插页广告，没有插页广告那就么有办法了。。。
    /// </summary>
    /// <param name="loadhandle"></param>
    /// <param name="resulte"></param>
    public void displayVideoAds(Loading.LodingActionHandler loadhandle = null, BYVideoResulte resulte = null) {
        if( _videoAds == null ) {
            initVideoAds();
        }
        try {
            if(  null != _videoAds && _videoAds.IsReady()) {
                _videoAds.Display(loadhandle, resulte);
            } else if( null != _interAds && _interAds.IsReady()) {
                _interAds.displayInterstitialAdsWithVideo(resulte);
                if( loadhandle != null ) {
                    loadhandle(ActionType.Video, ActionResulet.Success);
                }
            } else {
                if (loadhandle != null) {
                    loadhandle(ActionType.Video, ActionResulet.NoAds);
                }
                if (null != resulte) {
                    resulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.NoAds));
                }
            }
        } catch(Exception e) {
            BYLog.InfoLog(e.Message);
            if( loadhandle != null ) {
                loadhandle(ActionType.Video, ActionResulet.Error);
                loadhandle = null;
            }
            if (null != resulte) {
                resulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.Error));
                resulte = null;
            }
        }

    }

    /// <summary>
    ///  初始化视屏广告
    /// </summary>
    public void initVideoAds() {
        if( _videoAds == null ) {
            _videoAds = new VideoAdsManage();
        }
        _videoAds.Init(_strategy.Video);
    }


    /// <summary>
    ///  网络断链重连之后回调
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void OnNetConnectionSucceeded(object sender, EventArgs args) {
        BYLog.Log("ReLoad OnNetConnectionSucceeded Async");
        location.FetchLocationAndCallBannerInterstitial();
        DisplayBannerAds(_strategy.Banner);
        InitInterstitial(_strategy.Interstitial);
        initVideoAds();
    }
}



