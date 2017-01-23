/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

namespace BaiyiGame.JumpMrQ
{
	/// <summary>
	/// Class in charge to animate the platform when the player jump
	/// </summary>
	public class PlatformLogic : MonoBehaviorHelper
	{

		public DiamondLogic diamond;

		public GameObject lastScore;

        public GameObject bestScore;

        public GameObject gleamingGo;

        public float disappearDistance = 10;

        public Vector2 position
		{
			set
			{
				transform.position = value;
			}
		}

        Collider2D col = null;

        private float normalY;
        private float sinkY;
        private float animtTime = 0.02f;

        void Awake()
		{
            col = GetComponent<Collider2D>();
			lastScore.SetActive(false);
			bestScore.SetActive(false);
			diamond.gameObject.SetActive(false);
            normalY = transform.localPosition.y;
            sinkY = transform.localPosition.y - Config.SINK_DISTANCE;
        }

		void OnEnable()
		{
            PlayerManager.OnPlayerJump += DoMove;
            col.enabled = true;
			transform.DOKill();
        }

		void OnDisable()
		{
            lastScore.SetActive(false);
			bestScore.SetActive(false);
			diamond.gameObject.SetActive(false);
			transform.DOKill();
            PlayerManager.OnPlayerJump -= DoMove;
        }

        void DoMove(bool isSmall, float animTime, float distX, float distY, float jumpHeight)
        {
            // 柱子超出摄像机一定的距离之后 隐藏柱子
            if (cam.transform.position.x - this.gameObject.transform.position.x > disappearDistance)
            {
                this.gameObject.SetActive(false);
            }
        }

        void OnDoElasticIn(Transform t)
        {
            DoElasticIn(t);
        }
        void OnDoElasticOut(Transform t)
        {
            DoElasticOut(t);
        }

        public void ActivateDiamond()
		{
			diamond.gameObject.SetActive(true);
		}

        /// <summary>
        /// 下压效果
        /// </summary>
        public void DoElasticIn()
        {
            gleamingGo.SetActive(true);
            transform.DOKill();
            transform.DOLocalMoveY(sinkY, animtTime).SetEase(Ease.Linear);
        }

        public void DoElasticIn(Transform other)
        {
            gleamingGo.SetActive(true);
            transform.DOKill();
            transform.DOLocalMoveY(sinkY, animtTime).SetEase(Ease.Linear);
            other.localPosition -= Vector3.up * Config.SINK_DISTANCE;
        }

        /// <summary>
        /// 上弹效果
        /// </summary>
        public void DoElasticOut()
        {
            transform.DOKill();
            transform.DOLocalMoveY(normalY, animtTime).SetEase(Ease.Linear);
        }

        public void DoElasticOut(Transform other)
        {
            transform.DOKill();
            transform.DOLocalMoveY(normalY, animtTime).SetEase(Ease.Linear);

            if (playerManager.jumpCount != 1)
            {
                other.localPosition += Vector3.up * Config.SINK_DISTANCE;
            }
        }
    }
}