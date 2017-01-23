/*********************************************************************//**
*	namespace	:	Assets.Scripts.SDKManage
*
*	describe	:	N/A
*	class name	:	ActionResulet
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/6/12 17:06:22				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*   控制分享界面响应
**********************************************************************//**/
using UnityEngine;
using cn.sharesdk.unity3d;
using PathologicalGames;
using System.Collections.Generic;
using DG.Tweening;

namespace BYSDKManage {
public class ShareControl : MonoBehaviour {

    CanvasGroup alphaControl;
    void OnEnable() {
        alphaControl = gameObject.GetComponent<CanvasGroup>();
        BYSDKManage.SDKEventManager.NotifyShareShow();
        FadeIn();
    }

    void FadeIn() {
        alphaControl.alpha = 0.1f;
        DOTween.To(()=>alphaControl.alpha,x =>alphaControl.alpha = x ,1f,1f);
    }

    void FadeOut() {
        alphaControl.alpha = 1;
        DOTween.To(() => alphaControl.alpha, x => alphaControl.alpha = x, 0.1f, 1f).OnComplete(Close);
    }

    public void OnShareBtnClick(GameObject go) {
        Dictionary<string, string> shareDic = BYSDKManage.SDKEventManager.GetShareContent();
        string title = shareDic[ShareField.TITLE];
        string content = shareDic[ShareField.CONTENT];
        string downloadUrl = shareDic[ShareField.DOWNLOAD_URL];
        string imageUrl = shareDic[ShareField.IMAGE_URL];
        Debug.Log(content);

        PlatformType? platformType = null;
        string tag = go.name;
        if (tag.Contains("WeChat")) {
            platformType = PlatformType.WeChat;
        } else if (tag.Contains("WechatMoments")) {
            platformType = PlatformType.WeChatMoments;
        } else if (tag.Contains("QQ")) {
            platformType = PlatformType.QQ;
        } else if (tag.Contains("Qzone")) {
            platformType = PlatformType.QZone;
        } else if (tag.Contains("Sina")) {
            platformType = PlatformType.SinaWeibo;
        } else if (tag.Contains("Twitter")) {
            platformType = PlatformType.Twitter;
        } else if (tag.Contains("FaceBook")) {
            platformType = PlatformType.Facebook;
        } else if (tag.Contains("GooglePlus")) {
            platformType = PlatformType.GooglePlus;
        } else if (tag.Contains("ShortMessage")) {
            platformType = PlatformType.SMS;
        } else if (tag.Contains("Email")) {
            platformType = PlatformType.Mail;
        } else if (tag.Contains("More")) {
            ShareManage._instance.ShareGameByOS(content, downloadUrl);
        }

        if (platformType != null) {
            ShareManage._instance.ShareTo(platformType.Value, title, content, downloadUrl, imageUrl);
        }
        Close();
    }

    public void OnCancel() {
        FadeOut();
    }

    void Close() {
//        BYSDKManage.SDKEventManager.NotifyShareClose();
//        transform.SetParent(null);
        PoolManager.Pools["UIPool"].Despawn(this.transform);
    }
}
}