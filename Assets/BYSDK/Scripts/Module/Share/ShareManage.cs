using System;
using UnityEngine;
using System.Collections;
using cn.sharesdk.unity3d;
using PathologicalGames;

namespace BYSDKManage {
public class ShareManage : MonoBehaviour {
    public static ShareManage _instance;
    public static string SHARE_PANLE_PREFAB_PATH = "SharePanelLeft";
    public delegate void ActionCallBack(ActionType type, ActionResulet resul);
    public delegate void CloseSharePanleHandler();
    private static ShareSDK sharecontent;
    private static CommentGame ComGame;
    private static UULongAdsSDK googleAds;
    ShareListener.iOSShareCallBackHandle shareHandler;
    private AdsProportion restartgame = new AdsProportion();   // 用于游戏结束判定显示插页还是视频
    private UMPush _push;
    void Awake() {
        if (null != _instance && _instance != this) {
            Destroy(this.gameObject);
        } else if (null == _instance) {
            _instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    // Use this for initialization
    void Start() {
        _push = new UMPush();
        this.gameObject.AddComponent<UULongAdsSDK>();
        googleAds = this.GetComponent<UULongAdsSDK>();
        sharecontent = gameObject.GetComponent<ShareSDK>();
		ComGame = new CommentGame(Config.appID);
        sharecontent.authHandler += OnAuthHander; // 授权回调
        sharecontent.shareHandler += OnShareHandler; // 分享回调函数
        sharecontent.showUserHandler += OnShowUserHandler; // 获取好友回调函数
        sharecontent.getFriendsHandler += OnGetFriendHandler; // 获取好友列表
        sharecontent.followFriendHandler += OnFollowFriendHandler; //
        this.gameObject.AddComponent<TalkingDataScript>();
    }


    void OnDestory() {
        sharecontent.authHandler -= OnAuthHander; // 授权回调
        sharecontent.shareHandler -= OnShareHandler; // 分享回调函数
        sharecontent.showUserHandler -= OnShowUserHandler; // 获取好友回调函数
        sharecontent.getFriendsHandler -= OnGetFriendHandler; // 获取好友列表
        sharecontent.followFriendHandler -= OnFollowFriendHandler; //
    }


    /// <summary>
    /// 穿件分享界面
    /// </summary>
    public void CreateSharePanle() {
        Transform transform = PoolManager.Pools["UIPool"].Spawn(SHARE_PANLE_PREFAB_PATH);
        transform.SetParent(GameObject.Find("PublicPanel").transform);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        // closeShareHandler = sharePanle.GetComponent<ShareControl>().OnCancel;
    }


    /// <summary>
    ///  分享回调函数
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    private void OnShareHandler(int reqID, ResponseState state, PlatformType type, Hashtable data) {
        Debug.Log("share resulte " + state.ToString());
        if (null == shareHandler) {
            return;
        }

        switch (state) {
        case ResponseState.Success:
            shareHandler(ActionType.Share, ActionResulet.Success);
            break;
        case ResponseState.Cancel:
            shareHandler(ActionType.Share, ActionResulet.Skipe);
            break;
        case ResponseState.Fail:
            shareHandler(ActionType.Share, ActionResulet.Failed);
            break;
        }
        shareHandler = null;
        //closeShareHandler();
        //closeShareHandler = null;
    }

    /// <summary>
    ///  授权回调函数
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    private void OnAuthHander(int reqID, ResponseState state, PlatformType type, Hashtable data) {

    }

    /// <summary>
    /// 好友分享回调
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    private void OnShowUserHandler(int reqID, ResponseState state, PlatformType type, Hashtable data) {

    }

    /// <summary>
    /// 获取好友列表回调
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    private void OnGetFriendHandler(int reqID, ResponseState state, PlatformType type, Hashtable data) {

    }

    /// <summary>
    ///  邀请好友回调函数
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    private void OnFollowFriendHandler(int reqID, ResponseState state, PlatformType type, Hashtable data) {

    }

    /// <summary>
    /// 设置分享回调函数，用于添加游戏点数
    /// </summary>
    /// <param name="handler"></param>
    public void setShareHandler(ShareListener.iOSShareCallBackHandle handler) {
        shareHandler = handler;
    }

    public void ShowPlatformList(String title, String text, String url, String imageUrl) {
        ShareContent content = new ShareContent();
        content.SetText(text);
        content.SetImageUrl(imageUrl);
        content.SetTitle(title);
        content.SetTitleUrl(url);
        content.SetSite(title);
        content.SetSiteUrl(url);
        content.SetUrl(url);
        content.SetComment("");
        content.SetShareType(ContentType.Auto);
        sharecontent.ShowPlatformList(null, content, 100, 100);
    }

    public void ShareTo(PlatformType plat, String title, String text, String url, String imageUrl) {
        ShareContent content = new ShareContent();
		if (plat == PlatformType.Facebook) {
			content.SetShareType (ContentType.Text);
		} else {
			content.SetText(text);
			content.SetImageUrl(imageUrl);
			content.SetTitle(title);
			content.SetTitleUrl(url);
			content.SetSite(title);
			content.SetSiteUrl(url);
			content.SetUrl(url);
			content.SetComment("");
			content.SetShareType(ContentType.Auto);
		}
        
        sharecontent.DisableSSO(false);
        sharecontent.ShowShareContentEditor(plat, content);
    }

    /// <summary>
    /// 分享网络图片
    /// </summary>
    /// <param name="plat">平台类型</param>
    public void ShareGameByImageUrl(PlatformType plat, string text, string imageUrl) {
        ShareContent content = new ShareContent();
        content.SetText(text);
        content.SetImageUrl(imageUrl);
        content.SetComment("");
        content.SetShareType(ContentType.Image);
        sharecontent.ShowShareContentEditor(plat, content);
    }

    /// <summary>
    /// 分享文字信息
    /// </summary>
    /// <param name="plat">平台信息</param>
    public void ShareGameByText(PlatformType plat, string title, string text, string url) {
        Debug.Log(text);
        ShareContent content = new ShareContent();
        content.SetText(text);
        content.SetShareType(ContentType.Text);
        content.SetTitle(title);
        content.SetTitleUrl(url);
        content.SetSiteUrl(url);
        content.SetUrl(url);
        sharecontent.ShowShareContentEditor(plat, content);
    }

    /// <summary>
    /// 分享本地图片
    /// </summary>
    /// <param name="plat">平台信息</param>
    public void ShareGameByLocalImage(PlatformType plat, string title, string text, string url) {

        string path = GetImagePath();
        Debug.Log("path" + path);
        if (path == "") {
            ShareGameByText(plat, title, text, url);
            return;
        }
        Debug.Log(text);
        ShareContent content = new ShareContent();
        content.SetText(text);
        //content.SetImagePath(path);
        content.SetShareType(ContentType.Image);
        content.SetTitle(title);
        content.SetTitleUrl(url);
        content.SetSite(title);
        content.SetSiteUrl(url);
        content.SetUrl(url);
#if UNITY_IPHONE
        content.SetThumbImageUrl(path);
        content.SetImageUrl(path);
#endif
        sharecontent.ShowShareContentEditor(plat, content);
    }

    private string GetImagePath() {
        string path = Application.persistentDataPath + "/ShareLogo.png";
        if (!System.IO.File.Exists(path)) {
            Texture2D texture = Resources.Load("ShareLogo") as Texture2D;
            if (null != texture) {
                System.IO.File.WriteAllBytes(path, texture.EncodeToPNG());
                return path;
            }
            return "";
        }

        return path;
    }

    /// <summary>
    ///  登录授权
    /// </summary>
    /// <param name="plat">平台信息</param>
    public void loginFB(PlatformType plat) {
        sharecontent.Authorize(plat);
    }

    /// <summary>
    ///  获取好友
    /// </summary>
    public void getFriend() {


    }

    /// <summary>
    /// 显示静态插页广告
    /// </summary>
    public void ShowInterstitial() {
        googleAds.displayInterstitialAds();
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

	public void ShowInterstitialP(BYVideoResulte resulte = null) {
        BYLog.Log("Show Interstitial ads which only picture");
		googleAds.displayInterstitialAdsP(resulte);
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }
    /// <summary>
    ///  跳转评论页面
    /// </summary>
    public void CommentGame() {
        ComGame.Comment();

    }

    /// <summary>
    /// 调用系统的分享
    /// </summary>
    public void ShareGameByOS(string content, string url) {
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
                jo.Call("nativeShare", "", content);
            }
        }
#elif UNITY_IPHONE
        ShareListener.noop();
        SYSShareIOS.OnShareForIOS(content,url);
#endif
        ShareListener.instance.setCallBackHandle(shareHandler);
        //if (null != shareHandler)
        //    shareHandler(ActionType.Share, ActionResulet.Success);
    }

    /// <summary>
    ///  IOS分享回调函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="resulte"></param>
    void IosShareCallBack(ActionType type, ActionResulet resulte) {
        if (shareHandler != null) {
            shareHandler(type, resulte);
        }
    }

    /// <summary>
    /// 调用系统本身的发邮件功能
    /// </summary>
    public void SendEmailByOS() {
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
                jo.Call("sendEmail", "ihoukai@163.com", "", "");
            }
        }
#elif UNITY_IPHONE
        SYSShareIOS.SendEmail();
#endif
    }

