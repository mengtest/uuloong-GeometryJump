using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.Common.AdsConfig
*
*	describe	:	N/A
*	class name	:	AdsIndividualData
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/1 11:10:26	|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

// 广告个性化配置数据实体
public  class AdsIndividualData {

    /// <summary>
    ///  每条广告显示间隔时长 - s
    /// </summary>
    long TimeWindow;

    /// <summary>
    /// 上次显示广告的时间
    /// </summary>
    long LastShowTime;

    /// <summary>
    /// 每次游戏显示次数
    /// </summary>
    int ShowTimesPerDay;

    /// <summary>
    /// 已经显示的次数
    /// </summary>
    int ShowTimes;

    /// <summary>
    /// 广告类型标记，用以记录上次播放时间
    /// </summary>
    string TagStr;

    /// <summary>
    /// 是否使用标记进行数据存取
    /// </summary>
    bool IsTagUsefull;
    /// <summary>
    /// 个性化陪配置数据初始化
    /// </summary>
    /// <param name="Interval"> 显示时间间隔 </param>
    /// <param name="LimitCount"> 显示次数限制 </param>
    public AdsIndividualData(long Interval,int LimitCount,string tag = "", bool isUseTag = false) {
        TimeWindow = Interval;
        ShowTimesPerDay = LimitCount;
        TagStr = tag;
        IsTagUsefull = isUseTag;
        LastShowTime = 0;
        ShowTimes = 0;
        GetLastShowTimeByTagStr();
    }

    /// <summary>
    ///  获取上次保存的播放时时间
    /// </summary>
    void GetLastShowTimeByTagStr() {
        if (IsTagUsefull) {
            BYLog.Log("Before GetLastShowTimeByTagStr " + TagStr + " " + LastShowTime.ToString());
            LastShowTime = long.Parse(PlayerPrefs.GetString(TagStr, "0"));
            BYLog.Log("After GetLastShowTimeByTagStr " + TagStr + " " + LastShowTime.ToString());
        }
    }

    /// <summary>
    /// 保存本次播放时间
    /// </summary>
    void SaveLastShowTimeByTagStr() {
        if (IsTagUsefull) {
            BYLog.Log("SaveLastShowTimeByTagStr  " + TagStr + " " + LastShowTime.ToString() + " long type: " + LastShowTime);
            PlayerPrefs.SetString(TagStr, LastShowTime.ToString());
        }
    }
    /// <summary>
    /// 记录广告显示次数，同时记录广告显示的时间节点
    /// </summary>
    public void UpdateShowCount() {
        ShowTimes++;
        LastShowTime = SDKUtiles.GetSeconds();
        SaveLastShowTimeByTagStr();
    }

    /// <summary>
    /// 判断时间窗是否开启
    /// </summary>
    /// <returns> true - 开启 ，fals - 未开启 </returns>
    public bool IsTimeWindOpen() {
        if (LastShowTime <= 0)
            return true;
        if (ShowTimesPerDay >= 1 && ShowTimes >= ShowTimesPerDay)
            return false;
        return SDKUtiles.IsTimeWindowOpen(LastShowTime, TimeWindow);
    }
}

