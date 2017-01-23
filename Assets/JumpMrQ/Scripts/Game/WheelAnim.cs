using UnityEngine;
using System.Collections;
using DG.Tweening;

public class WheelAnim : MonoBehaviour
{

    //旋转一周的时间
    [SerializeField]
    private float RotationTime = 2f;
    void Start()
    {
        this.transform.DOLocalRotate(new Vector3(0, 0, -360), RotationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }
}
