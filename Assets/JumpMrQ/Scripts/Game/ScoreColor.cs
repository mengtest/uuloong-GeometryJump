using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine.UI;
using BaiyiGame.JumpMrQ;

public class ScoreColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
        //Debug.Log("SceneName ----  >  "+ SceneName);
        if (SceneConfig.SPRING.Equals(SceneName))
        {
            this.GetComponent<Text>().DOColor(new Color32(253,159,86,255),0.5f);
        }
        else if (SceneConfig.SUMMER.Equals(SceneName))
        {
            this.GetComponent<Text>().DOColor(new Color32(128, 140, 126, 255), 0.5f);
        }
        else if (SceneConfig.AUTUMN.Equals(SceneName))
        {
            this.GetComponent<Text>().DOColor(new Color32(253, 159, 86, 255), 0.5f);
        }
        else if (SceneConfig.WINTER.Equals(SceneName))
        {
            this.GetComponent<Text>().DOColor(new Color32(255, 213, 223, 255), 0.5f);
        }

    }
}
