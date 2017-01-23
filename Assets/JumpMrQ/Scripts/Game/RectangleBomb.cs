using UnityEngine;
using System.Collections;
using BaiyiGame.JumpMrQ;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class RectangleBomb : MonoBehaviour
{
    //春天的柱子碎片
    [SerializeField]
    private Sprite[] BombSpring;
    //夏天的柱子碎片
    [SerializeField]
    private Sprite[] BombSummer;
    //秋天的柱子碎片
    [SerializeField]
    private Sprite[] BombAutumn;
    //冬天的柱子碎片
    [SerializeField]
    private Sprite[] BombWinter;

    //临时存储场景名称
    private string SceneName = "Spring";

    string curRole = null;

    void Awake()
    {
        curRole = PlayerData.CurRole;
        if (string.IsNullOrEmpty(curRole))
        {
            curRole = Constants.DEFAULE_ROLE;
        }
    }

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
        this.SceneName = SceneName;
    }
    int count = 0;
    /// <summary>
    /// 当进入触发器
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((curRole + "(Clone)").Equals(other.gameObject.name))
        {
            var platform = other.GetComponent<PlatformLogic>();

            if (platform != null)
            {
                platform.DoElasticOut();
                return;
            }

            var player = other.GetComponentInParent<PlayerManager>();
            if (player != null)
            {
                player.LaunchGameOver();
                return;
            }
        }
        else
        {
            if (other.gameObject.name.Contains("Rectangle"))
            {
                if (count == 0)
                {
                    count = 1;
                    //被碰撞柱子的位置
                    Vector3 position = other.transform.localPosition;
                    //悲壮的柱子的名称
                    string rectName = other.gameObject.name;
                    //临时存储
                    Sprite[] Bombs;
                    if (SceneConfig.SPRING.Equals(SceneName))
                    {
                        Bombs = BombSpring;
                    }
                    else if (SceneConfig.SUMMER.Equals(SceneName))
                    {
                        Bombs = BombSummer;
                    }
                    else if (SceneConfig.AUTUMN.Equals(SceneName))
                    {
                        Bombs = BombAutumn;
                    }
                    else if (SceneConfig.WINTER.Equals(SceneName))
                    {
                        Bombs = BombWinter;
                    }
                    else
                    {
                        Bombs = BombSpring;
                    }
                    StartCoroutine(PlayBomb(Bombs, other));
                }
            }
        }
    }

    private static WaitForSeconds waitForSeconds = new WaitForSeconds(/*Time.deltaTime*/0.02f);
    private IEnumerator PlayBomb(Sprite[] Bombs, Collider2D other)
    {
        if (Bombs != null)
        {
            for (int i = 0; i < Bombs.Length; i++)
            {
                other.gameObject.GetComponent<SpriteRenderer>().sprite = Bombs[i];
                yield return waitForSeconds;
            }
            other.gameObject.SetActive(false);
            count = 0;
        }
    }

    void Destroy()
    {
        StopCoroutine(PlayBomb(null, null));
    }
}
