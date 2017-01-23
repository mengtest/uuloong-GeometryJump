using UnityEngine;
using System;
public class AdBuddizAds {

    bool _isInit = false;
    static AdBuddizAds _instance;

    public static AdBuddizAds GetInstance() {
        if( null == _instance ) {
            _instance = new AdBuddizAds();
        }

        return _instance;
    }
    public void init(string android_id,string iOS_id) {
        if( !_isInit) {
            AdBuddizBinding.SetLogLevel(AdBuddizBinding.ABLogLevel.Info);
            AdBuddizBinding.SetAndroidPublisherKey(android_id);
            AdBuddizBinding.SetIOSPublisherKey(iOS_id);
            _isInit = true;
        }

    }
}


