using UnityEngine;
using System.Collections;
using System;
using System.Threading;

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


public class SDKKener  {
    private static SDKKener     _instance;
    public static User          Muser = new User();
    public static GameInfo      gameinfo = new GameInfo();
    private  EventManage        _eventer;
    ConfiguerManager            _sdkConfig;
    AuthManage                  _authManage;
    MonoBehaviour               order;
    public  bool                isChina;



    SDKKener() {
        gameinfo.location = Location.ABROAD;        //默认位置在国外
        _eventer = new EventManage();
    }

    void Init() {
        _authManage = new AuthManage(order);
        _sdkConfig = new ConfiguerManager(order);
        _sdkConfig.Init();
    }
    public static  SDKKener GetInstance() {
        if( null == _instance) {
            _instance = new SDKKener();
        }

        return _instance;
    }

    public void SetOrder(MonoBehaviour _order) {
        order = _order;
        Init();
    }
    /// <summary>
    /// 注册关注的事件
    /// </summary>
    /// <param name="order">事件注册者</param>
    /// <param name="type">事件类型</param>
    /// <param name="handle">响应事件的句柄</param>
    /// <returns></returns>
    public bool  RegistEventHandler(object order, SDKEvent type, Action<SDKEventArgs> handle ,bool once = true) {
        Debug.Log("Regist event: " + type.ToString());
        return _eventer.RegistEventHadle(order,type,handle,once);
    }

    /// <summary>
    /// 取消注册
    /// </summary>
    /// <param name="order">事件注册者</param>
    /// <param name="type">事件类型</param>
    public void UnRegistEventHandler(object order, SDKEvent type) {
        _eventer.UnRegistEventHandle(order, type);
    }

    /// <summary>
    /// 消息通知
    /// </summary>
    /// <param name="resulte"></param>
    public void EventNotict(SDKEventArgs resulte) {
        Debug.Log("==== > Event: " + resulte.type.ToString() + "  ===> status: " + resulte.status.ToString());
        _eventer.EventNotice(resulte);
    }

    /// <summary>
    /// 通过键值获取配置数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public ConfigleModel GetConfigByKey(ConfigKey key) {
        return _sdkConfig.GetConfig(key);
    }

    /// <summary>
    /// 获取游戏节点
    /// </summary>
    /// <returns></returns>
    public Hashtable GetGameNode() {
        return _sdkConfig.GetGameNode();
    }

    /// <summary>
    /// 获取广告策略文件
    /// </summary>
    /// <returns></returns>
    public StrategyModel GetStrategy() {
        return _sdkConfig.GetStrategy();
    }

    /// <summary>
    /// 设置位置信息
    /// </summary>
    public void SetLocation(bool _isChina) {
        isChina = _isChina;
#if UNITY_ADNROID
        gameinfo.device =Device.ANDROID;
#elif UNITY_IPHONE
        gameinfo.device = Device.IOS;
#endif
        gameinfo.name = Application.productName.Replace(" ","").ToLower();
        if ( _isChina ) {
            gameinfo.location = Location.CHINA;
        } else {
            gameinfo.location = Location.ABROAD;
        }
        _instance.EventNotict(new SDKEventArgs(SDKEvent.LOCATION, EventStatus.COMPLETE,_isChina));
        _instance.EventNotict(new SDKEventArgs(SDKEvent.ADS_CONFIGURE, EventStatus.ALLOW));    // 通知广告初始化
    }

    public string GameID() {
        return "1";
    }
}
