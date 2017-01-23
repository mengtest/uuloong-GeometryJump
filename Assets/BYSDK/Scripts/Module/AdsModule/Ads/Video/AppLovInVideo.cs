using UnityEngine;
using System.Collections;
using System;

public class AppLovInVideo : AbstractVideo {
    public override event EventHandler<AdsEventArgs> OnVoidStart;
    bool _loadMutex = false;
    bool _isLoadFailed = false;


    public AppLovInVideo() {
        AppLIAds.noop();
        RegistHandle();
    }

    ~AppLovInVideo() {
        RegistHandle();
    }


    void RegistHandle() {
        AppLIAds.onVideoLoad        += OnVideoLoad;
        AppLIAds.onVideoLoadFailed  += OnVideoLoadFailed;
        AppLIAds.onVideoPlay        += OnVideoPlay;
        AppLIAds.onVideoClose       += OnVideoClosed;
        AppLIAds.onVideoSkip        += OnVideoSkip;
        AppLIAds.onVideoBeShow      += OnVideoBeShow;
        AppLIAds.onVideoStop        += OnVideoStop;
    }

    void UnRegistHandle() {
        AppLIAds.onVideoLoad        -= OnVideoLoad;
        AppLIAds.onVideoLoadFailed  -= OnVideoLoadFailed;
        AppLIAds.onVideoPlay        -= OnVideoPlay;
        AppLIAds.onVideoClose       -= OnVideoClosed;
        AppLIAds.onVideoSkip        -= OnVideoSkip;
        AppLIAds.onVideoBeShow      -= OnVideoBeShow;
        AppLIAds.onVideoStop        -= OnVideoStop;
    }


    public override void Load() {
        if (!_loadMutex) {
            AppLIAds.LoadVideo();
            _loadMutex = true;
        }
    }
    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.VideoAds);
    }
    public override void ReLoad() {
        BYLog.Log("Begin to reload AppLovIn video");
        Load();
    }

    public override bool IsReady() {
        return AppLIAds.IsVideoReady() && IndividualData.IsTimeWindOpen();
    }

    public override void Display(BYVideoResulte resulte = null) {
        videoresulte = resulte;
        IndividualData.UpdateShowCount();
        AppLIAds.DisplayVideo();
    }

    public override bool  CheckAds() {
        if( _isLoadFailed) {
            BYLog.Log("AppLovIn video not reload ,begin to load it");
            Load();
            return false;
        } else {
            BYLog.Log("AppLovIn video already loaded");
            return true;
        }
    }


    void OnVideoLoad(object sender , EventArgs args) {
        BYLog.Log("AppLovIn Video is loaded");
        _loadMutex = false;
        _isLoadFailed = true;
    }

    void OnVideoLoadFailed(object sender,EventArgs args) {
        BYLog.Log("AppLovIn Video is load failed");
        _loadMutex = false;
        _isLoadFailed = false;
        Collecter.Failed("load failed");
    }

    void OnVideoPlay(object sender, EventArgs args) {
        BYLog.Log("AppLovIn Video is playing");
        EventHandler<AdsEventArgs> handle = OnVoidStart;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
        OnVoidStart = null;
        Collecter.AdsShow();
    }

    void OnVideoClosed(object sender,EventArgs args) {
        BYLog.Log("AppLovIn Video is Closed");
        OnVoidStart = null;
        if ( videoresulte != null ) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video,BYSDKManage.ActionResulet.Success));
            videoresulte = null;
        }
        Collecter.AdsClosed();
        Load();
    }

    void OnVideoSkip(object sender,EventArgs args) {
        if (videoresulte != null) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Skipe));
            videoresulte = null;
        }
        Collecter.Failed("skip");
        BYLog.Log("AppLovIn Video is skip ");
    }

    void OnVideoBeShow(object sender,EventArgs args) {
        BYLog.Log("AppLovIn Video is be showing");
        Collecter.AdsShow();
    }

    void OnVideoStop(object sender ,EventArgs args) {
        BYLog.Log("AppLovIn Video is stoped");
    }
}
