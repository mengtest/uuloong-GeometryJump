using UnityEngine;
using System.Collections;
using System;

public class AdColonyVideo : AbstractVideo {
    public override event EventHandler<AdsEventArgs> OnVoidStart;
    string _zoneID;
    bool _isLoad;
    public AdColonyVideo(string verison,string appid,string zoneID) {
        AdColony.Configure(
            verison,
            appid,
            zoneID);
        _zoneID = zoneID;
        _isLoad = false;
        RegistHandle();
    }

    ~AdColonyVideo() {
        UnRegistHandle();
    }

    void RegistHandle() {
        AdColony.OnVideoStarted             += OnVideoStart;
        AdColony.OnVideoFinishedWithInfo    += OnVideoFinishedWithInfo;
        AdColony.OnAdAvailabilityChange     += OnAdAvilabilityChange;
        AdColony.OnVideoFinished            += OnVideoFinished;
    }

    void UnRegistHandle() {
        AdColony.OnVideoStarted             -= OnVideoStart;
        AdColony.OnVideoFinishedWithInfo    -= OnVideoFinishedWithInfo;
        AdColony.OnAdAvailabilityChange     -= OnAdAvilabilityChange;
        AdColony.OnVideoFinished            -= OnVideoFinished;
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.VideoAds);
    }

    public override void Display(BYVideoResulte resulte = null) {
        videoresulte = resulte;
        IndividualData.UpdateShowCount();
        AdColony.ShowVideoAd(_zoneID);
    }

    public override bool IsReady() {
        return AdColony.IsVideoAvailable(_zoneID) && IndividualData.IsTimeWindOpen();
    }

    /// <summary>
    /// AdColony 视屏广告属于自动加载，因此不实现 Load ReLoad 方法
    /// </summary>
    public override void Load() {
        throw new NotImplementedException();
    }

    public override void ReLoad() {
        throw new NotImplementedException();
    }

    public override bool  CheckAds() {
        if(_isLoad) {
            BYLog.Log("AdColony is ready");
            return false;
        } else {
            BYLog.Log("AdColony not ready");
            return false;
        }
    }

    void OnVideoStart() {
        _isLoad = false;
        EventHandler<AdsEventArgs> handle = OnVoidStart;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
        OnVoidStart = null;
        Collecter.AdsShow();
    }

    /// <summary>
    ///  视屏播放成功回调，同时设置isload 为 false 用于CheckAds进行重新加载
    /// </summary>
    /// <param name="tag"></param>
    void OnVideoFinished(bool tag) {
        _isLoad = false;
        OnVoidStart = null;
        if ( null != videoresulte ) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Success));
            videoresulte = null;
        }
        Collecter.AdsClosed();
    }
    void OnVideoFinishedWithInfo(AdColonyAd info) {
        BYLog.Log(" AdColony info : " + info);
    }
    void OnAdAvilabilityChange(bool tag, string zoneid) {
        BYLog.Log(" AdColony status : " + tag + "  zone: " + zoneid);
        _isLoad = tag;
    }
}
