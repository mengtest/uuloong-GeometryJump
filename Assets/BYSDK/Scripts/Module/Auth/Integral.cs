using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pathfinding.Serialization.JsonFx;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.Auth
*
*	describe	:	N/A
*	class name	:	Integral
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/24 15:09:49				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public class Integral
{

    MonoBehaviour order;

    public Integral(MonoBehaviour _order)
    {
        order = _order;
        RegistEvent();
    }

    void RegistEvent()
    {
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.INTEGRAL_RANK, GetRank);
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.POINT_RULEID, SendPointByRuleID);
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.POINT_RULE, SendPoints);
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.GET_FESTIVAL, GetFestival);
    }

    #region 查询积分排行
    /// <summary>
    /// 查询积分排行
    /// </summary>
    void GetRank(SDKEventArgs args)
    {
        if (args.type == SDKEvent.INTEGRAL_RANK && args.status == EventStatus.ALLOW)
        {
            IntegralRank_Struct data = (IntegralRank_Struct)args.data;
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.IntegralRankURL, JsonWriter.Serialize(data), OnGetRankSuccess, OnGetRankFailed);
        }
    }

    void OnGetRankSuccess(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.INTEGRAL_RANK, EventStatus.SUCCESS, msg));
    }

    void OnGetRankFailed(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.INTEGRAL_RANK, EventStatus.FAILED, msg));
    }

    #endregion



    #region 根据积分规则id赠送积分
    /// <summary>
    /// 更具积分规则ID赠送积分
    /// </summary>
    void SendPointByRuleID(SDKEventArgs args)
    {
        if (args.type == SDKEvent.POINT_RULEID && args.status == EventStatus.ALLOW)
        {
            PointRuleID_Struct data = (PointRuleID_Struct)args.data;
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.SendPointByRuleURL, JsonWriter.Serialize(data), OnRuleIDSuccess, OnRuleIDFailed);
        }
    }

    void OnRuleIDSuccess(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.POINT_RULEID, EventStatus.SUCCESS, msg));
    }
    void OnRuleIDFailed(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.POINT_RULEID, EventStatus.FAILED, msg));
    }
    #endregion



    #region 根据规则赠送积分
    /// <summary>
    /// 根据规则赠送积分
    /// </summary>
    void SendPoints(SDKEventArgs args)
    {
        if (args.type == SDKEvent.POINT_RULE && args.status == EventStatus.ALLOW)
        {
            Point_Struct data = (Point_Struct)args.data;
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.SendPointByRuleURL, JsonWriter.Serialize(data), OnSendPointsSuccess, OnSendPointsFailed);
        }
    }


    void OnSendPointsSuccess(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.POINT_RULE, EventStatus.SUCCESS, msg));
    }


    void OnSendPointsFailed(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.POINT_RULE, EventStatus.FAILED, msg));
    }
    #endregion


    #region 获取活动
    /// <summary>
    /// 获取当前活动
    /// </summary>
    void GetFestival(SDKEventArgs args)
    {
        if (args.type == SDKEvent.GET_FESTIVAL && args.status == EventStatus.ALLOW)
        {
            Festival_Struct data = (Festival_Struct)args.data;
            string url = string.Format(SDKNetUtls.SendPointByRuleURL + "?gameid={0}&language={1}", data.gameid, data.language);
            HttpOperation.GetByHttp<string>(order, url, FestivalSuccess, FestivalFailed);
        }
    }

    void FestivalSuccess(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.GET_FESTIVAL, EventStatus.SUCCESS, msg));
    }


    void FestivalFailed(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.GET_FESTIVAL, EventStatus.SUCCESS, msg));
    }
    #endregion
}
