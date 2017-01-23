using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pathfinding.Serialization.JsonFx;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.Configure
*
*	describe	:	N/A
*	class name	:	GlobalConfig
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/19 16:56:14				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public  class GlobalConfig {
    public event Action<UpdateResulte> OnUpdateComplete;
    Hashtable       _configData;
    ConfStatus      _status;
    public bool     ifChanged;              // 判断是否因为更新修改了部分值
    ConfigleModel   adsDataPostDate;        // 广告数据上报时间记录 - 与其他服务端配置分开存取
    public GlobalConfig() {
        _configData = new Hashtable();
        ifChanged = false;
        SetStatus(ConfStatus.NONE);
        adsDataPostDate = new ConfigleModel();
        adsDataPostDate.name = AdsDataManager.Name();
        adsDataPostDate.date = 0;
        adsDataPostDate.data = "";
        adsDataPostDate.value = 0;
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.ADS_DATA_REPORT, OnAdsDataReport);
    }
    /// <summary>
    /// 通过服务器拉取配置数据
    /// </summary>
    /// <param name="order"></param>
    /// <param name="gameName"></param>
    /// <param name="country"></param>
    /// <param name="device"></param>
    public void UpdateGlobalConfig(MonoBehaviour order,string gameName,int country,int device) {
        SetStatus(ConfStatus.UPDATING);
        HttpOperation.GetByHttp(order, SDKNetUtls.GetGameConfigureData(gameName, country, device), OnSuccess, OnFailed);
    }

    /// <summary>
    /// 广告数据上报完成之后，保存上报的时间
    /// </summary>
    /// <param name="args"></param>
    public void OnAdsDataReport(SDKEventArgs args) {
        if (args.type == SDKEvent.ADS_DATA_REPORT && args.status == EventStatus.COMPLETE) {
            adsDataPostDate.date = long.Parse(SDKUtiles.GetTimeStamp());
        }
    }
    /// <summary>
    /// 成功回调
    /// </summary>
    /// <param name="mesg"></param>
    private void OnSuccess(string msg) {
        Debug.Log(msg);
        List<ConfigleModel> _list = JsonReader.Deserialize<List<ConfigleModel>>(msg);
        _configData = new Hashtable();


        for ( int i = 0; i < _list.Count; i++ ) {
            if( _list[i].name != AdsDataManager.Name()) {
                _configData.Add(_list[i].name, _list[i]);
            } else {
                adsDataPostDate = _list[i];
            }
        }
        if( _configData.Count > 0) {
            SetStatus(ConfStatus.NORMAL);
            ifChanged = true;
        } else {
            SetStatus(ConfStatus.NONE);
        }
        SaveData();
        if (null != OnUpdateComplete) {
            var ret = new UpdateResulte();
            ret.tag = CheckList.StrategyUpdate;
            ret.status = true;
            OnUpdateComplete(ret);
        }
        OnUpdateComplete = null;
    }

    public Hashtable GetGlobalConfig() {
        return (Hashtable)_configData.Clone();
    }

    /// <summary>
    /// 保存修改后的数据
    /// </summary>
    /// <returns></returns>
    public bool SaveData() {
        if (_configData.Count <= 0 || !ifChanged)
            return false;
        var data = new ConfigleData();
        data.Id = 1;
        List<ConfigleModel> _list = new List<ConfigleModel>();
        foreach(string keys in _configData.Keys) {
            _list.Add((ConfigleModel)_configData[keys]);
        }
        _list.Add(adsDataPostDate);
        data.value = JsonWriter.Serialize(_list);
        return data.Save();
    }

    /// <summary>
    ///  从数据库获取数据并进行初始化
    /// </summary>
    public void InitDataFromDB() {
        var data = DBOperation.GetInstance().FindOneBySql<ConfigleData>("Id", 1);
        if(data == null ) {
            SetStatus(ConfStatus.NONE);
            return;
        }
        List<ConfigleModel> _list = JsonReader.Deserialize<List<ConfigleModel>>(data.value);
        _configData = new Hashtable();
        for (int i = 0; _list != null && i < _list.Count; i++) {
            _configData.Add(_list[i].name, _list[i]);
        }
        if (_configData.Count > 0) {
            SetStatus(ConfStatus.NORMAL);
        } else {
            SetStatus(ConfStatus.NONE);
        }
    }
    /// <summary>
    /// 失败回调
    /// </summary>
    /// <param name="mesg"></param>
    private void OnFailed(string msg) {
        if (_configData.Count > 0) {
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
        Debug.Log(" GlobalConfig status: " + status.ToString());
        _status = status;
    }

    public ConfigleModel GetConfigByKey(ConfigKey key) {
        if( ConfigKey.AdsDataManager  == key) {
            return adsDataPostDate;
        } else {
            return (ConfigleModel)_configData[key.ToString()];
        }
    }
}

