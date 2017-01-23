using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.Common
*
*	describe	:	N/A
*	class name	:	SDKUtiles
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/24 19:26:42				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

class SDKUtiles {

    /// <summary>
    /// 获取当前时间的秒数
    /// </summary>
    /// <returns></returns>
    public static long GetSeconds() {
        return DateTime.Now.Ticks / 10000000;
    }

    /// <summary>
    /// 查询时间窗是否开启
    /// </summary>
    /// <param name="t">事件发生时间 s </param>
    /// <param name="tw">时间窗大小 s </param>
    /// <returns> true - 时间窗已经开启，false - 时间窗锁定</returns>
    public static bool IsTimeWindowOpen(long t,long tw) {
        if( GetSeconds() - t >= tw ) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <returns>20160902</returns>
    public static int GetNowT() {
        return int.Parse(System.DateTime.Now.ToString("yyyyMMdd"));
    }

    public static bool GetLocationByLanguae() {
        return Application.systemLanguage == SystemLanguage.Chinese
               || Application.systemLanguage == SystemLanguage.ChineseSimplified
               || Application.systemLanguage == SystemLanguage.ChineseTraditional;
    }

    /// <summary>
    ///  获取设备识别码
    /// </summary>
    /// <returns></returns>
    public static string GetIMEI() {
        return SystemInfo.deviceUniqueIdentifier;
    }

    /// <summary>
    /// 获取时间戳
    /// </summary>
    /// <returns></returns>
    public static string GetTimeStamp() {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    /// <summary>
    /// 判断是否是Email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsEmail(string email) {
        var r = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
        if (r.IsMatch(email)) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 判断是否是手机号码
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsCellPhoneNumber(string number) {
        var r = new Regex("^(13|15|18)[0-9]{9}$");
        if (r.IsMatch(number)) {
            return true;
        } else {
            return false;
        }
    }
}
