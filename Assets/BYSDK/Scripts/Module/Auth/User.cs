using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.Auth
*
*	describe	:	N/A
*	class name	:	User
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/29 11:02:22				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public   class User {
    public LoginType type {
        get;
        set;
    }

    public string email {
        get;
        set;
    }

    public string token {
        get;
        set;
    }

    public string alias {
        get;
        set;
    }
}

/// <summary>
/// 玩家登录类型
/// </summary>
public enum LoginType {
    /// <summary>
    /// 游客登录
    /// </summary>
    Gust = 0,

    /// <summary>
    /// 百益账号登录
    /// </summary>
    BY   = 1,

    /// <summary>
    /// 第三方登录
    /// </summary>
    THIRD=2
}