using UnityEngine;
using System.Collections;
using BaiyiGame.JumpMrQ;
using UnityEngine.EventSystems;
using System;

public class ButtonSoundControl : MonoBehaviour,
    IPointerDownHandler,
    IPointerClickHandler{

    void Awake () {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayBtnFx();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

}
