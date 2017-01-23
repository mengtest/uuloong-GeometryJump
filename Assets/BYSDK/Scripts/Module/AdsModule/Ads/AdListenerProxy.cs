#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
namespace bysdk
{
    public class AdListenerProxy : AndroidJavaProxy
    {
        private IAdListener listener;
        internal AdListenerProxy(IAdListener listener)
            : base("com.baiyigame.plugin.IAdListener")
        {
            this.listener = listener;
        }
        void onAdEvent(string adtype, string eventName, string paramString)
        {
            listener.onAdEvent(adtype, eventName, paramString);
        }

        string toString()
        {
            return "AdListenerProxy";
        }
    }
}
#endif