using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BYSDKManage {
public class SDKEventManager {
    #region SDK主动调用游戏的方法

    /// <summary>
    /// 获取分享内容
    /// </summary>
    /// <returns></returns>
    public delegate Dictionary<string, string> OnGetShareContentCallBack();

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public delegate void OnPlayBGMCallBack();

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public delegate void OnStopBGMCallBack();

    private static event OnGetShareContentCallBack OnGetShareCallBack;
    private static event OnPlayBGMCallBack onPlayBGMCallBack;
    private static event OnStopBGMCallBack onStopBGMCallBack;


    /// <summary>
    /// 设置分享的回调
    /// </summary>
    /// <param name="Event"></param>
    public static void SetShareContentCallBack(OnGetShareContentCallBack callback) {
        OnGetShareCallBack = callback;
    }

    /// <summary>
    /// 设置播放背景音乐回调
    /// </summary>
    /// <param name="callback"></param>
    public static void SetPlayBGMCallBack(OnPlayBGMCallBack callback) {
        onPlayBGMCallBack = callback;
    }

    /// <summary>
    /// 关闭背景音乐
    /// </summary>
    /// <param name="callback"></param>
    public static void SetStopBGMCallBack(OnStopBGMCallBack callback) {
        onStopBGMCallBack = callback;
    }

    /// <summary>
    /// 获取分享的内容
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> GetShareContent() {
        return OnGetShareCallBack();
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public static void PlayBGM() {
        onPlayBGMCallBack();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public static void StopBGM() {
        onStopBGMCallBack();
    }

    #endregion

    #region SDK通知游戏的相关事件
    public delegate void OnShareShowEvent();
    public delegate void OnShareCloseEvent();

    private static event OnShareShowEvent   onShareShowEvent;
    private static event OnShareCloseEvent  onShareCloseEvent;

    public static void SubscribShareShow(OnShareShowEvent shareShowEvent) {
        onShareShowEvent += shareShowEvent;
    }

    public static void NotifyShareShow() {
        if( null != onShareShowEvent)
            onShareShowEvent();
    }

    public static void SubscribShareClose(OnShareCloseEvent shareCloseEvent) {
        onShareCloseEvent += shareCloseEvent;
    }

    public static void NotifyShareClose() {
        onShareCloseEvent();
    }

    #endregion
}

public class ShareField {
    // 分享的标题
    public static readonly string TITLE = "Title";
    // 分享的内容
    public static readonly string CONTENT = "Content";
    // 下载链接
    public static readonly string DOWNLOAD_URL = "DownloadUrl";
    // 图片链接
    public static readonly string IMAGE_URL = "ImageUrl";
}
}
