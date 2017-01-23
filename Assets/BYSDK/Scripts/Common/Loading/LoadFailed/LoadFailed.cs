using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.Common.AdsDataControl
*
*	describe	:	N/A
*	class name	:	NodeConfig
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/25 16:17:25				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/


public class LoadFailed : MonoBehaviour {
    public event Action CloseEvent;
    static readonly string title_zn = "加载失败";
    static readonly string title_en = "LOADING FAILED";

    static readonly string info_zn = "请稍后重试";
    static readonly string info_en = "Please try again later";

    static readonly string and_zn = "或者";
    static readonly string and_en = "or";

    static readonly string pointinfo_zn = "获取功能点";
    static readonly string pointinfo_en = "win Action Points";

    public Text TitleText;
    public Text InfoText;
    public Text AndText;
    public Text PointText;
    public LoadFailedTips Tips;
    bool IsChineseLanguage = false;
    // Use this for initialization
    void OnEnable() {
        IsChineseLanguage = SDKUtiles.GetLocationByLanguae();
        Init();
    }

    void Init() {
        if( IsChineseLanguage ) {
            TitleText.text = title_zn;
            InfoText.text = info_zn;
            AndText.text = and_zn;
            PointText.text = pointinfo_zn;
        } else {
            TitleText.text = title_en;
            InfoText.text = info_en;
            AndText.text = and_en;
            PointText.text = pointinfo_en;
        }
    }

    public void SetData(LoadFailedArgs args) {
        Tips.SetData(args);
        if( args == LoadFailedArgs.None ) {
            AndText.text = "";
            PointText.text = "";
        }
    }

    public void OnClose() {
        if(CloseEvent != null ) {
            CloseEvent();
            CloseEvent = null;
        }
        Destroy(this.gameObject);
    }
}

public enum LoadFailedArgs {
    All = 1,
    OnlyShare = 2,
    OnlyCommon = 3,
    None = 4
};