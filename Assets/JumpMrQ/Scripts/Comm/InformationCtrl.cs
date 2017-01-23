/**
 * Author: Tan Yuqing
 * Date: 2016-10-11
 * Desc: 系统弹窗功能（信息）
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class InformationCtrl : MonoBehaviour {
/*
    public delegate void CallbackHandler ();

    private CallbackHandler callback;*/

    public RectTransform box; //窗体
    public RectTransform background; //背景

    public Text infoText;

    void Start () {
        MessageBoxAnimation.ZoomIn (box);
    }

    public void SetContent(string content) {
        infoText.text = content;
    }

    //确定按钮点击事件
    public void OnOKBtnClick () {
        //AudioManager.instance.PlayButtonSound ();
        Close ();
    }

    //Mask区域点击事件
    public void OnMaskAreaClick () {
        Close ();
    }

    //关闭
    private void Close() {
        Destroy (this.gameObject);
    }
}
