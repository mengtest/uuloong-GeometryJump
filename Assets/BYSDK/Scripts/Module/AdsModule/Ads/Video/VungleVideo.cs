using UnityEngine;
using System.Collections;
using System;

public class VungleVideo : AbstractVideo {
    public override event EventHandler<AdsEventArgs> OnVoidStart;

    public VungleVideo(string androidAppID,string iOSAppID ,string winAppID = "") {
        Vungle.init(androidAppID, iOSAppID, winAppID);
        videoresulte = null;
        RegistHandle();
    }

    ~VungleVideo() {
        UnRegistHandle();
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.VideoAds);
    }
    void RegistHandle() {
        Vungle.onAdStartedEvent     += onAdStartedEvent;
        Vungle.onAdFinishedEvent    += onAdFinishedEvent;
        Vungle.adPlayableEvent      += adPlayableEvent;
        Vungle.onLogEvent           += OnLogEvent;
    }

    void UnRegistHandle() {
        Vungle.onAdStartedEvent     -= onAdStartedEvent;
        Vungle.onAdFinishedEvent    -= onAdFinishedEvent;
        Vungle.adPlayableEvent      -= adPlayableEvent;
        Vungle.onLogEvent           -= OnLogEvent;
    }

    public override bool IsReady() {
        return Vungle.isAdvertAvailable() && IndividualData.IsTimeWindOpen();
    }

    public override void Display(BYVideoResulte resulte = null) {
        videoresulte = resulte;
        if( !Vungle.isAdvertAvailable() && null!= videoresulte) {
            videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Failed));
            videoresulte = null;
        }
        IndividualData.UpdateShowCount();
        Vungle.playAd();
    }

    public override void ReLoad() {
        //:TODO
    }

    public override void Load() {
        //:TODO
    }


    public override bool CheckAds() {
        //TODO
        if( Vungle.isAdvertAvailable() ) {
            BYLog.Log(" Vungle video is ready");
            return false;
        } else {
            BYLog.Log(" Vungle video not ready");
            return false;
        }
    }

    void onAdStartedEvent() {
        BYLog.Log("onAdStartedEvent");
        EventHandler<AdsEventArgs> handle = OnVoidStart;
        if (null != handle) {
            handle(this, new AdsEventArgs());
        }
        OnVoidStart = null;
        Collecter.AdsShow();
    }


    void onAdFinishedEvent(AdFinishedEventArgs arg) {
        OnVoidStart = null;
        Collecter.AdsClosed();
        if ( null != videoresulte ) {
            if(arg.IsCompletedView) {

                videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Success));
            } else {
                videoresulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Skipe));
            }
            videoresulte = null;
        }
        BYLog.Log("onAdFinishedEvent. watched: " + arg.TimeWatched + ", length: " + arg.TotalDuration + ", isCompletedView: " + arg.IsCompletedView);
    }


    void adPlayableEvent(bool playable) {
        BYLog.Log("adPlayableEvent: " + playable);
    }

    void OnLogEvent(string evn) {
        BYLog.Log("Vungle logEvent: " + evn);
    }
}
