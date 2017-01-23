/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// Class in charge to move the big hazard on the left continuously during the game
/// </summary>
namespace BaiyiGame.JumpMrQ
{
    public class ContinuousMove : MonoBehaviorHelper
    {

        public bool BLOCK_ME = false;

        void OnEnable()
        {
            GameManager.OnGameStart += DoMove;
            GameManager.OnGameOverStarted += StopMove;
            GameManager.OnSetPoint += OnSetPoint;
            SeasonManager.onSwitchShow += OnSwitchShow;
            PlayerManager.OnPlayerJumpStarted += DoMove;
        }

        void OnDisable()
        {
            GameManager.OnGameStart -= DoMove;
            GameManager.OnGameOverStarted -= StopMove;
            GameManager.OnSetPoint -= OnSetPoint;
            SeasonManager.onSwitchShow -= OnSwitchShow;
            PlayerManager.OnPlayerJumpStarted -= DoMove;
        }

        bool isMoving = false;

        public void StopMove()
        {
            isMoving = false;
        }

        public void DoMove()
        {
            isMoving = true;
        }

        [SerializeField]
        private float speed = 1;

        void OnSetPoint(int p)
        {
            speed++;
        }

        void OnSwitchShow(string season)
        {
            speed += 60;
        }

        public List<Transform> l;
        string curRole;
        void Awake()
        {
        }
        void Start()
        {
            Reposition();
        }

        float GetDistance()
        {
            float d = playerManager.transform.position.x - transform.position.x;
            return d;
        }

        void Update()
        {
            if (BLOCK_ME)
            {
                return;
            }

            if (!isMoving)
                return;

            transform.Translate((GetDistance() + speed / 100f) * Time.deltaTime, 0, 0, Camera.main.transform);
            Vector2 pos = transform.position;
            transform.position = pos;
        }

        public void Reposition()
        {
            // 调整位置使其在屏幕的最左端
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            var p = transform.position;
            // 向右3个单位
            //p.x = cam.transform.position.x - width/2f + 2;
            //         transform.position = p;
            p.x = cam.transform.position.x - width / 2f;
            transform.position = p;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //被碰撞的物体
            string otherNmae = other.gameObject.name;
            if (!GameConfig.Rectangle.Equals(otherNmae) || !GameConfig.RectangleSpring.Equals(otherNmae)
                || !GameConfig.RectangleSummer.Equals(otherNmae) || !GameConfig.RectangleAutumn.Equals(otherNmae)
                || !GameConfig.RectangleWinter.Equals(otherNmae) || !GameConfig.SceneStartCollider.Equals(otherNmae)
                || !GameConfig.SceneEndCollider.Equals(otherNmae))
            {
                var platform = other.GetComponent<PlatformLogic>();

                if (platform != null)
                {
                    platform.DoElasticOut();
                    return;
                }

                var player = other.GetComponent<PlayerManager>();
                if (player != null)
                {
                    player.LaunchGameOver();
                    return;
                }
            }
        }


        public void DoContinueRestart()
        {
            transform.position += 2 * Vector3.down;
        }
    }
}