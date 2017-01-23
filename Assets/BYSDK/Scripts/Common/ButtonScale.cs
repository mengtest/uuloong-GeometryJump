/**
 * Author: Tan Yuqing
 * Date: 2015-5-20
 * Desc: 实现点击按钮缩放效果
 */

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace BYSDKManage {
public class ButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public RectTransform targetButton; //目标按钮

    void Start() {
        if (!targetButton) {
            targetButton = this.GetComponent<RectTransform>();
        }
    }

    //点击事件
    public void OnPointerClick(PointerEventData eventData) {
        //Debug.Log ("OnPointerClick...");
    }

    //按下事件
    public void OnPointerDown(PointerEventData eventData) {
        smallScale();
    }

    //抬起事件
    public void OnPointerUp(PointerEventData eventData) {
        resetScale();
    }

    //设置scale为原来的80%
    private void smallScale() {
        float targetScale = 0.8f;

        targetButton.localScale = new Vector3(targetScale, targetScale, targetScale);
    }

    //还原scale
    private void resetScale() {
        int defaultScale = 1;

        targetButton.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
    }
}
}