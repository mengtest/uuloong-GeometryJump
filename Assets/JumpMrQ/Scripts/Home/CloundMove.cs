using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class CloundMove : MonoBehaviour {

    //x的其实位置
    [SerializeField]
    private float StartX;
    //x 的重点位置
    [SerializeField]
    private float EndX;
    //动画时间
    [SerializeField]
    private float AnimTime = 10f;
    // Use this for initialization
    void Start () {
        StartAinm();
	}

    private void StartAinm()
    {
        this.transform.DOMoveX(EndX, AnimTime).OnComplete<Tweener>(
            ()=> 
            {
                BackAinm();
            });
    }
    private void BackAinm()
    {
        this.transform.DOMoveX(StartX, AnimTime).OnComplete<Tweener>(
           () =>
           {
               StartAinm();
           });
    }
}
