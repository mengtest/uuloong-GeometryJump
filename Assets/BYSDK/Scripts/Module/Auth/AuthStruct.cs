using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.Auth
*
*	describe	:	N/A
*	class name	:	AuthStruct
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/24 17:44:03				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

// 通过邮箱找回密码
#region 通过邮箱找回密码
public struct GetPWDByEmail_Struct {
    public string email {
        get;
        set;
    }
}
#endregion


//通过邮件获取密码数据提交结构体
#region 修改密码
public struct ChangePWD_Struct {
    /// <summary>
    /// 注册邮箱
    /// </summary>
    public string email {
        get;
        set;
    }

    /// <summary>
    /// 验证码
    /// </summary>
    public string verification_code {
        get;
        set;
    }

    /// <summary>
    /// 新密码
    /// </summary>
    public string newpassword {
        get;
        set;
    }
}
#endregion

// 游客登录数据提交结构体
#region 游客登录数据提交结构体
public struct GustLogin_Struct {

    /// <summary>
    /// 手机唯一识别码
    /// </summary>
    public string client_code {
        get;
        set;
    }

    /// <summary>
    /// 游戏id
    /// </summary>
    public string gameid {
        get;
        set;
    }

    /// <summary>
    /// 用于消息推送的客户标示
    /// </summary>
    public string clientid {
        get;
        set;
    }
}
#endregion


#region 登录结果返回
public struct LoginData {
    public string token {
        get;
        set;
    }

    /// <summary>
    /// 用于推送的别名
    /// </summary>
    public string alias {
        get;
        set;
    }
}

#endregion

// 注册数据提交结构体
#region Regist_struct
public struct Regist_Struct {
    /// <summary>
    /// 注册邮箱
    /// </summary>
    public string email {
        get;
        set;
    }

    /// <summary>
    /// 手机唯一识别码
    /// </summary>
    public string client_code {
        get;
        set;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string username {
        get;
        set;
    }

    /// <summary>
    /// 密码
    /// </summary>
    public string password {
        get;
        set;
    }
}
#endregion



/// 百益用户登录
#region BY_Login
public struct BY_Login {
    /// <summary>
    /// 游戏id
    /// </summary>
    public string gameid {
        get;
        set;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string username {
        get;
        set;
    }

    /// <summary>
    /// 密码
    /// </summary>
    public string password {
        get;
        set;
    }

    /// <summary>
    /// 用于消息推送
    /// </summary>
    public string clientid {
        get;
        set;
    }
}
#endregion

// 第三方登录
#region ThirdParty_struct
public struct ThirdParty_struct {

    /// <summary>
    /// 第三方登录返回的 access_token
    /// </summary>
    public string access_token {
        get;
        set;
    }

    /// <summary>
    /// 登录类型 facebook，google
    /// </summary>
    public string login_type {
        get;
        set;
    }

    /// <summary>
    /// 游戏id
    /// </summary>
    public string gameid {
        get;
        set;
    }

    /// <summary>
    /// 用于消息推送
    /// </summary>
    public string clientid {
        get;
        set;
    }
}
#endregion