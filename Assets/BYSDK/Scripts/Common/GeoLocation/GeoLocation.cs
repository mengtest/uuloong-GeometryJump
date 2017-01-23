using System;
using System.IO;
using System.Threading;
using UnityEngine;
using System.Collections;
using System.Net;
using Newtonsoft.Json;

class CurrentLocationValue {
    public static bool LocationChina {
        get;
        set;
    }
    public static bool ResultReady {
        get;
        set;
    }
}

class CurrentLocation : MonoBehaviour {

    public bool LocationChina {
        get {
            return CurrentLocationValue.LocationChina;
        }
    }

    private bool _RequestingLock = false;

    public void FetchLocationAndCallBannerInterstitial() {
        if (_RequestingLock) {
            return;
        }

        _RequestingLock = true;
        StartCoroutine(StartFetchLocationWorker());
    }

    public IEnumerator StartFetchLocationWorker() {
        BYLog.Log("GeoLocationGeoLocationGeoLocation : StartFetchLocationWorker");
        WWW www = new WWW("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json");

        float timer = 0;
        bool failed = false;
        while (!www.isDone) {
            if (timer > 1.5) {
                failed = true;
                break;
            }
            timer += Time.deltaTime;
            yield return www;
        }

        if (failed) {
            www.Dispose();
            SDKKener.GetInstance().SetLocation(false);
            // SentMessageToObject();
            yield break;
        };

        if (www.error == null) {
            GeoLocation g = JsonConvert.DeserializeObject<GeoLocation>(www.text);
            BYLog.Log("GeoLocationGeoLocationGeoLocation " + g.country);

            if (g.country == "中国") {
                // CurrentLocationValue.LocationChina = true;
                SDKKener.GetInstance().SetLocation(true);
            }
        } else {
            BYLog.Log("Net Request Message " + www.error);
            SDKKener.GetInstance().SetLocation(false);
        }

        _RequestingLock = false;

        // SentMessageToObject();

        yield return null;
    }

    private void SentMessageToObject() {
        BYLog.Log("Begin send message to show ads ");
        gameObject.SendMessage("DisplayBannerAds");
        gameObject.SendMessage("InitInterstitial");
        gameObject.SendMessage("initVideoAds");
    }
}

class GeoLocation {
    public string country {
        get;
        set;
    }
}

