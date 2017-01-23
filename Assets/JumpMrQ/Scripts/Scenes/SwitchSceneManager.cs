using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using DG.Tweening;
using BaiyiGame.JumpMrQ;
using System.Collections.Generic;

public class SwitchSceneManager : MonoBehaviour {

    //场景坐标
    private int index = 0;

    //显示下一个
    public delegate void _ShowNext(int index);
    public static event _ShowNext OnShowNext;
    //显示隐藏一个
    public delegate void _HideFront(int index);
    public static event _HideFront OnHideFront;

    //摄像机
    [SerializeField]
    private Camera camera;
    // Use this for initialization
    void Start() {
        //Spring = CreateScenes(Spring, 52.55f);
        StartCoroutine(SwitchScenes());
    }


    void OnEnable()
    {
        PlayerManager.OnPlayerJump += DoMove;
        CanvasManager.OnGamePause += OnGamePause;
        BtnPauseControl.OnGameContinue += OnGameContinue;
    }

    void OnDisable()
    {
        PlayerManager.OnPlayerJump -= DoMove;
        CanvasManager.OnGamePause -= OnGamePause;
        BtnPauseControl.OnGameContinue -= OnGameContinue;
    }

    void OnGamePause()
    {
        isPlaying = false;
    }
    void OnGameContinue()
    {
        isPlaying = true;
    }

    private float distancce = 0;
    void DoMove(bool isSmall, float animTime, float distX, float distY, float jumpHeight)
    {
    }
    bool isPlaying = true;
    private int count = 0;
    //移动的距离
    private float moveDis;
     IEnumerator SwitchScenes()
    {
        while (true)
        {
            if (isPlaying)
            {
                yield return new WaitForSeconds(20);
                if (count  == 0)
                {
                    moveDis = 78.0f;
                    Switch();
                    count = 1;
                }
                else if (count == 1)
                {
                    //-118.5
                    moveDis = 42f;
                    Switch();
                    count = 0;
                }
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    private void Switch()
    {
        ++index;
        if (OnShowNext != null)
        {
            OnShowNext(index);
        }

        this.transform.DOMove(new Vector3(this.transform.localPosition.x - moveDis, this.transform.localPosition.y, this.transform.localPosition.z),5f)
            .OnComplete(()=>
            {
                if (OnHideFront != null)
                {
                    OnHideFront(index - 1);
                }
            });
    }
}
