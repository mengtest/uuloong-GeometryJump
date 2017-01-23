using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.Common
*
*	describe	:	N/A
*	class name	:	Config
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/10/10 21:10:31				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public  class Config {
    /// <summary>
    /// 应用在AppStore的ID
    /// </summary>
    public readonly static string appID = "1181349927";

    /// <summary>
    /// 更多游戏列表
    /// </summary>
    public readonly static string moreGameUrl = "itms-apps://itunes.apple.com/us/developer/ruijun-li/id1139073406";

    #region GameCenter
    public readonly static string Leaderboar = "Mr.Q";
    public readonly static string[] achieveIDLise = { "", "N_A_01", "N_A_02", "N_A_03", "N_A_04", "N_A_05",
                                                      "N_A_06","N_A_07","N_A_08","N_A_09","N_A_10","N_A_11"
                                                    };
    #endregion

    /// <summary>
    /// ShareSDK账号
    /// </summary>
#if UNITY_ANDROID
	public readonly static string appKey = "18da618ad9428";
#elif UNITY_IPHONE
	public readonly static string appKey = "18da5befdf514";
#endif

    /// <summary>
    /// 授权回调URL
    /// </summary>
    public readonly static string RedirectUrl = "http://uuloong.com:808/gameservice/server/Appdownload/jumpmrq";



    #region  Talkingdata
    /// <summary>
    /// TalkData appID
    /// </summary>
	public static readonly string TALK_DATA = "275112D8914C4A008F289BBCDECEC07E";
    #endregion

    /// <summary>
    /// 新浪微博
    /// </summary>
	public readonly static PlatFormObj SinaWeiBo      = new PlatFormObj { AppKey = "1228174909", AppSecret = "ebf0cbffcb743e4ef5f547cdab04d798" };

    /// <summary>
    /// Facebook
    /// </summary>
	public readonly static PlatFormObj FaceBook       = new PlatFormObj { AppKey = "872693642867234", AppSecret = "84c58d57a80e74f5ca42f54d2cf24986" };

    /// <summary>
    /// QQ
    /// </summary>
	#if UNITY_ANDROID
	public readonly static PlatFormObj QQ             = new PlatFormObj { AppKey = "1105810360", AppSecret = "Jg69xoD5gLjb2pI2" };
	#elif UNITY_IPHONE
	public readonly static PlatFormObj QQ             = new PlatFormObj { AppKey = "1105810278", AppSecret = "qOBTLeTYyo5bkoPS" };
	#endif
    

    /// <summary>
    /// 微信
    /// </summary>
	public readonly static PlatFormObj WeiChat        = new PlatFormObj { AppKey = "wx1626780eec579027", AppSecret = "6e2ecd23fff67daca99c9070d033acc6" };

    /// <summary>
    /// Twitter
    /// </summary>
	public readonly static PlatFormObj Twitter        = new PlatFormObj { AppKey = "Ht7UXog1CT2gSe2udqv9QxKmy", AppSecret = "2W8DxIjcJuJVjSZH7BhdMc3tUO7910Sp1NeP8TYKuwZTyLcARr" };
}

public class PlatFormObj {

    /// <summary>
    /// App ID
    /// </summary>
    public  string AppKey {
        get;
        set;
    }

    /// <summary>
    /// App 秘钥
    /// </summary>
    public  string AppSecret {
        get;
        set;
    }
}

