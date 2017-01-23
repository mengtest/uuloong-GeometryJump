using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

class SDKNetUtls {
    private const string GAME_SVR = "http://123.56.240.25:8989/";
    private const string CONF_SVR = "http://123.56.240.25:8990/";
    /// <summary>
    /// 获取游戏策略文件
    /// </summary>
    /// <returns></returns>
    public static string GetStrategy(string gameName,int country,int device) {
        return string.Format(CONF_SVR + "AdsManager/GetStrategy?game_name={0}&country={1}&device={2}",gameName,country,device);
    }

    /// <summary>
    /// 获取游戏节点配置信息
    /// </summary>
    /// <returns></returns>
    public static string GetGameNodeConfig(string gameName, int country, int device) {
        return string.Format(CONF_SVR + "AdsManager/GetGameNodeConfig?game_name={0}&country={1}&device={2}",gameName,country,device);
    }

    /// <summary>
    /// 获取游戏控制配置参数
    /// </summary>
    /// <returns></returns>
    public static string GetGameConfigureData(string gameName,int country,int device) {
        return string.Format(CONF_SVR + "AdsManager/GetGameConfigureData?game_name={0}&country={1}&device={2}", gameName, country, device);
    }

    /// <summary>
    /// 获取节日活动
    /// </summary>
    /// <param name="gameid"></param>
    /// <returns></returns>
    public static string GetConfigUpdateTime(string gameName, int country, int device) {
        return string.Format(CONF_SVR + "AdsManager/GetGameConfigureData?game_name={0}&country={1}&device={2}", gameName, country, device);
    }

    /// <summary>
    /// 发送手机端收集到的广告信息
    /// </summary>
    /// <returns></returns>
    public static string PostSeriesData(string gameName) {
        return string.Format(CONF_SVR + "AdsManager/Putseries_data?game_name={0}", gameName);
    }

    #region 登录相关
    /// <summary>
    /// 通过邮箱获取密码
    /// </summary>
    /// <returns></returns>
    public static string GetPwdByEmailURL = GAME_SVR + "auth/sendemail";

/// <summary>
/// 修改密码
/// </summary>
/// <returns></returns>
    public static string ForgetPwdURL =  GAME_SVR + "auth/forgotpassword";

/// <summary>
/// 游客登录
/// </summary>
/// <returns></returns>
    public static string GustLoginURL = GAME_SVR + "auth/gust";


/// <summary>
/// 注册
/// </summary>
/// <returns></returns>
    public static string RegisterURL = GAME_SVR + "auth/register";

/// <summary>
/// 获取登录连接
/// </summary>
/// <returns></returns>
    public static string BYLoginURL = GAME_SVR + "auth/login";


/// <summary>
/// 获取第三方登录接口
/// </summary>
/// <returns></returns>
    public static string ThirfPartyURL = GAME_SVR + "auth/thirdparty";

    #endregion

    #region 积分相关
    public static readonly string IntegralRankURL = GAME_SVR + "integral/getRank";                  // 获取排行
    public static readonly string SendPointByRuleURL = GAME_SVR + "integral/sendPointsByRuleID";    // 根据规则ID赠送积分
    public static readonly string SendPoints = GAME_SVR + "integral/sendPoints";                    // 根据规则赠送积分

    /// <summary>
    /// 获取当前活动
    /// </summary>
    /// <param name="gameid"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public static string GetFestivalURL(string gameid,string language) {
        return string.Format(GAME_SVR + "integral/getFestival?gameid={0}&language={1}", gameid, language);
    }
    #endregion


    #region 好友相关
    public static readonly string PushMessageFriend = GAME_SVR + "invite/InviteMessage";        /// 向好友推送消息
    #endregion

}
