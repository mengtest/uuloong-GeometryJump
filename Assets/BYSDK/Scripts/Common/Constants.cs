using UnityEngine;
using System.Collections;

namespace BYSDKManage {
public class Constants {
	public static readonly string DOWNLOAD_URL = "http://uuloong.com:808/gameservice/server/Appdownload/tap1";
	public static readonly string LOGO_URL = "http://o9xyhx6pn.bkt.clouddn.com/jof.png";

    public static readonly string LODING_INFO = Localization.GetText("正在努力加载中");
    public static readonly string LODING_SUCCESS = Localization.GetText("加载成功");
    public static readonly string LOADING_FAIL = Localization.GetText("加载失败，请稍后再试");
    public static readonly string ConnectMeail = "ihoukai@163.com";
    public static readonly string GAME_CENTER_INFO = Localization.GetText("网络错误，或者没有登录");
    public static readonly string SDK_SHARE_KEY = "BYSHAREKEY";
    public static readonly string SDK_COMMONT_KEY = "BYCOMMONTKEY";

	public static readonly int LIMITED_COMMENT_COUNT = 1;//评论次数限制
	public static readonly int LIMITED_DAY_SHARE_COUNT = 3;//每日分享次数限制

	public static readonly string PREFAB_PATH_MSBOX = "Prefabs/Information";
	public static readonly string GET_POINT = "Get {0} Diamond";
}
}