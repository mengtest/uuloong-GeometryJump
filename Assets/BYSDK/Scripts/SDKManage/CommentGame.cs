using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*********************************************************************//**
*   namespace   :   Assets.Scripts.SDKManage
*
*   describe    :   N/A
*   class name  :   CommentGame
*
*   Ver     |   change date         |       author          |       describe    |
*   --------|-----------------------|-----------------------|-------------------|
*   V0.01   |   2016/6/12 17:14:08              |       Mr.Li           |                   |
*
*   Copytight (c) 2015 Goodall  Corporation. All rights reserved.
*
*   |-----------------------------------------------------------------------|
*   |   以下信息为公司机密，未经本公司书面同意禁止向第三方披露             |
*   |   版权所有：百益技术有限公司                                           |
*   |-----------------------------------------------------------------------|
*
**********************************************************************//**/
namespace BYSDKManage {

/// <summary>
/// 游戏评论管理类，当前只负责IOS平台测评论
/// </summary>
public class CommentGame {
    public delegate void CommentGameHandler(ActionType type, ActionResulet result);
    public string appID;        // ITunes 申请的APP ID,即应用在AppStore 上的ID
    private static string commentStr = "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewSoftware?id={0}";
    private static string AndroidStr = "http://fir.im/r7xs?release_id=5774f24bf2fc42067600004f";
    CommentGameHandler CMHandler;
    /// <summary>
    ///  初始化函数
    /// </summary>
    /// <param name="ID"></param>
    public CommentGame(string ID) {
        appID = ID;
    }
    /// <summary>
    ///  跳转到AppStore
    /// </summary>
    public void Comment() {
        string str = string.Format("itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewSoftware?id={0}&mt=8", appID);
        Debug.Log(str);
        Application.OpenURL(str);
        if( null != CMHandler)
            CMHandler(ActionType.Comment, ActionResulet.Success);

    }

    public void Comment(string id) {
        string str = string.Format("itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewSoftware?id={0}&mt=8", id);
        Debug.Log(str);
        Application.OpenURL(str);
        if (null != CMHandler)
            CMHandler(ActionType.Comment, ActionResulet.Success);
    }

    public void setCMhandler(CommentGameHandler hanler) {
        CMHandler = hanler;
    }

    public string GetAppUrl() {
        string str;
#if UNITY_ANDROID
        str = AndroidStr;

#elif UNITY_IPHONE
        str = string.Format(commentStr, appID);
#endif
        return str;

    }
}
}
