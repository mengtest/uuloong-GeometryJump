using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class WaterWaveMOve : MonoBehaviour {

    //移动到哪里
    [SerializeField]
    private float MoveDistance = -0.016f;
    [SerializeField]
    private float AnimTime = 1f;

    //记录刚开始的位置
    private Vector3 originPosition;

    void Awake()
    {
        originPosition = this.transform.localPosition;
    }
	void Start () {
        MoveOutSide();
	}

    private void MoveOutSide()
    {
        originPosition.y += MoveDistance;
        this.transform.DOLocalMoveY(originPosition.y, AnimTime)
            .OnComplete<Tweener>(() =>
            {
                MoveInSide();
            });
    }
    private void MoveInSide()
    {
        originPosition.y -= MoveDistance;
        this.transform.DOLocalMoveY(originPosition.y, AnimTime)
           .OnComplete<Tweener>(() =>
           {
               MoveOutSide();
           });
    }

}
