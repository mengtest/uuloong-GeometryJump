using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.Ads.Video
*
*	describe	:	N/A
*	class name	:	AdBuzzizVideo
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/15 11:41:42				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

class AdBuddizVideo : AbstractVideo {
    public override event EventHandler<AdsEventArgs> OnVoidStart;
    bool _mutexLoad = false;
    bool _isLoad = false;

    public AdBuddizVideo(string android_id, string iOS_id) {
        AdBuddizAds.GetInstance().init(android_id, iOS_id);
        RegistHandle();
    }

    void RegistHandle() {
        AdBuddizRewardedVideoManager.didFetch += DidFetch;
        AdBuddizRewardedVideoManager.didFail += DidFail;
        AdBuddizRewardedVideoManager.didComplete += DidComplete;
        AdBuddizRewardedVideoManager.didNotComplete += DidNotComplete;
    }

    void UnRegistHandle() {
        AdBuddizRewardedVideoManager.didFetch -= DidFetch;
        AdBuddizRewardedVideoManager.didFail -= DidFail;
        AdBuddizRewardedVideoManager.didComplete -= DidComplete;
        AdBuddizRewardedVideoManager.didNotComplete -= DidNotComplete;
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind,count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.VideoAds);
    }
    public override void Load() {
        if( !_mutexLoad ) {
            AdBuddizBinding.RewardedVideo.Fetch();
            _mutexLoad = true;
        } else {
            BYLog.Log("已经有一个AdBuddiz 视屏下载任务在执行，请稍等 ");
        }

    }

    public override void ReLoad() {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 播放视频广告，同时设置播放时间
    /// </summary>
    /// <param name="resulte"></param>
    public override void Display(BYVideoResulte resulte = null) {
        videoresulte = resulte;
        AdBuddizBinding.RewardedVideo.Show();
        IndividualData.UpdateShowCount();
        EventHandler<AdsEventArgs> handle = OnVoidStart;
        if (null != handle) {
            handle(this,new AdsEventArgs());
        }
        Collecter.AdsShow();
        OnVoidStart = null;
    }

    /// <summary>
    /// 检测广告加载状态
    /// </summary>
    /// <returns>false - 需要进行再次请求 ，true - 正在加载中</returns>
    public override bool CheckAds() {
        if( !_isLoad ) {
            Load();
            return false;
        } else {
            BYLog.Log("经过CheckAds AdBuddiz 视屏已经下载完成，不需要再次下载 ");
            return true;
        }
    }

    public override bool IsReady() {
        return AdBuddizBinding.RewardedVideo.IsReadyToShow() && IndividualData.IsTimeWindOpen();
    }
    #region Video delegate
    void DidFetch() {
        //AdBuddizBinding.LogNative("DidFetch");
        //AdBuddizBinding.ToastNative("DidFetch");
        _isLoad = true;
        _mutexLoad = false;
        BYLog.Log("AdBuzziz load Complete");
    }

    void DidFail(string adBuddizError) {
        //AdBuddizBinding.LogNative("DidFail: " + adBuddizError);
        //AdBuddizBinding.ToastNative("DidFail: " + adBuddizError);
        _isLoad = false;
        _mutexLoad = false;
        BYLog.Log("AdBuzziz load failed error: " + adBuddizError);
        Collecter.Failed("load Failed");
    }

    void DidComplete() {
        //AdBuddizBinding.LogNative("DidComplete");
        //AdBuddizBinding.ToastNative("DidComplete");
        OnVoidStart = null;
        if ( null != videoresulte ) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Success));
            videoresulte = null;
        }
        Collecter.AdsClosed();
        BYLog.Log("AdBuzziz did Complete");
    }

    void DidNotComplete() {
        OnVoidStart = null;
        //AdBuddizBinding.LogNative("DidNotComplete");
        //AdBuddizBinding.ToastNative("DidNotComplete");
        if (null != videoresulte) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Skipe));
            videoresulte = null;
        }
        Collecter.Failed("skipe");
        BYLog.Log("AdBuzziz not Complete");
    }
    #endregion

}

