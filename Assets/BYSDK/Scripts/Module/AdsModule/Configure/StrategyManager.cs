using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.Configure
*
*	describe	:	N/A
*	class name	:	StrategyManager
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/9 16:14:33				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public  class StrategyManager {
    public event Action<UpdateResulte> OnUpdateComplete;
    private StrategyModel _strategy;
    ConfStatus _status;

    public StrategyManager() {
        _strategy = new StrategyModel();
        SetStatus(ConfStatus.NONE);
    }
    /// <summary>
    /// 通过网络更新ads策略
    /// </summary>
    /// <param name="gameName"></param>
    /// <param name="counrty"></param>
    /// <param name="device"></param>
    public void UpdateStrategy(MonoBehaviour order, string gameName,int counrty,int device) {
        SetStatus(ConfStatus.UPDATING);
        HttpOperation.GetByHttp(order, SDKNetUtls.GetStrategy(SDKKener.gameinfo.name, (int)SDKKener.gameinfo.location, (int)SDKKener.gameinfo.device), OnSuccess, OnFailed);
    }

    /// <summary>
    /// 从数据库中获取策略信息
    /// </summary>
    public void InitStrategyFromDB() {
        Ads_Strategy data =  DBOperation.GetInstance().FindOneBySql<Ads_Strategy>("Id",1);
        if( data == null ) {
            SetStatus(ConfStatus.NONE);
            return;
        }
        _strategy = JsonReader.Deserialize<StrategyModel>(data.Strategy);
        if( _strategy != null || _strategy.CanUse() ) {
            SetStatus(ConfStatus.NORMAL);
        } else {
            SetStatus(ConfStatus.NONE);
        }
    }

    public StrategyModel GetStrategyModel() {
        if (_status == ConfStatus.NORMAL) {
            return _strategy;
        } else {
            return null;
        }
    }

    /// <summary>
    /// 策略获取成功，保存并存入数据库.
    /// 处理好数据之后需要对外发送消息
    /// </summary>
    /// <param name="msg"></param>
    void OnSuccess(string msg) {
        _strategy = JsonReader.Deserialize<StrategyModel>(msg);
        Ads_Strategy temp = new Ads_Strategy();
        temp.Id = 1;
        temp.Strategy = msg;
        temp.Save();

        if (_strategy != null || _strategy.CanUse()) {
            SetStatus(ConfStatus.NORMAL);
        } else {
            SetStatus(ConfStatus.NONE);
        }

        if ( null != OnUpdateComplete ) {
            var ret = new UpdateResulte();
            ret.tag = CheckList.StrategyUpdate;
            ret.status = true;
            OnUpdateComplete(ret);
        }
        OnUpdateComplete = null;

    }

    void OnFailed(string msg) {
        if (_strategy != null || _strategy.CanUse()) {
            SetStatus(ConfStatus.NORMAL);
        } else {
            SetStatus(ConfStatus.NONE);
        }

        if (null != OnUpdateComplete) {
            var ret = new UpdateResulte();
            ret.tag = CheckList.StrategyUpdate;
            ret.status = false;
            ret.error = msg;
            OnUpdateComplete(ret);
        }
        OnUpdateComplete = null;
    }

    /// <summary>
    /// 获取配置项状态
    /// </summary>
    /// <returns></returns>
    public ConfStatus GetStatus() {
        return _status;
    }

    /// <summary>
    /// 设置配置项状态
    /// </summary>
    /// <param name="status"></param>
    void SetStatus(ConfStatus status) {
        Debug.Log(" Strategy status: " + status.ToString());
        _status = status;
    }
}
