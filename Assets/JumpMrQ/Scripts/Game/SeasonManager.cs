using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BaiyiGame.JumpMrQ
{
    public class SeasonManager : MonoBehaviour
    {
        /// <summary>
        /// 显示过场元素
        /// </summary>
        public delegate void OnSwitchShow(string season);
        public static OnSwitchShow onSwitchShow;

        [SerializeField]
        private Transform Camera;

        /// <summary>
        /// 移动的层
        /// </summary>
        [SerializeField]
        private Transform[] MoveLevels;

        /// <summary>
        /// 场景切换距离
        /// </summary>
        [SerializeField]
        private float SwitchSeasonDistX;

        [SerializeField]
        private float StepLength;

        /// <summary>
        /// 切换的次数
        /// </summary>
        private int SwitchCount = 0;
        private float NextSeasonDistance;
        private bool isSwitchShow = false;
        private Dictionary<int, Vector3> positonsDic;

        void OnEnable()
        {
            PlayerManager.OnPlayerJump += DoMove;
            CanvasManager.onRestart += onRestart;
            NextSeasonDistance = SwitchSeasonDistX + Camera.position.x + SwitchCount * 23;
            isSwitchShow = false;
            SwitchCount++;
        }

        void OnDisable()
        {
            PlayerManager.OnPlayerJump -= DoMove;
            CanvasManager.onRestart -= onRestart;
        }

        void Awake()
        {
            positonsDic = new Dictionary<int, Vector3>();
            SavePositions();
        }

        void Start()
        {
        }

        void onRestart()
        {
            SwitchCount = 0;
        }

        void DoMove(bool isSmall, float animTime, float distX, float distY, float jumpHeight)
        {
            if (!isSwitchShow
                && Camera.position.x > NextSeasonDistance)
            {
                if (onSwitchShow != null)
                {
                    onSwitchShow(this.gameObject.name);
                }
                isSwitchShow = true;
            }
        }

        public void ResetPositions()
        {
            ResetPositions(this.transform);
        }

        private void ResetPositions(Transform go)
        {
            Vector3 vec3 = positonsDic[go.GetInstanceID()];
            go.localPosition = vec3;
            int childCount = go.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform trans = go.GetChild(i);
                ResetPositions(trans);
            }
        }

        private void SavePositions()
        {
            SavePositions(this.transform);
        }

        private void SavePositions(Transform go)
        {
            positonsDic.Add(go.GetInstanceID(),
                new Vector3(go.localPosition.x, go.localPosition.y, go.localPosition.z));
            int childCount = go.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform trans = go.GetChild(i);
                SavePositions(trans);
            }
        }

        public void SetSortingLayer(string sortinglayer)
        {
            SetSortingLayer(this.transform, sortinglayer);
        }

        private void SetSortingLayer(Transform go, string sortinglayer)
        {
            // 过场元素不设置
            if (go.name.Contains("Switch"))
            {
                return;
            }
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = sortinglayer;
            }

            int childCount = go.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform trans = go.gameObject.transform.GetChild(i);
                SetSortingLayer(trans, sortinglayer);
            }
        }

        public void SetChildCanMove(bool isCanMove)
        {
            int count = MoveLevels.Length;
            for (int i = 0; i < count; i++)
            {
                MoveLevels[i].GetComponent<MoveLevel>().CanMove = isCanMove;
            }
        }
    }
}