using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace BYSDKManage {
public class Loading : MonoBehaviour {
    public delegate void LodingActionHandler(ActionType type, ActionResulet result);        // 回调loading界面的句柄类型
    public event Action OnWindowClose;                                                      // loading 界面主动关闭时调用句柄
    public GameObject   loadingIcon;

    long                timeWindow = 8;
    long                StartTime = 0;
    bool isReturnFaile = false;

    void Awake() {
        Debug.Log(" loading awake");
    }
    void Start() {

    }

    void OnEnable() {
        isReturnFaile = false;
        StartTime = SDKUtiles.GetSeconds();
        int unit = Screen.height / 8;
        var pos = loadingIcon.GetComponent<RectTransform>().localPosition;
        loadingIcon.GetComponent<RectTransform>().localPosition = new Vector3(pos.x, pos.y + unit, pos.z);
        int minSize = Screen.height > Screen.width ? Screen.width : Screen.height;
        float sizW = 78f / 750f * minSize;
        float sc = sizW / 78f;
        loadingIcon.GetComponent<RectTransform>().localScale = new Vector3(sc,sc,1);

        InvokeRepeating("RotateLogo", 0, 0.2f);
    }

    void RotateLogo() {
        loadingIcon.transform.Rotate(new Vector3(0f, 0f, 0.5f), -30);
    }
    // Update is called once per frame
    /// <summary>
    /// 新添加，视屏加载失败。界面自动关闭
    /// </summary>
    void Update() {
        CloseByTimeOut();
    }

    /// <summary>
    ///  如果给定时间窗内没有发生回调，直接关闭加载界面
    /// </summary>
    public void CloseByTimeOut() {
        if(SDKUtiles.IsTimeWindowOpen(StartTime, timeWindow)) {
            if( null != OnWindowClose) {
                OnWindowClose();
                OnWindowClose = null;
            }
            Close();
        }
    }

    /// <summary>
    /// 返回失败后，开启音乐
    /// </summary>
    private void Close() {
        if (isReturnFaile) {
            SDKEventManager.PlayBGM();
        }
//        if(GameModeSetting.isInModalState ) {       // 修改游戏模态
//            GameModeSetting.isInModalState = false;
//        }
        Destroy(this.gameObject);
    }




/// <summary>
/// Loading界面回调函数，用于判断是否出现网络等故障导致无法加载。从而自动关闭loading界面
/// </summary>
/// <param name="type"></param>
/// <param name="resulte"></param>
    public void LoadingCallBack(ActionType type, ActionResulet resulte) {
        Debug.Log(" LoadingCallBack " + resulte);
        if( resulte != ActionResulet.Success ) {
            isReturnFaile = true;
        }
        Close();
    }

}
}
