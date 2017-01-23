/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class in charge to manage the game logic
/// </summary>
namespace BaiyiGame.JumpMrQ
{
    public class GameManager : MonoBehaviorHelper
    {
        public delegate void _GameStart();
        public static event _GameStart OnGameStart;

        public delegate void _GameOverStart();
        public static event _GameOverStart OnGameOverStarted;

        public delegate void _GameOverEnd();
        public static event _GameOverEnd OnGameOverEnded;

        public delegate void _SetPoint(int point);
        public static event _SetPoint OnSetPoint;

        public delegate void _SetLife(int life);
        public static event _SetLife OnSetLife;


        [SerializeField]
        private Transform obstacleRectanglePrefab;

        [SerializeField]
        private List<PlatformLogic> obstacleRectanglePrefabList = new List<PlatformLogic>();

        [SerializeField]
        private float SpringRectDisappearDist;
        [SerializeField]
        private float SummerRectDisappearDist;
        [SerializeField]
        private float AutumnRectDisappearDist;
        [SerializeField]
        private float WinterRectDisappearDist;

        [SerializeField]
        private Sprite SpringRect;
        [SerializeField]
        private Sprite SummerRect;
        [SerializeField]
        private Sprite AutumnRect;
        [SerializeField]
        private Sprite WinterRect;

        [SerializeField]
        private Score SCORE;
        [SerializeField]
        private Item LIFE;
        [SerializeField]
        private Item DIAMOND;
        private int RectCount = 20;

        public int AddPoint(int p)
        {
            int temp = this.SCORE.AddPoint(p);

            if (OnSetPoint != null)
                OnSetPoint(temp);

            return temp;
        }

        public int GestBestScore()
        {
            return this.SCORE.GetBest();
        }

        public int GestLastScore()
        {
            return this.SCORE.GetLast();
        }

        public int GetPoint()
        {
            return this.SCORE.GetPoint();
        }

        public bool HaveLife()
        {
            return this.GetLife() > 0;
        }

        public int GetLife()
        {
            return LIFE.GetCount();
        }

        public int AddLife(int n)
        {
            int temp = LIFE.AddToCount(n);

            if (OnSetLife != null)
                OnSetLife(temp);

            return temp;
        }

        private void Awake()
        {

            this.SCORE = new Score();
            this.LIFE = new Item(ItemType.LIFE, 3);

            Application.targetFrameRate = 60;

            CreateListRectangle(RectCount);

            InputTouch.OnTouchScreen += OnTouchScreen;

            DoSpawnStart();
        }

        void OnEnable()
        {
            PlayerManager.OnPlayerJump += DoMove;
            SeasonManager.onSwitchShow += OnSwitchShow;
            InputTouch.OnTouchScreen += OnTouchScreen;
        }

        void OnDisable()
        {
            PlayerManager.OnPlayerJump -= DoMove;
            SeasonManager.onSwitchShow -= OnSwitchShow;
            InputTouch.OnTouchScreen -= OnTouchScreen;
        }

        void OnTouchScreen()
        {
            InputTouch.OnTouchScreen -= OnTouchScreen;
            OnStart();
        }

        void DoMove(bool isSmall, float animTime, float distX, float distY, float jumpHeight)
        {
            DoSpawn();
        }

        void OnSwitchShow(string season)
        {
            int seasonType = 0;
            Sprite spr = SpringRect;
            if (season.Contains("Spring"))
            {
                spr = SummerRect;
                seasonType = 1;
            }
            else if (season.Contains("Summer"))
            {
                spr = AutumnRect;
                seasonType = 2;
            }
            else if (season.Contains("Autumn"))
            {
                spr = WinterRect;
                seasonType = 3;
            }
            else if (season.Contains("Winter"))
            {
                spr = SpringRect;
                seasonType = 0;
            }

            float camPosX = cam.transform.position.x;
            int count = obstacleRectanglePrefabList.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject go = obstacleRectanglePrefabList[i].gameObject;
                float posX = go.transform.position.x;
                if (!go.activeSelf)
                {
                    go.GetComponent<SpriteRenderer>().sprite = spr;
                }
                if ((seasonType == 0 && (posX - camPosX > SpringRectDisappearDist))
                    || (seasonType == 1 && (posX - camPosX > SummerRectDisappearDist))
                    || (seasonType == 2 && (posX - camPosX > AutumnRectDisappearDist))
                    || (seasonType == 3 && (posX - camPosX > WinterRectDisappearDist)))
                {
                    go.GetComponent<SpriteRenderer>().sprite = spr;
                }
            }
        }

        IEnumerator Start()
        {
            Application.targetFrameRate = 60;
            GC.Collect();

            yield return 0;
        }

        void CreateListRectangle(int i)
        {
            int count = 0;

            while (count < i)
            {
                CreateRectangle();
                count++;
            }
        }

