using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;
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


public class Auth
{

    MonoBehaviour order;

    public Auth(MonoBehaviour _order)
    {
        order = _order;
        RegistEvent();
    }

    void RegistEvent()
    {
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.GET_PWD_EMAIL, GetbackPasswordByEmail);
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.CHANGE_PWD, ChangePassword);
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.GUST_LOGIN, GustLogin);
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.REGIST_USER, Register);
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.BYLOGIN, BYLogin);
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.THRID_LOGIN, ThirdPartyLogin);
    }

    /// <summary>
    /// 通过邮件获取登录密码
    /// </summary>
    #region
    void GetbackPasswordByEmail(SDKEventArgs args)
    {
        if (args.type == SDKEvent.GET_PWD_EMAIL && args.status == EventStatus.ALLOW)
        {
            GetPWDByEmail_Struct data = (GetPWDByEmail_Struct)args.data;
            if (!SDKUtiles.IsEmail(data.email))
            {
                SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.GET_PWD_EMAIL, EventStatus.FAILED, "Email address error"));
            }
            else {
                HttpOperation.PostByHttp<string>(order, SDKNetUtls.GetPwdByEmailURL, JsonWriter.Serialize(data), OnGetBackPWDSuccess, OnGetBackPWDFailed);
            }
        }

    }

    /// <summary>
    /// 密码发送成功，发送成功消息
    /// </summary>
    /// <param name="msg"></param>
    void OnGetBackPWDSuccess(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.GET_PWD_EMAIL, EventStatus.SUCCESS, msg));
    }

    /// <summary>
    /// 密码发送失败，发送失败消息
    /// </summary>
    /// <param name="msg"></param>
    void OnGetBackPWDFailed(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.GET_PWD_EMAIL, EventStatus.FAILED, msg));
    }
    #endregion


    #region 修改密码
    /// <summary>
    /// 修改密码
    /// </summary>
    void ChangePassword(SDKEventArgs args)
    {
        if (args.type == SDKEvent.CHANGE_PWD && args.status == EventStatus.ALLOW)
        {
            ChangePWD_Struct data = (ChangePWD_Struct)args.data;
            string dataStr = JsonWriter.Serialize(data);
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.ForgetPwdURL, dataStr, OnChangePWDSuccess, OnChangePWDFailed);
        }
    }

    /// <summary>
    /// 修改成功
    /// </summary>
    /// <param name="msg"></param>
    void OnChangePWDSuccess(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.CHANGE_PWD, EventStatus.SUCCESS, msg));
    }

    /// <summary>
    /// 修改失败
    /// </summary>
    /// <param name="error"></param>
    void OnChangePWDFailed(string error)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.CHANGE_PWD, EventStatus.FAILED, error));
    }

    #endregion



    #region 游客登录
    /// <summary>
    ///  游客登录
    /// </summary>
    void GustLogin(SDKEventArgs args)
    {
        if (args.type == SDKEvent.GUST_LOGIN && args.status == EventStatus.ALLOW)
        {
            GustLogin_Struct data = (GustLogin_Struct)args.data;
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.GustLoginURL, JsonWriter.Serialize(data), OnGustLogin, OnGustLoginFailed);
        }
    }

    void OnGustLogin(string msg)
    {
        var data = JsonReader.Deserialize<LoginData>(msg);
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.GUST_LOGIN, EventStatus.SUCCESS, data));
    }

    void OnGustLoginFailed(string error)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.GUST_LOGIN, EventStatus.FAILED, error));
    }

    #endregion


    #region 用户注册
    /// <summary>
    /// 用户注册
    /// </summary>
    void Register(SDKEventArgs args)
    {
        if (args.type == SDKEvent.REGIST_USER && args.status == EventStatus.ALLOW)
        {
            Regist_Struct data = (Regist_Struct)args.data;
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.RegisterURL, JsonWriter.Serialize(data), OnRegistSuccess, OnRegistFailed);
        }
    }

    void OnRegistSuccess(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.REGIST_USER, EventStatus.SUCCESS, msg));
    }

    void OnRegistFailed(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.REGIST_USER, EventStatus.FAILED, msg));
    }

    #endregion


    #region 百益账号登录
    /// <summary>
    ///  百益账户登录
    /// </summary>
    void BYLogin(SDKEventArgs args)
    {
        if (args.type == SDKEvent.BYLOGIN && args.status == EventStatus.ALLOW)
        {
            BY_Login data = (BY_Login)args.data;
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.BYLoginURL, JsonWriter.Serialize(data), OnBYLoginSuccess, OnBYLoginFailed);
        }
    }

    void OnBYLoginSuccess(string msg)
    {
        var data = JsonReader.Deserialize<LoginData>(msg);
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.BYLOGIN, EventStatus.SUCCESS, data));
    }

    void OnBYLoginFailed(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.BYLOGIN, EventStatus.FAILED, msg));
    }

    #endregion


    #region 第三方登录
    /// <summary>
    ///  第三方登录
    /// </summary>
    void ThirdPartyLogin(SDKEventArgs args)
    {
        if (args.type == SDKEvent.THRID_LOGIN && args.status == EventStatus.ALLOW)
        {
            ThirdParty_struct data = (ThirdParty_struct)args.data;
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.ThirfPartyURL, JsonWriter.Serialize(data), OnThirdLoginSuccess, OnThirdLoginFailed);
        }
    }

    void OnThirdLoginSuccess(string msg)
    {
        var data = JsonReader.Deserialize<LoginData>(msg);
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.THRID_LOGIN, EventStatus.SUCCESS, data));
    }

    void OnThirdLoginFailed(string msg)
    {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.THRID_LOGIN, EventStatus.FAILED, msg));
    }

    #endregion
}
