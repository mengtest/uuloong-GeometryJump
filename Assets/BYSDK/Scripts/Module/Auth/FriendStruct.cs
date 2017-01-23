using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.Auth
*
*	describe	:	N/A
*	class name	:	FriendStruct
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/26 14:04:03				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/


#region 邀请好友
public struct FriendMSG_Struct {
    /// <summary>
    /// 游戏id
    /// </summary>
    public long gameid {
        get;
        set;
    }

    /// <summary>
    /// 邀请人id ,标识发送邀请的用户id (BY会员)
    /// </summary>
    public long sendid {
        get;
        set;
    }

    /// <summary>
    /// 受邀人id ,标识需接受邀请的用户id (BY会员, 2选1)
    /// </summary>
    public long receiveid {
        get;
        set;
    }

    /// <summary>
    /// 受邀人别名 ,标识受邀人客户端的id (客户端别名, 2选1)
    /// </summary>
    public string clientid {
        get;
        set;
    }

    /// <summary>
    /// 消息标题
    /// </summary>
    public string title {
        get;
        set;
    }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string content {
        get;
        set;
    }

    /// <summary>
    /// 过期时间 (ex:"2016-10-01" or "2016-10-01T02:00:00")
    /// </summary>
    public string expiredtime {
        get;
        set;
    }
}
#endregion