using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace BaiyiGame.JumpMrQ
{
    public class ScenesSwitch : MonoBehaviour
    {
        [SerializeField]
        private Transform Spring;

        [SerializeField]
        private Transform Summer;

        [SerializeField]
        private Transform Autumn;

        [SerializeField]
        private Transform Winter;

        [SerializeField]
        private float StepLength;

        private bool canMove = false;

        private static string curSeason = "Spring";
        public static string CurSeason
        {
            get { return curSeason; }
        }

        //切换场景
        public delegate void SceneChanged(string scene);
        public static SceneChanged OnSceneChanged;
        //临时存储场景名称
        private string SceneName = "Spring";

        void OnEnable()
        {
            SeasonManager.onSwitchShow += OnSwitchShow;
            CanvasManager.onRestart += onRestart;
            PlayerManager.OnPlayerJump += DoMove;
        }

        void OnDisable()
        {
            SeasonManager.onSwitchShow -= OnSwitchShow;
            CanvasManager.onRestart -= onRestart;
            PlayerManager.OnPlayerJump -= DoMove;
        }

        void onRestart()
        {
            curSeason = "Spring";
        }

        void DoMove(bool isSmall, float animTime, float distX, float distY, float jumpHeight)
        {
            //场景改变  改变字体颜色等
            if (null != OnSceneChanged)
            {
                OnSceneChanged(SceneName);
            }

            if (!canMove)
            {
                return;
            }

            float moveLen = isSmall ? StepLength : 2 * StepLength;

            Transform prevSeasonTransf = null;
            Transform curSeasonTransf = null;
            if (curSeason.Contains("Summer"))
            {
                prevSeasonTransf = Spring;
                curSeasonTransf = Summer;
                SceneName = "Summer";
            }
            if (curSeason.Contains("Autumn"))
            {
                prevSeasonTransf = Summer;
                curSeasonTransf = Autumn;
                SceneName = "Autumn";
            }
            if (curSeason.Contains("Winter"))
            {
                prevSeasonTransf = Autumn;
                curSeasonTransf = Winter;
                SceneName = "Winter";
            }
            if (curSeason.Contains("Spring"))
            {
                prevSeasonTransf = Winter;
                curSeasonTransf = Spring;
                SceneName = "Spring";
            }

            if (curSeasonTransf.localPosition.x + moveLen < 0)
            {
                prevSeasonTransf.gameObject.SetActive(false);
                curSeasonTransf.localPosition = Vector3.zero;
                curSeasonTransf.GetComponent<SeasonManager>().SetChildCanMove(true);
                canMove = false;
                return;
            }

            curSeasonTransf.DOLocalMoveX(curSeasonTransf.localPosition.x + moveLen, animTime).OnComplete(delegate ()
            {
                if (curSeasonTransf.localPosition.x < 0)
                {
                    curSeasonTransf.localPosition = Vector3.zero;
                    curSeasonTransf.GetComponent<SeasonManager>().SetChildCanMove(true);
                    canMove = false;
                }
            });

        }

        public void OnSwitchShow(string season)
        {
            StartCoroutine(ConfigNextSeasons(season));
        }

        IEnumerator ConfigNextSeasons(string season)
        {
            Transform curSeasonTrans = Summer;
            if (season.Contains("Spring"))
            {
                Spring.GetComponent<SeasonManager>().SetSortingLayer("SeasonLow");
                curSeason = "Summer";
                curSeasonTrans = Summer;

            }
            else if (season.Contains("Summer"))
            {
                Summer.GetComponent<SeasonManager>().SetSortingLayer("SeasonLow");
                curSeason = "Autumn";
                curSeasonTrans = Autumn;
            }
            else if (season.Contains("Autumn"))
            {
                Autumn.GetComponent<SeasonManager>().SetSortingLayer("SeasonLow");
                curSeason = "Winter";
                curSeasonTrans = Winter;
            }
            else if (season.Contains("Winter"))
            {
                Winter.GetComponent<SeasonManager>().SetSortingLayer("SeasonLow");
                curSeason = "Spring";
                curSeasonTrans = Spring;
            }

            curSeasonTrans.gameObject.SetActive(true);
            curSeasonTrans.GetComponent<SeasonManager>().SetChildCanMove(false);
            curSeasonTrans.GetComponent<SeasonManager>().ResetPositions();
            curSeasonTrans.GetComponent<SeasonManager>().SetSortingLayer("SeasonHigh");
            curSeasonTrans.localPosition = new Vector3(20, curSeasonTrans.localPosition.y, curSeasonTrans.localPosition.z);
            yield return null;
            canMove = true;
        }
    }
}