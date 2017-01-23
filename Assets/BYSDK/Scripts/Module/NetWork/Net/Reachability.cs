using UnityEngine;
using System;
using System.Net;
using System.Collections;

enum NetWorkConnectionStatus { Succeeded = 1, Failed };

public class Reachability : MonoBehaviour {
    public event EventHandler<EventArgs> OnNetWorkConnectingFailed = delegate { };
    public event EventHandler<EventArgs> OnNetWorkConnectingSucceeded = delegate { };
    public event EventHandler<EventArgs> OnCheckAds = delegate { };
    private NetWorkConnectionStatus LastConnectStatus = NetWorkConnectionStatus.Succeeded;

    public static bool IsInternetConnection() {
        bool isConnectedToInternet = false;
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
                Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork) {
            isConnectedToInternet = true;
        }
        return isConnectedToInternet;
    }


    public void CheckForConnection() {
        StartCoroutine(RunConnectionWorker());
    }

    IEnumerator RunConnectionWorker() {
        while (true) {
            WWW www = new WWW("http://www.baidu.com");
            yield return www;

            if (www.error == null) {
                if (LastConnectStatus == NetWorkConnectionStatus.Failed) {
                    BYLog.Log("OnNetWorkConnectingSucceeded");
                    LastConnectStatus = NetWorkConnectionStatus.Succeeded;
                    EventHandler<EventArgs> handler = OnNetWorkConnectingSucceeded;
                    if (handler != null) {
                        handler(null, new EventArgs());
                    }

                }
            } else {
                if (LastConnectStatus == NetWorkConnectionStatus.Succeeded) {
                    BYLog.Log("OnNetWorkConnectingFailed " + www.error);
                    LastConnectStatus = NetWorkConnectionStatus.Failed;
                    EventHandler<EventArgs> handler = OnNetWorkConnectingFailed;
                    if (handler != null) {
                        handler(null, new EventArgs());
                    }

                }
            }
            EventHandler<EventArgs> handl = OnCheckAds;
            if (handl != null) {
                handl(null, new EventArgs());
            }
            yield return new WaitForSeconds(60.0f);
        }
    }
}