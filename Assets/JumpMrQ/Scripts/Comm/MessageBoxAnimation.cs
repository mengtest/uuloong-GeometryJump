// MessageBoxAnimation.cs
// Author: Tan Yuqing
// Date: 2016/10/26
// Desc: 消息框动画
//
using UnityEngine;
using System.Collections;
using DG.Tweening;

public static class MessageBoxAnimation {

    //由小变大效果
    public static void ZoomIn (Transform win) {
        const int targetScale = 1; //目标缩放比例
        const float animTime = 0.32f; //动画时间

        win.localScale = Vector3.zero; //先缩小

        win.DOScale (targetScale, animTime).SetEase(Ease.OutBack);
    }

    //飞入效果
    public static void FlyIn () {

    }

    //飞出效果
    public static void FlyOut () {

    }
}

