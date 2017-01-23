using UnityEngine;
using System.Collections;

public class InMoBi  {

    /// <summary>
    ///  获取InMoBiAds 对象
    /// </summary>
    /// <returns></returns>
    public static InMoBiAds GetInMoBiAds() {
        InMoBiAds ret;
        CreateEventManage();
#if UNITY_IPHONE
        ret = new InMoBiIOS(AdsConfig.ACCID);
#elif UNITY_ANDROID
        ret = new InMoBiAndroid(AdsConfig.ACCID);
#endif
        ret.init(AdsConfig.GetInMoBiData());
        return ret;
    }


    public static void CreateEventManage() {

#if UNITY_IPHONE
        GameObject go = new GameObject("InMobiManager");
        go.AddComponent<InMobiManager>();
#elif UNITY_ANDROID
        GameObject go = new GameObject("InMobiAndroidManager");
        go.AddComponent<InMobiAndroidManager>();
#endif


    }
}
