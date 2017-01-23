using UnityEngine;
using System.Collections;

namespace BYSDKManage
{
    public class ShareListener : MonoBehaviour
    {
        public delegate void iOSShareCallBackHandle(ActionType typr, ActionResulet resulte);
        public iOSShareCallBackHandle onIosShareCallBack;
        public static ShareListener instance;
        private static bool isCreate = false;
        static ShareListener()
        {
            if (isCreate)
            {
                return;
            }
            try
            {
                var go = new GameObject("ShareListener");
                go.AddComponent<ShareListener>();
                DontDestroyOnLoad(go);
                isCreate = true;
            }
            catch (UnityException)
            {
                Debug.LogWarning("It looks like you have the ShareListener on a GameObject in your scene. Please remove the script from your scene.");
            }
        }

        public static void noop()
        {

        }
        void Awake()
        {
            //if( null != instance && instance != this ) {
            //    Destroy(this.gameObject);
            //} else if( null == instance) {
            instance = this;
            //    DontDestroyOnLoad(this.gameObject);
            //}
        }

        /// <summary>
        /// 设置回调句柄，每次调用都需要设置一次
        /// </summary>
        /// <param name="handle"></param>
        public void setCallBackHandle(iOSShareCallBackHandle handle)
        {
            onIosShareCallBack = handle;
        }


        /// <summary>
        ///  用于接收IOS系统分享返回的信息
        /// </summary>
        /// <param name="resulte">true 发送成功，false 发送失败</param>
        public void OnIosShareCallBack(string resulte)
        {
            if (null == onIosShareCallBack)
            {
                Debug.Log("Call back is null fuck");
                return;
            }

            if (resulte == "true")
            {
                onIosShareCallBack(ActionType.Share, ActionResulet.Success);
            }
            else {
                onIosShareCallBack(ActionType.Share, ActionResulet.Failed);
            }
            onIosShareCallBack = null;              // 调用后销毁回调句柄
        }
    }
}