using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.Common
*
*	describe	:	N/A
*	class name	:	GameInfo
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/29 17:23:51				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public   class GameInfo {
    public GameInfo() {
#if UNITY_IHONE
        device = Device.IOS;
#elif UNITY_ANDROID
        device = Device.ANDROID;
#endif
    }
    /// <summary>
    /// 游戏名
    /// </summary>
    public string name {
        get;
        set;
    }

    /// <summary>
    /// 游戏id
    /// </summary>
    public string id {
        get;
        set;
    }

    /// <summary>
    /// 位置信息
    /// </summary>
    public Location location {
        get;
        set;
    }

    /// <summary>
    /// 设备平台
    /// </summary>
    public Device device {
        get;
        set;
    }
}

/// <summary>
/// 地理位置定义
/// </summary>
public enum Location {
    ALL = 1,
    ABROAD = 2,
    CHINA  = 3
}

/// <summary>
/// 设备平台定义
/// </summary>
public enum Device {
    ALL = 1,
    ANDROID = 2,
    IOS = 3
}