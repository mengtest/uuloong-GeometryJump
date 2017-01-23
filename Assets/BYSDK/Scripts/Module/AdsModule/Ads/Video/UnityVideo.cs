using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Advertisements;
using BYSDKManage;
/*********************************************************************//**
*	namespace	:	Assets.Script.Video
*
*	describe	:	N/A
*	class name	:	UnityVideo
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/7/29 22:08:38				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public class UnityVideo : AbstractVideo {
    public override event EventHandler<AdsEventArgs> OnVoidStart;
    public string adsID = "";

    AbstractVideo video;
    public UnityVideo() {
        init();
    }

    public UnityVideo(string  ID) {
        adsID = ID;
        init();
    }


    void init() {
        BYLog.Log("[BYSDK - Video init with ] " + adsID);
        Advertisement.Initialize(adsID, false);
        Advertisement.debugLevel = Advertisement.DebugLevel.Debug;
    }

    public override void SetIndividualData(long _TimeWind, int count, string tag = "", bool isuse = false) {
        IndividualData = new AdsIndividualData(_TimeWind, count,tag,isuse);
        Collecter = new DataCollecter(MType, SDKAdsType.VideoAds);
    }
    public override void Display(BYVideoResulte resulte = null) {
        BYLog.Log("[BYSDK - Video Unity start to display]");
        videoresulte = resulte;
        if (Advertisement.IsReady()) {
            var options = new ShowOptions { resultCallback = CallBackHandle };
            IndividualData.UpdateShowCount();
            Advertisement.Show(null, options);
            EventHandler<AdsEventArgs> handle = OnVoidStart;
            if (null != handle) {
                handle(this, new AdsEventArgs());
            }
            Collecter.AdsShow();
            OnVoidStart = null;
        } else {
            BYLog.Log("[BYSDK - Video Unity not ready]");
            CallBackHandle(ShowResult.Failed);
        }
    }

    public override bool IsReady() {
        return Advertisement.IsReady() && IndividualData.IsTimeWindOpen();
    }
    void CallBackHandle(ShowResult result) {
        BYLog.Log("resoult : " + result.ToString());
        switch (result) {
        case ShowResult.Finished:
            if (null != videoresulte)
                videoresulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.Success));
            Collecter.AdsClosed();
            break;
        case ShowResult.Skipped:
            if (null != videoresulte)
                videoresulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.Skipe));
            Collecter.Failed("skipe");
            break;
        case ShowResult.Failed:
            if (null != videoresulte)
                videoresulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.Failed));
            Collecter.Failed("load faile");
            ReLoad();
            break;
        }
        OnVoidStart = null;
        videoresulte = null;
        //addPointHandler = null;
    }

    public override bool CheckAds() {
        if( Advertisement.isInitialized ) {
            BYLog.Log("Unity inted");
            return false;
        } else {
            BYLog.Log("Unity not inited");
            return false;
        }

    }
    public override void Load() {

    }

    public static bool isSupported() {
        return Advertisement.isSupported;
    }
    public override void ReLoad() {

    }
}
