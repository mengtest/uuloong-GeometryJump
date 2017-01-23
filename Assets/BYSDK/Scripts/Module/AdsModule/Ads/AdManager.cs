using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

#if UNITY_ANDROID
namespace bysdk
{
    public class AdManager
    {
        public static string BAIDU = "Baidu";
        public static string TENCENT = "Tencent";
        public static string DOMOB = "Domob";
        public static string MOBISAGE = "MobiSage";

        public delegate void AdManagerEventHandler(string eventName, string msg);
        public event AdManagerEventHandler bannerEventHandler;
        public event AdManagerEventHandler interstitialEventHandler;
        public event AdManagerEventHandler videoEventHandler;

        private static Dictionary<string, AdManager> instanceDic = new Dictionary<string, AdManager>();

        private AdManager()
        {
        }

        public static AdManager Instance(string ad)
        {
            AdManager adm = null;
            if (instanceDic.ContainsKey(ad))
            {
                adm = instanceDic[ad];
            }
            else {
                adm = new AdManager();
                adm.preInitAdManager(ad);
                instanceDic.Add(ad, adm);
            }
            return adm;
        }

        private AndroidJavaObject jAdManager;
        private void preInitAdManager(string ad)
        {
            if (jAdManager == null)
            {
                AndroidJavaClass AdManagerUnityPluginClass = new AndroidJavaClass("com.baiyigame.plugin.AdFactory");
                jAdManager = AdManagerUnityPluginClass.CallStatic<AndroidJavaObject>("getInstance", ad);
                InnerAdManagerListener innerlistener = new InnerAdManagerListener();
                innerlistener.AdManagerInstance = this;
                jAdManager.Call("setListener", new AdListenerProxy(innerlistener));
            }
        }

        public void init(string appID)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activy = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jAdManager.Call("init", new object[] { activy, appID });
        }

        public void setBannerId(string bannerID)
        {
            jAdManager.Call("setBannerId", bannerID);
        }

        public void setInstitialId(string institialId)
        {
            jAdManager.Call("setInstitialId", institialId);
        }

        public void createBanner()
        {
            jAdManager.Call("createBannerAd");
        }

        public void showBanner()
        {
            jAdManager.Call("showBannerAd");
        }

        public void removeBanner()
        {
            jAdManager.Call("removeBannerAd");
        }


        public void loadInterstitial()
        {
            jAdManager.Call("loadInterstitialAd");
        }
        public bool isInterstitialReady()
        {
            return jAdManager.Call<bool>("isInterstitialReady");
        }
        public void showInterstitial()
        {
            jAdManager.Call("showInterstitial");
        }

        class InnerAdManagerListener : IAdListener
        {
            internal AdManager AdManagerInstance;

            public void onAdEvent(string adtype, string eventName, string paramString)
            {
                if (adtype == "banner")
                {
                    AdManagerInstance.bannerEventHandler(eventName, paramString);
                }
                else if (adtype == "interstitial")
                {
                    AdManagerInstance.interstitialEventHandler(eventName, paramString);
                }
                else if (adtype == "video")
                {
                    AdManagerInstance.videoEventHandler(eventName, paramString);
                }
            }
        }
    }
}
#endif