using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BYSDKManage;

public class SDKEventManager : MonoBehaviour
{
    //private static bool isCreate = false;
	public static readonly SDKEventManager Instance;
    private static Dictionary<string, string> shareDic;

	static SDKEventManager () {
		Instance = new SDKEventManager ();
	}

	public void Init () {
		BYSDKManage.SDKEventManager.SetShareContentCallBack(GetShareContentCallBack);

		BYSDKManage.SDKEventManager.SetPlayBGMCallBack (OnPlayBGM);
		BYSDKManage.SDKEventManager.SetStopBGMCallBack (OnStopBGM);

		BYSDKManage.SDKEventManager.SubscribShareShow (OnShareShow);
		BYSDKManage.SDKEventManager.SubscribShareClose (OnShareClose);
	}

//    void Awake()
//    {
//        if (!isCreate) {
//            isCreate = true;
//            DontDestroyOnLoad(this);
//            BYSDKManage.SDKEventManager.SetShareContentCallBack(GetShareContentCallBack);
//
//			BYSDKManage.SDKEventManager.SetPlayBGMCallBack (OnPlayBGM);
//			BYSDKManage.SDKEventManager.SetStopBGMCallBack (OnStopBGM);
//
//			BYSDKManage.SDKEventManager.SubscribShareShow (OnShareShow);
//			BYSDKManage.SDKEventManager.SubscribShareClose (OnShareClose);
//	
//        } else
//        {
//            Destroy(this);
//        }
//    }

//    void OnDestroy()
//    {
//        BYSDKManage.SDKEventManager.UnRegistEventHandler(SDKEventType.SHARE_SHOW, OnShareShow);
//        BYSDKManage.SDKEventManager.UnRegistEventHandler(SDKEventType.SHARE_CLOSE, OnShareClose);
//        BYSDKManage.SDKEventManager.UnRegistEventHandler(SDKEventType.PLAY_MUSIC, OnPlayBGM);
//        BYSDKManage.SDKEventManager.UnRegistEventHandler(SDKEventType.STOP_MUSIC, OnStopBGM);
//        BYSDKManage.SDKEventManager.UnRegistEventHandler<VersionInfo>(SDKEventType.VERSION_UPDATE, OnVersionUpdate);
//    }

    private Dictionary<string, string> GetShareContentCallBack()
    {
		string content = "一起来玩Mr.Q吧";//string.Format(Constants.SHARE_TEXT, PlayData.GetInstance().MaxLevel) + Constants.DOWNLOAD_URL;
        if (shareDic == null)
        {
            shareDic = new Dictionary<string, string>();
        }
        shareDic.Clear();
        shareDic.Add(ShareField.TITLE, "Mr.Q");
        shareDic.Add(ShareField.CONTENT, content);
        shareDic.Add(ShareField.DOWNLOAD_URL, Constants.DOWNLOAD_URL);
        shareDic.Add(ShareField.IMAGE_URL, Constants.LOGO_URL);
        return shareDic;
    }

    private void OnPlayBGM()
    {
		//AudioControl.instance.BgStart();
    }

    private void OnStopBGM()
    {
		//AudioControl.instance.BgStop();
    }

    private void OnShareShow()
    {
        BYLog.Log("############################ Modal state true");
        //GameModeSetting.isInModalState = true;
    }

    private void OnShareClose()
    {
        BYLog.Log("############################ Modal state false");
        //GameModeSetting.isInModalState = false;
    }

//    private void OnVersionUpdate(VersionInfo versionInfo)
//    {
//        if (!string.IsNullOrEmpty(versionInfo.gameid) && !string.IsNullOrEmpty(versionInfo.url))
//        {
//            var dialogTransform = GameObject.Find("Canvas").transform;
//
//            GameObject versionUpdate = Instantiate(Resources.Load("Prefabs/UI/VersionUpdate", typeof(GameObject))) as GameObject;
//            versionUpdate.GetComponent<VersionUpdateView>()
//                .SetVersion(versionInfo.version)
//                .SetContent(versionInfo.record)
//                .SetDownloadUrl(versionInfo.url);
//            versionUpdate.transform.SetParent(dialogTransform);
//            versionUpdate.transform.localPosition = new Vector3(0, 0, 0);
//            versionUpdate.transform.localScale = new Vector3(1, 1, 1);
//        }
//    }
}
