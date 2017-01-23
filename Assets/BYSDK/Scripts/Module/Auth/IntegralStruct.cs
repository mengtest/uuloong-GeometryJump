using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.Auth
*
*	describe	:	N/A
*	class name	:	IntegralStruct
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/26 13:40:03				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

#region 查询积分排行
public struct IntegralRank_Struct {
    /// <summary>
    /// 游戏id
    /// </summary>
    public string gameid {
        get;
        set;
    }

    /// <summary>
    /// 登录后返回的 access_token
    /// </summary>
    public string access_token {
        get;
        set;
    }

    /// <summary>
    /// 语言类型
    /// </summary>
    public string language {
        get;
        set;
    }
}
#endregion

#region 根据积分规则ID赠送积分
public struct PointRuleID_Struct {

    /// <summary>
    /// 规则id
    /// </summary>
    public int ruleid {
        get;
        set;
    }

    /// <summary>
    /// 登录后返回的回话id
    /// </summary>
    public string access_token {
        get;
        set;
    }

    /// <summary>
    /// 语言
    /// </summary>
    public string language {
        get;
        set;
    }
}

#endregion


#region 根据积分规则赠送积分
public struct Point_Struct {

    /// <summary>
    /// 游戏id
    /// </summary>
    public string gameid {
        get;
        set;
    }

    /// <summary>
    /// 登录后返回的回话id
    /// </summary>
    public string access_token {
        get;
        set;
    }

    /// <summary>
    /// 规则类型(1.抽奖 2.邀请好友 3.节日活动 4.游戏时长 5.游戏分数 6.游戏关卡 7.游戏排名)
    /// </summary>
    public int ruletypekey {
        get;
        set;
    }

    /// <summary>
    /// 规则值：(1.抽中10积分 2.空 3.空 4.当日游戏时长：30min 5.当前游戏分数：100分 6.当前游戏关卡：10关 7.空)
    /// </summary>
    public string ruletypevalue {
        get;
        set;
    }

    /// <summary>
    /// 语言
    /// </summary>
    public string language {
        get;
        set;
    }
}
#endregion

#region 获取当前活动
public struct Festival_Struct {
    /// <summary>
    /// 游戏id
    /// </summary>
    public string gameid {
        get;
        set;
    }

    /// <summary>
    /// 语言
    /// </summary>
    public string language {
        get;
        set;
    }
}
#endregion