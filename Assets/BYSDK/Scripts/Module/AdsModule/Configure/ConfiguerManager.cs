using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;
using System.Collections.Generic;
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


public class ConfiguerManager  {

    /// <summary>
    /// 游戏节点管理
    /// </summary>
    GameNodeManager gameNode;

    /// <summary>
    /// 策略管理
    /// </summary>
    StrategyManager strategy;

    /// <summary>
    /// 游戏配置管理
    /// </summary>
    GlobalConfig    globalConfig;

    AdsDataManager adsDataManager;
    /// <summary>
    /// 从服务器拉取的配置信息
    /// </summary>
    Hashtable       _checkData;

    /// <summary>
    /// 更新计数
    /// </summary>
    int             updateCount;

    /// <summary>
    ///  是否需要手动进行修改配置更新时间
    /// </summary>
    bool            needUpdateManner;

    /// <summary>
    /// 策略数据管理
    /// </summary>
    Configure _configure;

    MonoBehaviour   order;
    public ConfiguerManager(MonoBehaviour _order) {
        order = _order;
        _configure = new Configure();
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.ADS_CHECK, OnUpdateEvent);

    }

    /// <summary>
    ///  接收到容许检测更新时开始进行更新
    /// </summary>
    /// <param name="resulte"></param>
    public void OnUpdateEvent(SDKEventArgs resulte) {
        if( resulte.type == SDKEvent.ADS_CHECK && resulte.status == EventStatus.ALLOW) {
            SDKKener.GetInstance().UnRegistEventHandler(this, SDKEvent.ADS_CHECK);
            CheckUpdate();
        }
    }


    public void Init() {
        updateCount = 0;
        needUpdateManner = true;        // 默认需要进行更新。只有在游戏全局配置数据更新后，才不会进行更新
        gameNode = new GameNodeManager();
        gameNode.InitGameNodeFromDB();
        strategy = new StrategyManager();
        strategy.InitStrategyFromDB();
        globalConfig = new GlobalConfig();
        globalConfig.InitDataFromDB();
        adsDataManager = new AdsDataManager(order);
        gameNode.OnUpdateComplete += OnCompleteUpdate;
        strategy.OnUpdateComplete += OnCompleteUpdate;
        globalConfig.OnUpdateComplete += OnCompleteUpdate;
    }

    /// <summary>
    /// 检测是否需要进行更新
    /// 更新包括地理位置发生变化(由外部进行激发)、服务端配置数据发生变动、本地数据库没有配置数据 三个场景进行更新
    /// </summary>
    public void CheckUpdate(bool locationchange = false) {
        StartCheck();
        if ( globalConfig.GetGlobalConfig().Count > 0 && !locationchange) {
            Debug.Log("---> check config");
            HttpOperation.GetByHttp(order,
                                    SDKNetUtls.GetConfigUpdateTime(SDKKener.gameinfo.name,(int)SDKKener.gameinfo.location,(int)SDKKener.gameinfo.device),
                                    OnSuccess, OnFailed);
        } else {
            Debug.Log("---> update all");
            UpdateConfig(CheckList.ALL);
        }

    }

    public Hashtable GetGameNode() {
        return gameNode.GetGameNode();
    }

    public StrategyModel  GetStrategy() {
        return  strategy.GetStrategyModel();
    }

    /// <summary>
    /// 获取更新信息成功
    /// </summary>
    /// <param name="msg"></param>
    public void OnSuccess(string msg) {
        var _list = JsonReader.Deserialize<List<ConfigleModel>>(msg);
        _checkData = new Hashtable();
        for( int i = 0; i < _list.Count; i++) {
            _checkData.Add(_list[i].name, _list[i]);
        }
        bool ifNeedUpdate = false;

        for (int i = 1; i <= 3; i++) {              // 检测个配置信息更新时间
            CheckList value = (CheckList)i;
            if(!_checkData.ContainsKey(value.ToString())) {
                ifNeedUpdate = true;
                UpdateConfig(value);
            } else {
                var newdata = (ConfigleModel)_checkData[value.ToString()];
                var olddata = (ConfigleModel)globalConfig.GetGlobalConfig()[value.ToString()];
                if( olddata == null || newdata.date > olddata.date ) {
                    ifNeedUpdate = true;
                    UpdateConfig(value);
                }
            }
        }

        if( !ifNeedUpdate ) {               // 没有需要更新的时候直接发送通知
            var e = new SDKEventArgs();
            e.status = EventStatus.COMPLETE;
            e.type = SDKEvent.ADS_CHECK;
            SDKKener.GetInstance().EventNotict(e);
        }
    }

    /// <summary>
    /// 获取更新数据失败，发送失败消息
    /// </summary>
    /// <param name="msg"></param>
    public void OnFailed(string msg) {
        var e = new SDKEventArgs();
        e.status = EventStatus.FAILED;
        e.type = SDKEvent.ADS_CHECK;
        SDKKener.GetInstance().EventNotict(e);
    }


    /// <summary>
    /// 控制游戏进行更新,统计更新任务
    /// </summary>
    /// <param name="value"></param>
    private void UpdateConfig(CheckList value) {
        Debug.Log("UpdateConfig: " + value.ToString());
        switch(value) {
        case CheckList.ConfigUpdate:
            needUpdateManner = false;
            updateCount++;
            globalConfig.UpdateGlobalConfig(order, SDKKener.gameinfo.name, (int)SDKKener.gameinfo.location, (int)SDKKener.gameinfo.device);
            break;
        case CheckList.GameNodeUpdate:
            updateCount++;
            gameNode.UpdateGameNode(order, SDKKener.gameinfo.name, (int)SDKKener.gameinfo.location, (int)SDKKener.gameinfo.device);
            break;
        case CheckList.StrategyUpdate:
            updateCount++;
            strategy.UpdateStrategy(order, SDKKener.gameinfo.name, (int)SDKKener.gameinfo.location, (int)SDKKener.gameinfo.device);
            break;
        case CheckList.ALL:
            updateCount = 3;
            needUpdateManner = false;
            globalConfig.UpdateGlobalConfig(order, SDKKener.gameinfo.name, (int)SDKKener.gameinfo.location, (int)SDKKener.gameinfo.device);
            gameNode.UpdateGameNode(order, SDKKener.gameinfo.name, (int)SDKKener.gameinfo.location, (int)SDKKener.gameinfo.device);
            strategy.UpdateStrategy(order, SDKKener.gameinfo.name, (int)SDKKener.gameinfo.location, (int)SDKKener.gameinfo.device);
            break;
        }
    }

    /// <summary>
    /// 更新任务结束后进行回调
    /// </summary>
    /// <param name="tag">回调任务标志</param>
    /// <param name="isSuccess">是否成功</param>
    /// <param name="error"> 失败时返回错误信息</param>
    private void OnCompleteUpdate(UpdateResulte resulte) {
        if( updateCount < 0 ) {
            updateCount = 0;
        } else {
            updateCount--;
        }
        // 每返回一次，就更新该更新项的更新时间
        if( resulte.status && needUpdateManner) {
            var data = (ConfigleModel)_checkData[resulte.tag.ToString()];
            globalConfig.GetGlobalConfig()[resulte.tag.ToString()] = data;
            globalConfig.ifChanged = true;
        }

        if( updateCount == 0 && resulte.status) {
            var e = new SDKEventArgs();
            e.status = EventStatus.COMPLETE;
            e.type = SDKEvent.ADS_CHECK;
            SDKKener.GetInstance().EventNotict(e);
        } else if(!resulte.status) {                // 如果有内容下载失败，直接上报失败消息
            OnFailed("");
        }
        globalConfig.SaveData();
    }

    /// <summary>
    /// 开始进行更新时发送消息
    /// </summary>
    private void StartCheck() {
        var e = new SDKEventArgs();
        e.status = EventStatus.START;
        e.type = SDKEvent.ADS_CHECK;
        SDKKener.GetInstance().EventNotict(e);
    }

    /// <summary>
    /// 进行简单返回处理 - 测试状态
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public ConfigleModel GetConfig(ConfigKey key) {
        return globalConfig.GetConfigByKey(key);
    }
}
