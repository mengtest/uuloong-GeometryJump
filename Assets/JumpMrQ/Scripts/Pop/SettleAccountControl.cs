using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using DG.Tweening;
using BaiyiGame.JumpMrQ;

namespace BaiyiGame.JumpMrQ
{
    public class SettleAccountControl : MonoBehaviour
    {
        public Button ButRestart;
        public Button BackToHome;
        private CanvasGroup canvasGroup;

        public CanvasManager canvasManager;
        private CanvasGroup canvasMangeCanvasGroup;

        public Button ButtonPause;
        // Use this for initialization
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasMangeCanvasGroup = canvasManager.GetComponent<CanvasGroup>();

            ButRestart.onClick.AddListener(BtnRestartClick);
            BackToHome.onClick.AddListener(BtnBackToHomeClick);
        }

        public void BtnRestartClick()
        {
			if (BYSDKManage.ShareManage._instance.IsInterstitialAdsReady()) {
				int num = UnityEngine.Random.Range (0,5);
				if (num == 0) {
					//var operation = new BYVideoResulte { resultCallback = OnSDKCallBack };
					BYSDKManage.ShareManage._instance.ShowInterstitialP ();
				}
			}
            this.canvasGroup.DOFade(0f, Config.ANIM_TIME_SHORT);
        }
        private void BtnBackToHomeClick()
        {
            BackHome();
        }

		private void BackHome () {
			this.canvasGroup.DOFade(0f, Config.ANIM_TIME_MID).OnComplete(delegate ()
				{
					Tweener gameUIOut = canvasMangeCanvasGroup.DOFade(0f, Config.ANIM_TIME_MID);
					Application.LoadLevel("HomeScene");
				});
		}

		/// SDK回调接口
		private void OnSDKCallBack (BYResulte result) {
			BackHome ();
		}
    }
}