    /// <summary>
    /// 返回视屏广告接在结果，从而控制关闭视屏加载页面
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="resulet"></param>
    void OnLoadingHandleCallBack(ActionType _type, ActionResulet resulet) {
        try {
            if (LoadingHandler != null) {
                LoadingHandler(_type, resulet);
                LoadingHandler = null;
            }
        } catch (Exception e) {
            BYLog.Log(e.ToString());
            LoadingHandler = null;
        }
    }

    /// <summary>
    /// 查询视屏广告是否准备就绪
    /// </summary>
    /// <returns></returns>
    public bool IsVideoAdsReady() {
        return googleAds.IsVideoAdsReady();
    }
    /// <summary>
    /// 查询插页广告是否准备就绪
    /// </summary>
    /// <returns></returns>
    public bool IsInterstitialAdsReady() {
        return googleAds.IsInterstitialAdsReady();
    }
    /// <summary>
    ///  视屏播放控制，由于视屏广告加载比较麻烦，容易出错
    ///  因此使用UnityAds 视屏广告 和 ChartBost 视屏结合使用
    ///  如果效果还是不佳，后面将会整合 Google 广告。。。。
    /// </summary>
    /// <modif>增加背景音乐控制 1、在播放视屏广告时进行暂停背景音乐 2、在加载广告失败 和 广告结束返回时播放背景音乐</modif>
    public void ShowVides(ActionCallBack handler) {
        addPointHandler = handler;
        CreateVideoLoadingPanle();
        SDKEventManager.StopBGM();
        BYVideoResulte operation = new BYVideoResulte { resultCallback = VideoCallHandler };
        googleAds.displayVideoAds(OnLoadingHandleCallBack, operation);
    }

