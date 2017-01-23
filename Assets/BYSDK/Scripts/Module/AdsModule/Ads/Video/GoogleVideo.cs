using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMobileAds.Api;
using BYSDKManage;
public class GoogleVideo : AbstractVideo {
    public override event EventHandler<AdsEventArgs> OnVoidStart;
    public RewardBasedVideoAd rewardBasedVideo;
    private bool _RequestMuxLock = false;
    private string videoID = "";
    private bool rewardBasedEventHandlersSet = false;

    private bool isGetR;            // 用于判断是否提前关闭
    private bool isLoadFailed = false;
    bool _isLoad = false;
    public GoogleVideo() {
        CreateVideoAds();
    }

    public GoogleVideo(string aVideoID ) {
        videoID = aVideoID;

        CreateVideoAds();
    }


    ~GoogleVideo() {
        UnRegistRewardVideoHandle();
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.VideoAds);
    }

    void CreateVideoAds() {
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        RegistRewardVideoHandle();
        Load();
    }

    public override void Load() {
        if(_RequestMuxLock ) {
            BYLog.Log(" The google video Lock is locked .");
            return;
        }
        BYLog.Log(" Begin to load the google video .");
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, videoID);
    }

    public override void Display(BYVideoResulte resulte = null) {
        BYLog.Log(" Display video");
        videoresulte = resulte;
        if( rewardBasedVideo.IsLoaded()) {
            BYLog.Log(" Display video and video is ready ");
            IndividualData.UpdateShowCount();
            rewardBasedVideo.Show();
        } else {
            BYLog.Log(" Display video and video not ready ");
            Load();
            if(videoresulte != null) {
                BYLog.Log(" Display video and video not ready begin to call back");
                videoresulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.Failed));
                videoresulte = null;
            }
        }
    }

    public override bool CheckAds() {
        if (!rewardBasedVideo.IsLoaded() ) {
            BYLog.Log("Google video not load ,after check ads.begin to reload it");
            Load();
            return false;
        } else {
            BYLog.Log("Google video load complete ,after check ads.");
            return true;
        }

    }

    public override bool IsReady() {
        return _isLoad && IndividualData.IsTimeWindOpen();
    }

    public override void ReLoad() {
        //:TODO
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        Load();
    }
    void RegistRewardVideoHandle() {
        if (!rewardBasedEventHandlersSet) {
            BYLog.Log("Google video RegistRewardVideoHandle now");
            // Ad event fired when the rewarded video ad
            // has been received.
            rewardBasedVideo.OnAdLoaded += HandleRewardVideoLoaded;
            // has failed to load.
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardVideoFailedToLoad;
            // is opened.
            rewardBasedVideo.OnAdOpening += HandleRewardVideoOpened;
            // has started playing.
            rewardBasedVideo.OnAdStarted += HandleRewardVideoStarted;
            // has rewarded the user.
            rewardBasedVideo.OnAdRewarded += HandleRewardVideoRewarded;
            // is closed.
            rewardBasedVideo.OnAdClosed += HandleRewardVideoClosed;
            // is leaving the application.
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardVideoLeftApplication;

            rewardBasedEventHandlersSet = true;
        }

    }

    void UnRegistRewardVideoHandle() {
        if (rewardBasedEventHandlersSet) {
            // Ad event fired when the rewarded video ad
            // has been received.
            BYLog.Log("Google video UnRegistRewardVideoHandle now");
            rewardBasedVideo.OnAdLoaded -= HandleRewardVideoLoaded;
            // has failed to load.
            rewardBasedVideo.OnAdFailedToLoad -= HandleRewardVideoFailedToLoad;
            // is opened.
            rewardBasedVideo.OnAdOpening -= HandleRewardVideoOpened;
            // has started playing.
            rewardBasedVideo.OnAdStarted -= HandleRewardVideoStarted;
            // has rewarded the user.
            rewardBasedVideo.OnAdRewarded -= HandleRewardVideoRewarded;
            // is closed.
            rewardBasedVideo.OnAdClosed -= HandleRewardVideoClosed;
            // is leaving the application.
            rewardBasedVideo.OnAdLeavingApplication -= HandleRewardVideoLeftApplication;

            rewardBasedEventHandlersSet = false;
        }
    }

    public void HandleRewardVideoLoaded(object sender,EventArgs args) {
        BYLog.Log("[BYSDK google video ]  is loaded");
        // unlock request lock
        _RequestMuxLock = false;
        isLoadFailed = false;
        _isLoad = true;
    }


    public void HandleRewardVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        _RequestMuxLock = false;
        isLoadFailed = true;
        _isLoad = false;
        Collecter.Failed("load failed");
        BYLog.Log("[BYSDK google video ]  is load failed" + args.Message);

    }
    public void HandleRewardVideoOpened(object sender,EventArgs args) {
        BYLog.Log("[BYSDK google video ]  is opened");
    }
    public void HandleRewardVideoStarted(object sender,EventArgs args) {
        BYLog.Log("[BYSDK google video ]  is start");
        EventHandler<AdsEventArgs> handle = OnVoidStart;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
        Collecter.AdsShow();
        OnVoidStart = null;
    }
    public void HandleRewardVideoClosed(object sender,EventArgs args) {
        BYLog.Log("[BYSDK google video ]  is closed");
        OnVoidStart = null;
        _isLoad = false;
        Collecter.AdsClosed();
        if (null != videoresulte) {
            if (isGetR) {
                videoresulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.Success));
            } else {
                videoresulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.Failed));
            }
            videoresulte = null;
        }
        Load();
    }

    public void HandleRewardVideoLeftApplication(object sender,EventArgs args) {
        BYLog.Log("[BYSDK google video ]  is left app");
    }
    public void HandleRewardVideoRewarded(object sender, Reward args) {
        BYLog.Log("[BYSDK google video ]  is get reward");
        isGetR = true;
        //if ( null!= videoresulte) {
        //    videoresulte.resultCallback(new BYResulte(ActionType.Video,ActionResulet.Success));
        //}
    }

}
