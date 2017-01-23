#region 程序集 BYSDKManage, Version=1.0.0.0,
/*********************************************************************//**
*   namespace   :   Assets.Scripts.SDKManage
*
*   describe    :   N/A
*   class name  :   ActionResulet
*
*   Ver     |   change date         |       author          |       describe    |
*   --------|-----------------------|-----------------------|-------------------|
*   V0.01   |   2016/6/12 17:06:22              |       Mr.Li           |                   |
*
*   Copytight (c) 2015 Goodall  Corporation. All rights reserved.
*
*   |-----------------------------------------------------------------------|
*   |   以下信息为公司机密，未经本公司书面同意禁止向第三方披露             |
*   |   版权所有：百益技术有限公司                                           |
*   |-----------------------------------------------------------------------|
*   IOS系统自带分享调用管理
**********************************************************************//**/
#endregion
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace BYSDKManage {
#if UNITY_IPHONE
public class SYSShareIOS {
    [DllImport("__Internal")]
    private static extern void start(string text, string url);

    [DllImport("__Internal")]
    private static extern void startEmail(string emailStr);

    /// <summary>
    /// 调用IOS系统自带的分享系统
    /// </summary>
    /// <param name="textStr">分享的介绍语句</param>
    /// <param name="urlStr"> 应用下载链接</param>
    public static void OnShareForIOS(string textStr, string urlStr) {
        start(textStr, urlStr);
    }

    /// <summary>
    /// 调用邮件发送功能，让玩家联系我们
    /// </summary>
    public static void SendEmail() {
        startEmail(Constants.ConnectMeail);
    }
}
#endif
}
