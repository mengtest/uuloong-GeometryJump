using System;
using UnityEngine;

namespace bysdk
{
    // Interface for the methods to be invoked by the native plugin.
	internal interface IAdListener
    {
        void onAdEvent(string adtype,string eventName,string paramString);
    }
}
