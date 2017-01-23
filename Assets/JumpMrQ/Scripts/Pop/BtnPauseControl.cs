using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;
using BYSDKManage;

public class BtnPauseControl : MonoBehaviour
{

    public Button buttonMenu;
    public Button buttonContinue;
    public Button buttonRestart;

    private CanvasGroup canvasGroup;


    //暂停游戏
    public delegate void _GameContinue();
    public static event _GameContinue OnGameContinue;

    // Use this for initialization
    void Start()
    {

        canvasGroup = GetComponent<CanvasGroup>();
        buttonMenu.onClick.AddListener(BtnMenuClick);
        buttonContinue.onClick.AddListener(BtnContinueClick);
        buttonRestart.onClick.AddListener(buttonRestartClick);
    }

    private void buttonRestartClick()
    {
        if (ShareManage._instance.IsInterstitialAdsReady())
        {
            int num = UnityEngine.Random.Range(0, 2);
            if (num == 1)
            {
                //var operation = new BYVideoResulte { resultCallback = OnSDKCallBack };
                BYSDKManage.ShareManage._instance.ShowInterstitialP();
            }
        }
        //       canvasGroup.DOFade(0f, 0.5f).OnComplete(delegate() {
        //           this.gameObject.SetActive(false);
        //       });
    }

    private void BtnContinueClick()
    {
        Tweener continueTweener = canvasGroup.DOFade(0f, 0.5f).OnComplete(delegate ()
        {
            this.gameObject.SetActive(false);
            if (OnGameContinue != null)
            {
                OnGameContinue();
            }
        });
    }

    private void BtnMenuClick()
    {
        Application.LoadLevel("HomeScene");
        this.gameObject.SetActive(false);

    }

    /// SDK回调接口
    private void OnSDKCallBack(BYResulte result)
    {
        Application.LoadLevel("HomeScene");
        this.gameObject.SetActive(false);
    }
}
