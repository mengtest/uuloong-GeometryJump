using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.Common
*
*	describe	:	N/A
*	class name	:	ConfigKey
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/22 12:00:25				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
/// <summary>
/// 配置更新项
/// </summary>
public enum CheckList {
    ALL = 0,        // 更新所有
    StrategyUpdate = 1,
    GameNodeUpdate = 2,
    ConfigUpdate = 3
}


public class UpdateResulte {
    public CheckList tag {
        get;
        set;
    }
    public bool status {
        get;
        set;
    }
    public string error {
        get;
        set;
    }
}

/// <summary>
/// 配置项状态
/// </summary>
public enum ConfStatus {
    /// <summary>
    /// 正常可用
    /// </summary>
    NORMAL = 0,

    /// <summary>
    ///  更新中不可用
    /// </summary>
    UPDATING = 1,

    /// <summary>
    /// 没有数据，不可用
    /// </summary>
    NONE = 2
}


public enum ConfigKey {
    ShouldScore = 0,
    GoogleVideo = 1,
    AdsDataReport = 2,
    AdsDataManager = 3
}