        PlatformLogic CreateRectangle()
        {
            var ob = Instantiate(obstacleRectanglePrefab) as Transform;
            ob.SetParent(transform, false);
            ob.gameObject.SetActive(false);
            var pl = ob.GetComponent<PlatformLogic>();
            obstacleRectanglePrefabList.Add(pl);
            SetRectangleSprite(ob);
            return pl;
        }

        PlatformLogic GetRectangle()
        {
            var l = obstacleRectanglePrefabList.Find(o => o.gameObject.activeInHierarchy == false);
            if (l != null)
            {
                SetRectangleSprite(l.gameObject.transform);
            }
            else
            {
                l = CreateRectangle();
            }
            return l;
        }

        void SetRectangleSprite(Transform go)
        {
            Sprite spr = SpringRect;
            if (ScenesSwitch.CurSeason.Contains("Spring"))
            {
                spr = SpringRect;
            }
            else if (ScenesSwitch.CurSeason.Contains("Summer"))
            {
                spr = SummerRect;
            }
            else if (ScenesSwitch.CurSeason.Contains("Autumn"))
            {
                spr = AutumnRect;
            }
            else if (ScenesSwitch.CurSeason.Contains("Winter"))
            {
                spr = WinterRect;
            }

            go.GetComponent<SpriteRenderer>().sprite = spr;
        }

        public void GameOverStart()
        {
            if (OnGameOverStarted != null)
                OnGameOverStarted();
        }

        public void GameOverEnd()
        {
            OnGameOverEnded();

            if (OnGameOverEnded != null)
                SCORE.Save();
        }

        public void OnStart()
        {
            SCORE.SetPoint(0);

            if (OnGameStart != null)
                OnGameStart();

            if (OnSetPoint != null)
                OnSetPoint(0);

            countSpawn = 0;
        }


        [NonSerialized]
        public float smallSpace = 1.7f;
        public Vector2 lastPos;
        int countSpawn = 0;
        int diamondSpawnCount = 0;


        bool lastIsBig = false;

        int positionDisplayedBest = -1;
        int positionDisplayedLast = -1;

        public void DoSpawn()
        {
            lastPos += new Vector2(smallSpace, 0);

            int best = SCORE.GetBest();

            int last = SCORE.GetLast();

            bool displayBestOrLast = false;

            if (countSpawn == best || countSpawn == last)
                displayBestOrLast = true;


            // 1. 该柱子为上一次成绩或历史最好成绩时，显示柱子
            // 2. 起始至少有5个相连的柱子
            // 3. 上一个位置没有柱子则该位置要有柱子
            // 4. 1、2、3会导致后面的柱子间隔出现太有规律，加入随机因素
            if (displayBestOrLast || countSpawn < UnityEngine.Random.Range(5, 15) || lastIsBig || UnityEngine.Random.Range(0, 2) == 0)
            {
                lastIsBig = false;

                var ob = GetRectangle();
                ob.position = lastPos;
                ob.gameObject.SetActive(true);

                int diveuc = countSpawn % 30;

                if (diamondSpawnCount < diveuc)
                {
                    // 随机生成钻石
                    if (UnityEngine.Random.Range(0, Config.DIAMOND_PROBABILITY) == 0)
                    {
                        ob.ActivateDiamond();
                    }
                }

                if (countSpawn == best && positionDisplayedBest == -1)
                {
                    // 显示最佳成绩
                    ob.bestScore.SetActive(true);
                    positionDisplayedBest = countSpawn;
                }
                else
                {
                    ob.bestScore.SetActive(false);
                }

                if (countSpawn == last && last != best && positionDisplayedLast == -1)
                {
                    // 显示上一次成绩
                    ob.lastScore.SetActive(true);
                    positionDisplayedLast = countSpawn;
                }
                else
                {
                    ob.lastScore.SetActive(false);
                }
            }
            else
            {
                if (lastIsBig)
                    Debug.LogError("ERROR !!");

                lastIsBig = true;
            }

            countSpawn++;

            if (GetCount() < RectCount)
                DoSpawn();

        }

        void DoSpawnStart()
        {
            while (countSpawn < RectCount)
            {
                DoSpawn();
            }
        }

        int GetCount()
        {
            int count = 0;

            count += obstacleRectanglePrefabList.FindAll(o => o.gameObject.activeInHierarchy == true).Count;



            return count;
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }

        public void Pause()
        {
            SoundManager a = FindObjectOfType<SoundManager>();
            if (a != null)
            {
                a.MuteAllMusic();
            }

            Time.timeScale = 0;
        }

        public void Resume()
        {
            SoundManager a = FindObjectOfType<SoundManager>();
            if (a != null)
            {
                a.UnmuteAllMusic();
            }

            Time.timeScale = 1;
            GC.Collect();
            Application.targetFrameRate = 60;
        }
    }
}