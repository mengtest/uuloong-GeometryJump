using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts
*
*	describe	:	N/A
*	class name	:	ConfigleModule
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/8 16:13:25				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/


/// 游戏控制参数 该数据存入数据库中
public   class ConfigleData {
    /// <summary>
    /// 数据库主键
    /// </summary>
    public int Id {
        get;
        set;
    }


    /// <summary>
    /// 属性
    /// </summary>
    public string value {
        get;
        set;
    }

    public bool Save() {
        return  DBOperation.GetInstance().Save<ConfigleData>(this);
    }
}

public class ConfigleModel {

    /// <summary>
    ///  键值
    /// </summary>
    public string name {
        get;
        set;
    }

    /// <summary>
    /// 配置参数1
    /// </summary>
    public int value {
        get;
        set;
    }

    /// <summary>
    /// 配置参数2
    /// </summary>
    public string data {
        get;
        set;
    }
    /// <summary>
    /// 更新时间
    /// </summary>
    public long date {
        get;
        set;
    }
}

public enum CheckEnum {
    /// <summary>
    /// 游戏控制
    /// </summary>
    CONGIGUER = 1,

    /// <summary>
    /// 广告策略
    /// </summary>
    STRATEGY = 2,

    /// <summary>
    /// 游戏节点
    /// </summary>
    GAMENODE = 3
}