    Loading.LodingActionHandler LoadingHandler;
    ActionCallBack addPointHandler;
    /// <summary>
    ///  创建视屏加载界面
    /// </summary>
    private void CreateVideoLoadingPanle() {
        GameObject LoadPanle = Instantiate(Resources.Load(AdsConfig.LOAD_PANLE_PREFAB_PATH, typeof(GameObject))) as GameObject;
        LoadPanle.transform.SetParent(GameObject.Find("PublicPanel").transform);
        LoadPanle.transform.localPosition = new Vector3(0, 0, 0);
        LoadPanle.transform.localScale = new Vector3(1, 1, 1);
        LoadPanle.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        LoadPanle.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        setLoadingHandler(LoadPanle.GetComponent<Loading>().LoadingCallBack);
        LoadPanle.GetComponent<Loading>().OnWindowClose += delegate {
            LoadingHandler = null;
        };
    }

    /// <summary>
    ///  用于事件分发
    /// </summary>
    /// <param name="type"></param>
    /// <param name="resulte"></param>
    private void VideoCallHandler(BYResulte resulte) {
        if (ActionType.Video == resulte._type) {
            if (null != addPointHandler) {
                addPointHandler(resulte._type, resulte._resulte);
            }
            addPointHandler = null;
        }
        SDKEventManager.PlayBGM();
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    ///  通知loading界面的句柄
    /// </summary>
    /// <param name="handler"></param>
    private void setLoadingHandler(Loading.LodingActionHandler handler) {
        LoadingHandler = handler;
    }

    /// <summary>
    ///  分享完成后，保存分享的次数。之前写在别处，现在挪到此处
    ///  Date: 2016-6-27
    ///  value: 20160902_n
    /// </summary>
    public void SaveShareCount() {
        int shareCount = 0;
        string ret = PlayerPrefs.GetString(Constants.SDK_SHARE_KEY, "");
        if ("" != ret) {
            string[] temp = ret.Split('_');
            if (temp[0] == SDKUtiles.GetNowT().ToString()) {
                shareCount = int.Parse(temp[1]);
            }
        }
        shareCount++;
        PlayerPrefs.SetString(Constants.SDK_SHARE_KEY, SDKUtiles.GetNowT().ToString() + "_" + shareCount.ToString());
    }

    /// <summary>
    ///  分享完成后，保存评论的次数。之前写在别处，现在挪到此处
    ///  Date: 2016-6-27
    /// </summary>
    public void SaveCommentCount() {
        int currentCount = PlayerPrefs.GetInt(Constants.SDK_COMMONT_KEY, 0);
        currentCount++;
        PlayerPrefs.SetInt(Constants.SDK_COMMONT_KEY, currentCount);
    }

    /// <summary>
    /// 获取评论次数
    /// </summary>
    /// <returns>0 - 没有评论 </returns>
    public int GetCommectCount() {
        return PlayerPrefs.GetInt(Constants.SDK_COMMONT_KEY, 0);
    }

    /// <summary>
    /// 获取当日分享次数
    /// </summary>
    /// <returns></returns>
    public int GetTodayShareCount() {
        int shareCount = 0;
        string ret = PlayerPrefs.GetString(Constants.SDK_SHARE_KEY, "");
        if ("" != ret) {
            string[] temp = ret.Split('_');
            if (temp[0] == SDKUtiles.GetNowT().ToString()) {
                shareCount = int.Parse(temp[1]);
            }
        }
        return shareCount;
    }
    /// <summary>
    ///  判断是否安装可客户端
    /// </summary>
    /// <param name="plat"></param>
    /// <returns></returns>
    /// <remarks> 取消平台限制</remarks>
    public bool IsClientValid(PlatformType plat) {
#if UNITY_EDITOR
        return true;
#else
        return sharecontent.IsClientValid(plat);
#endif
    }

    /// <summary>
    ///  直接调用系统自带分享进行分享
    /// </summary>
    /// <param name="handl"></param>
    /// <param name="text"></param>
    /// <param name="url"></param>
    public void ShareByIos(ShareListener.iOSShareCallBackHandle handl, string text, string url) {
        Debug.Log("text " + text);
        Debug.Log("url " + url);
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
                jo.Call("nativeShare", "", text + url);
            }
        }
        handl(ActionType.Share, ActionResulet.Success);
#elif UNITY_IPHONE
        ShareListener.instance.setCallBackHandle(handl);
        SYSShareIOS.OnShareForIOS(text, url);
#endif
    }

    /// <summary>
    /// 更多游戏
    /// </summary>
    public void MoreGame() {
        Application.OpenURL(Config.moreGameUrl);
    }

    /// <summary>
    ///  根据给定的比例进行广告播放
    ///  例如： 播放 3 个插页广告，播放一次视频广告
    /// ShowAdsAfterGameOver （3，1）
    /// </summary>
    /// <param name="inter_num">Inter number.</param>
    /// <param name="video_num">Video number.</param>
    public void ShowAdsAfterGameOver(int inter_num, int video_num) {
        if (!restartgame.isNeedShowInter(inter_num, video_num)) {
            ShowVides(OnVideoCallback);
        } else {
            var option = new BYVideoResulte { resultCallback = OnInterCallback };
            googleAds.displayinterstitialGetZ(option);
        }
    }



    public void OnVideoCallback(BYSDKManage.ActionType _type, BYSDKManage.ActionResulet resulte) {
        if (resulte == BYSDKManage.ActionResulet.Success) {
            restartgame.AddVideoCount();
        }

    }

    public void OnInterCallback(BYResulte resulte) {
        if (resulte._resulte == BYSDKManage.ActionResulet.Success) {
            restartgame.AddinterCount();
        }

    }
}
}


public class AdsProportion {
    int interShowCount;
    int videoShowCount;

    public void AddVideoCount() {
        videoShowCount++;
    }

    public void AddinterCount() {
        interShowCount++;
    }


    public bool isNeedShowInter(int inter, int video) {

        if (video == 0 || interShowCount == 0 || interShowCount < inter)
            return true;

        if (videoShowCount > 0) {
            interShowCount = interShowCount - inter * videoShowCount;
            videoShowCount = 0;
            return interShowCount <= 0;
        } else {
            return false;
        }
    }
}
