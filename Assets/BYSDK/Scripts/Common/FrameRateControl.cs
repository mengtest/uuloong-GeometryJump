using UnityEngine;
using System.Collections;
using System.Threading;

public class FrameRateControl : MonoBehaviour
{
    public static FrameRateControl Instance;
    // 默认帧率
    public static int DefaulteRate = 30;
    // 待机帧率
    public static int StandbyRate = 10;

    // 时间间隔
    public float timeGap = 5;
    // 开始计时的时间
    private float startTime = 0;

    private bool isSetting = false;
    // 是否设置固定帧率
    private bool isFixRate = false;

    void Awake()
    {
        if (Instance == null || Instance == this) {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Application.targetFrameRate = DefaulteRate;
    }

    void FixedUpdate()
    {
        if (isSetting && !isFixRate && Time.time - startTime > timeGap)
        {
            SetFrameRate(StandbyRate, false);
            isSetting = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Application.targetFrameRate = DefaulteRate;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSetting = true;
            startTime = Time.time;
        }
    }

    public void SetFrameRate(int rate, bool isFixRate)
    {
        Application.targetFrameRate = rate;
        this.isFixRate = isFixRate;
    }
}
