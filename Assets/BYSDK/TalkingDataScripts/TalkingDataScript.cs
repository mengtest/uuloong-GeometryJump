using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class TalkingDataScript : MonoBehaviour {

    private string appID;
    private string channel;
    void Start () {
#if UNITY_IPHONE
        UnityEngine.iOS.NotificationServices.RegisterForNotifications(
            UnityEngine.iOS.NotificationType.Alert |
            UnityEngine.iOS.NotificationType.Badge |
            UnityEngine.iOS.NotificationType.Sound);
        channel = "iphone";
#elif UNITY_ANDROID
        channel = "android";
#endif
        BYLog.Log("TalkData begin to init");
        TalkingDataGA.OnStart(Config.TALK_DATA, channel);
        TDGAAccount.SetAccount(TalkingDataGA.GetDeviceId());
    }


    void Update () {
#if UNITY_IPHONE
        TalkingDataGA.SetDeviceToken();
        TalkingDataGA.HandleTDGAPushMessage();
#endif
    }

    void OnDestroy () {
        TalkingDataGA.OnEnd();
        BYLog.Log("TalkData onDestroy");
    }

    void Awake () {
        BYLog.Log("TalkData Awake");
    }

    void OnEnable () {
        BYLog.Log("TalkData OnEnable");
    }

    void OnDisable () {
        BYLog.Log("OnDisable");
    }
}
