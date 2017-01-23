using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour
{

    void Start()
    {
        HideSplash();
    }

    void HideSplash()
    {
#if UNITY_ANDROID
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call("HideSplash");
                }
            }
        }
#endif
    }
}
