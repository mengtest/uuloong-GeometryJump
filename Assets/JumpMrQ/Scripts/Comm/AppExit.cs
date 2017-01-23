using UnityEngine;
using System.Collections;
using System;

public class AppExit : MonoBehaviour {

    //记录点击次数
    private int PointCount = 0;
    //记录上次按下的时间
    private float LastTime = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Exit();
	}
    /// <summary>
    /// 两秒内连续点击两次就退出
    /// </summary>
    private void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PointCount >= 1)
            {
                //2秒内，再按（手机：返回键）就执行
                if (Time.time - LastTime < 200f)
                {
                    Application.Quit();//退出
                }
            }
            else
            {
                //提示再按一次
                ++PointCount;
            }
            //i1  ==  记录按下返回键时的当前时间（ Time.time）
            LastTime = Time.time;
        }
    }
}
