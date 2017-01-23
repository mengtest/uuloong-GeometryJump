using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine.UI;
using BaiyiGame.JumpMrQ;

public class DiamongTextColor : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
    }

    void OnEnable()
    {
        //当场景切换的时候
        ScenesSwitch.OnSceneChanged += OnSceneChanged;
    }

    void OnDisable()
    {
        ScenesSwitch.OnSceneChanged -= OnSceneChanged;
    }
    /// <summary>
    /// 当场景切换的时候
    /// </summary>
    /// <param name="SceneName">当前在摄像机范围内的场景名称</param>
    void OnSceneChanged(string SceneName)
    {
        if (null != SceneName)
        {
            if (SceneConfig.WINTER.Equals(SceneName))
            {
                // this.GetComponent<Text>().DOColor(new Color(251f, 198f, 32f, 255f), 0.5f);
                this.GetComponent<Text>().color = new Color32(251, 198, 32, 255);

            }
            else
            {
                //this.GetComponent<Text>().DOColor(new Color(220f, 138f, 41f, 255f), 0.5f);
                this.GetComponent<Text>().color = new Color32(220, 138, 41, 255);
                //this.GetComponent<Text>().color = Color.cyan;
            }
        }
    }
}
