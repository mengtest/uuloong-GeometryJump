using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class LeafAnim : MonoBehaviour {

    //标志位 1 左面 0 右面
    [SerializeField]
    private int flag = 0;

    //旋转角度 右
    [SerializeField]
    private float ratationRight = -15f;
    //旋转角度 左
    [SerializeField]
    private float ratationLeft = 15f;
    //动画时间
    [SerializeField]
    private float AnimTime = 10f;

    void Start () {
        if (flag == 0)
        {
            RatationLeft();
        }
        else
        {
            RatationRight();
        }
	}

    private void RatationRight()
    {
        this.transform.DOLocalRotate(new Vector3(0,0,ratationRight), AnimTime).OnComplete<Tweener>
            (()=> {
                RatationLeft();
            });
    }

    private void RatationLeft()
    {
        this.transform.DOLocalRotate(new Vector3(0, 0, ratationLeft), AnimTime).OnComplete<Tweener>
            (() => {
                RatationRight();
            });
    }
}
