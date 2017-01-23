using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Lunch : MonoBehaviour {
    public Image logo;
    AsyncOperation option;
    private int curTime = 0;

    // Use this for initialization
    void Start() {
        StartCoroutine(LoadSceneAsync());
        StartCoroutine(ActivateNextScene());
        InvokeRepeating("RotateLogo", 0, 0.2f);
    }

    void RotateLogo() {
        logo.transform.Rotate(new Vector3(0f, 0f, 0.5f), -30);
    }


    /// <summary>
    ///  异步加载场景信息
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSceneAsync() {
        option = SceneManager.LoadSceneAsync("start");
        option.allowSceneActivation = false;
        yield return option;
    }

    /// <summary>
    /// 显示下一个页面
    /// </summary>
    /// <returns></returns>
    IEnumerator ActivateNextScene() {
        yield return new WaitForSeconds(2f);
        option.allowSceneActivation = true;
    }
}
