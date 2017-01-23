using UnityEngine;
using System.Collections;
using System;

namespace BaiyiGame.JumpMrQ
{
    public class PlayerResManager : MonoBehaviorHelper
    {
        [SerializeField]
        private SpriteRenderer Player;

        [SerializeField]
        private Sprite[] PlayerAngles;

        [SerializeField]
        private AudioClip jumpFX;

        private int JumpCount;

        void OnEnable()
        {
            JumpCount = 0;
            soundManager.jumpFX = jumpFX;
            PlayerManager.OnPlayerJumpStarted += OnPlayerJumpStarted;
            GameManager.OnGameOverStarted += OnGameOverStarted;
        }

        void OnDisable()
        {
            PlayerManager.OnPlayerJumpStarted -= OnPlayerJumpStarted;
            GameManager.OnGameOverStarted -= OnGameOverStarted;
        }
        public void OnPlayerJumpStarted() {
            // 角色开始跳 替换相对应的视角图片
            JumpCount++;
            Player.sprite = PlayerAngles[JumpCount % 4];
            //StartCoroutine(PlayerAnim());
        }
        /// <summary>
        /// 人物在空中做动作
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayerAnim()
        {
            for (int i = 0; i < PlayerAngles.Length-1; i++)
            {
                Player.sprite = PlayerAngles[i];
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void OnGameOverStarted()
        {
            JumpCount = 0;
        }


        /// <summary>
        /// 当进入触发器
        /// </summary>
        /// <param name="other"></param>
        void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("PlayerResManager ---- >  "+other.gameObject.name);
            //string rectName  = other.gameObject.name;
            //if (GameConfig.RectangleSpring.Equals(rectName))
            //{
            //    this.transform.localPosition = new Vector3(other.transform.localPosition.x,transform.localPosition.y);
            //}
        }
    }
}