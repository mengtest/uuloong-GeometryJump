using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;
using System.Collections;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.DB.Model
*
*	describe	:	N/A
*	class name	:	MageNodeManager
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/9 16:30:06				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/


public    class GameNodeManager {
    public event Action<UpdateResulte> OnUpdateComplete;
    Hashtable _gameNode;
    ConfStatus _status;

    public GameNodeManager() {
        _gameNode = new Hashtable();
        SetStatus(ConfStatus.NONE);
    }
    /// <summary>
    /// 从服务器拉取游戏节点配置数据
    /// </summary>
    /// <param name="order"></param>
    /// <param name="gameName"></param>
    /// <param name="country"></param>
    /// <param name="device"></param>
    public void UpdateGameNode(MonoBehaviour order,string gameName,int country,int device) {
        SetStatus(ConfStatus.UPDATING);
        HttpOperation.GetByHttp(order, SDKNetUtls.GetGameNodeConfig(gameName,country,device), OnSuccess, OnFailed);
    }

    public void OnSuccess(string msg) {
        var temp = JsonReader.Deserialize<List<GameNode>>(msg);
        GameNodeData data = new GameNodeData();
        data.Id = 1;
        data.value = msg;
        data.Save();
        _gameNode = new Hashtable();
        for (int i = 0; i < temp.Count; i++) {
            _gameNode.Add(temp[i].node_id, temp[i]);
        }
        if( _gameNode.Count > 0 ) {
            SetStatus(ConfStatus.NORMAL);
        } else {
            SetStatus(ConfStatus.NONE);
        }
        if (null != OnUpdateComplete) {
            var ret = new UpdateResulte();
            ret.tag = CheckList.StrategyUpdate;
            ret.status = true;
            OnUpdateComplete(ret);
        }
        OnUpdateComplete = null;
    }

    /// <summary>
    /// 从数据库获取数据
    /// </summary>
    public void InitGameNodeFromDB() {
        var data =  DBOperation.GetInstance().FindOneBySql<GameNodeData>("Id", 1);
        if( data == null || data.value == "") {
            SetStatus(ConfStatus.NONE);
            return;
        }
        var temp = JsonReader.Deserialize<List<GameNode>>(data.value);
        _gameNode = new Hashtable();
        for (int i = 0; temp != null && i < temp.Count; i++) {
            var t = temp[i];
            _gameNode.Add(t.node_id,t);
        }
        if( _gameNode.Count > 0 ) {
            SetStatus(ConfStatus.NORMAL);
        } else {
            SetStatus(ConfStatus.NONE);
        }
    }

    /// <summary>
    /// 获取游戏节点配置数据
    /// </summary>
    /// <returns></returns>
    public Hashtable GetGameNode () {
        return _gameNode;
    }
    public void OnFailed(string msg) {
        if( _gameNode.Count > 0) {      // 更新失败，但是有历史数据可用
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
        Debug.Log(" GameNode status: " + status.ToString());
        _status = status;
    }
}

