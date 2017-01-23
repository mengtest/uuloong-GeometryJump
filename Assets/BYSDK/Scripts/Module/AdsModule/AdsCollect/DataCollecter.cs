using UnityEngine;
using System.Collections;
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


public class DataCollecter  {
    Action<AdsDBData>   DataSave;
    AdsDBData data;
    long        startTime;
    public DataCollecter(Vendors type,SDKAdsType adstype) {
        data = new AdsDBData();
        data.vendors = (int)type;
        data.adstype = (int)adstype;
        DataSave += AdsDataManager.SaveAdsData;
    }

    /// <summary>
    /// 广告出错
    /// </summary>
    /// <param name="ifFailed"></param>
    /// <param name="msg"></param>
    public void Failed(string msg = "") {
        data.if_failed = 1;
        data.info = msg;
        SaveData();
    }

    public void AdsShow() {
        if (startTime > 0) {
            SaveData();
        }
        data.if_display = 1;
        startTime = SDKUtiles.GetSeconds();
    }

    public void AdsClick() {
        data.if_clicked = 1;
    }

    public void AdsClosed() {
        data.duration = SDKUtiles.GetSeconds() - startTime;
        SaveData();
    }
    /// <summary>
    /// 保存数据
    /// </summary>
    void SaveData() {
        if( null != DataSave ) {
            DataSave(data);
        }
        Clear();
    }

    void Clear() {
        data.duration = 0;
        data.if_clicked = 0;
        data.if_display = 0;
        data.if_download = 0;
        data.if_failed = 0;
        data.info = "";
        startTime = 0;
    }
}
