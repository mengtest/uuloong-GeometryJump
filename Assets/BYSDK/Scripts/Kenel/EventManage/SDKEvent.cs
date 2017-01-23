using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.Common
*
*	describe	:	N/A
*	class name	:	SDKEvent
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/22 11:50:33				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
public class EventObject {
    /// <summary>
    /// 事件注册者
    /// </summary>
    public object order {
        get;
        set;
    }

    /// <summary>
    /// 是否只注册一次。即触发后立即清除
    /// </summary>
    public bool RegistOnce {
        get;
        set;
    }
    /// <summary>
    /// 回调句柄
    /// </summary>
    public Action<SDKEventArgs> handler {
        get;
        set;
    }
}

public class SDKEventArgs {
    public SDKEventArgs() {

    }
    public SDKEventArgs(SDKEvent t, EventStatus st, object obj = null) {
        type = t;
        status = st;
        data = obj;
    }
    /// <summary>
    /// 事件类型
    /// </summary>
    public SDKEvent type {
        get;
        set;
    }


    /// <summary>
    /// 事件结果
    /// </summary>
    public EventStatus status {
        get;
        set;
    }

    /// <summary>
    /// 事件附带信息
    /// </summary>
    public object data {
        get;
        set;
    }
}

/// <summary>
/// 事件状态
/// </summary>
public enum EventStatus {
    START = 0,              // 事件发生
    FAILED = 1,             // 失败
    COMPLETE = 2,           // 事件完成
    ALLOW = 3,              // 容许事件执行
    SUCCESS = 4             // 成功
}

/// <summary>
/// 事件定义
/// </summary>
public enum SDKEvent {
    CHECKVISON = 0,             // 检测版本
    GAME_UPDATE = 1,            // 更新游戏
    ADS_CHECK = 2,              // 广告检测
    ADS_DATA_REPORT = 3,        // 广告收集数据上报
    ADS_DATASAVE = 4,            // 保存广告手机数据

    /// <summary>
    /// 通过邮件获取密码
    /// </summary>
    GET_PWD_EMAIL = 5,

    /// <summary>
    /// 修改密码
    /// </summary>
    CHANGE_PWD = 6,

    /// <summary>
    /// 游客登录
    /// </summary>
    GUST_LOGIN = 7,

    /// <summary>
    /// 百益账号登录
    /// </summary>
    BYLOGIN = 8,

    /// <summary>
    /// 第三方登录
    /// </summary>
    THRID_LOGIN = 9,

    /// <summary>
    /// 积分排行
    /// </summary>
    INTEGRAL_RANK = 10,

    /// <summary>
    /// 根据积分规则ID赠送积分
    /// </summary>
    POINT_RULEID = 11,

    /// <summary>
    /// 根据积分规则赠送积分
    /// </summary>
    POINT_RULE = 12,

    /// <summary>
    /// 获取获得
    /// </summary>
    GET_FESTIVAL = 13,

    /// <summary>
    /// 给好友推送消息
    /// </summary>
    PUSGMSG_FRIEND = 14,

    /// <summary>
    /// 注册用户
    /// </summary>
    REGIST_USER = 15,

    /// <summary>
    /// 广告配置管理
    /// </summary>
    ADS_CONFIGURE = 16,

    /// <summary>
    /// 广告初始化
    /// </summary>
    ADS_INIT = 17,

    /// <summary>
    /// 位置信息事件
    /// </summary>
    LOCATION = 18,

    /// <summary>
    /// 网络状态事件
    /// </summary>
    NET_STATUS = 19
}
