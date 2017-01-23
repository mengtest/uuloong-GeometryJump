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
using System;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// Class in charge to manage the player
/// </summary>
namespace BaiyiGame.JumpMrQ
{
    public class PlayerManager : MonoBehaviorHelper
    {
        public int jumpCount = 0;

        private bool canJump;

        private bool isGameOver;

        private Vector3 originPos;

        private Transform spriteTransform;
        public List<Sprite> listMask;

        public Button buttonPause;

        void OnAnimationTransitionOutStart()
        {
            CanvasManager.OnAnimationTransitionOutStart -= OnAnimationTransitionOutStart;
        }
        public delegate void PlayerJumpStart();
        public static event PlayerJumpStart OnPlayerJumpStarted;

        public delegate void PlayerJumpEnd();
        public static event PlayerJumpEnd OnPlayerJumpEnded;

        public delegate void PlayerJump(bool isSmall, float animTime, float distX, float distY, float jumpHeight);
        public static event PlayerJump OnPlayerJump;

        //游戏结束
        public delegate void GameOver();
        public static event GameOver OnGameOver;


        void OnEnable()
        {
            CanvasManager.OnGamePause += OnGamePause;
            originPos = this.transform.position;
            InputTouch.OnTouchLeft += OnTouchLeft;
            InputTouch.OnTouchRight += OnTouchRight;
            CanvasManager.OnAnimationTransitionOutStart += OnAnimationTransitionOutStart;
            BtnPauseControl.OnGameContinue += OnGameContinue;
        }

        void OnDisable()
        {
            InputTouch.OnTouchLeft -= OnTouchLeft;
            InputTouch.OnTouchRight -= OnTouchRight;
            CanvasManager.OnAnimationTransitionOutStart -= OnAnimationTransitionOutStart;
            CanvasManager.OnGamePause -= OnGamePause;
            BtnPauseControl.OnGameContinue -= OnGameContinue;
        }

        bool isGamePause = false;
        /// <summary>
        /// 暂停游戏按钮按下，回调
        /// </summary>
        void OnGamePause()
        {
            isGamePause = true;
            canJump = false;
            isGameOver = false;
            continuousMove.StopMove();
            buttonPause.gameObject.SetActive(false);

        }

        /// <summary>
        /// 继续游戏
        /// </summary>
        void OnGameContinue()
        {
            isGamePause = false;
            canJump = true;
            isGameOver = false;
            buttonPause.gameObject.SetActive(true);
        }

        void OnTouchLeft()
        {
            if (isValidTouch())
            {
                DoMove(true);
            }
        }

        void OnTouchRight()
        {
            if (isValidTouch())
            {
                DoMove(false);
            }
        }

        /// <summary>
        /// 屏幕的下3/4为可以点击的区域
        /// </summary>
        /// <returns></returns>
        bool isValidTouch()
        {
#if (UNITY_ANDROID || UNITY_IOS || UNITY_TVOS)
            {
                int nbTouches = Input.touchCount;

                if (nbTouches > 0)
                {
                    
                    Touch touch = Input.GetTouch(0);
                    if (touch.position.y < Screen.height* 3.0 / 4.0)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }
#endif
            return true;
        }

        void Awake()
        {
            string curRole = PlayerData.CurRole;
            if (string.IsNullOrEmpty(curRole))
            {
                curRole = Constants.DEFAULE_ROLE;
            }

            var go = LoadPlayer(curRole);
            spriteTransform = go.transform;

            Constants.SELECT_ROLE_NAME = go.name;
        }

        void Start()
        {
            isGameOver = false;
            canJump = true;
        }

        GameObject LoadPlayer(string player)
        {
            string prefab = "Prefabs/Roles/" + player;
            var go = Instantiate(Resources.Load(prefab) as GameObject);
            go.transform.parent = this.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            return go;
        }

        public void LaunchGameOver()
        {
            if (isGameOver)
                return;

            //游戏结束
            if (null != OnGameOver)
            {
                OnGameOver();
            }


            isGameOver = true;

            StopAllCoroutines();

            StartCoroutine(CoroutLaunchGameOver());
        }

        IEnumerator CoroutLaunchGameOver()
        {
            gameManager.GameOverStart();

            soundManager.PlayMusicGameOver();

            yield return 0;

            StartCoroutine(CameraShake.Shake(Camera.main.transform, 0.1f));

            yield return new WaitForSeconds(1f);

            gameManager.GameOverEnd();

            float height = 2f * cam.orthographicSize;

            float finalPosY = transform.position.y - height;

            transform.DOMoveY(finalPosY, 1f).SetEase(Ease.Linear);
        }


        public void Continue()
        {
            isGameOver = false;
            canJump = true;
            continuousMove.Reposition();
            var pl = gameManager.gameObject.GetComponentsInChildren<PlatformLogic>(false);

            var oldPos = transform.position;

            var p = pl[0];
            int len = pl.Length;
            for (int i = 0; i < len - 1; i++)
            {
                // 判读浮点数是否接近，不能直接用==判断
                if (Mathf.Approximately(pl[i].transform.position.x, oldPos.x))
                {
                    p = pl[i];
                    break;
                }
                if (Mathf.Approximately(pl[i + 1].transform.position.x, oldPos.x))
                {
                    p = pl[i + 1];
                    break;
                }
                if (pl[i].transform.position.x < oldPos.x && pl[i + 1].transform.position.x > oldPos.x)
                {
                    p = pl[i];
                    break;
                }
            }

            // 柱子和角色下沉
            p.transform.position -= Vector3.up * Config.SINK_DISTANCE;
            transform.position = new Vector3(p.transform.position.x, originPos.y - Config.SINK_DISTANCE, originPos.z);
            mainCameraManager.UpdatePos(transform.position, delegate ()
            {
                continuousMove.Reposition();
            });
        }

        [NonSerialized]
        public float animTime = 0.12f;

        bool isDoingMoveX = false;

        bool isDoingMoveY = false;

        bool isMoving
        {
            get
            {
                return isDoingMoveX || isDoingMoveY;
            }
        }



        float GetFinalPosX(bool isSmall)
        {
            float startPosX = transform.localPosition.x;

            float finalPosX = startPosX + (isSmall ? gameManager.smallSpace : 2 * gameManager.smallSpace);

            return finalPosX;
        }


        float GetFinalPosY(bool isSmall)
        {
            float startPosY = transform.position.y;

            float finalPosY = startPosY + (isSmall ? 0 : 0);
            return finalPosY;
        }

        Vector3 GetFinalVector(bool isSmall)
        {
            return new Vector3(GetFinalPosX(isSmall), GetFinalPosY(isSmall), 0);
        }

        void DoMove(bool isSmall)
        {
            if (!canJump || isGameOver)
                return;

            if (isMoving)
                return;

            jumpCount++;

            soundManager.PlayJumpFX();

            var hit = Physics2D.Raycast(transform.localPosition, Vector3.down, 2);
            if (hit.collider)
            {
                var platformLogic = hit.collider.GetComponent<PlatformLogic>();
                if (platformLogic != null)
                    platformLogic.DoElasticOut(this.transform);
                else
                {
                    Debug.LogWarning("error!!");
                }
            }

            mainCameraManager.UpdatePos(GetFinalVector(isSmall), null);

            if (OnPlayerJumpStarted != null)
                OnPlayerJumpStarted();

            if (OnPlayerJump != null)
                OnPlayerJump(isSmall, animTime, GetFinalPosX(isSmall) - transform.localPosition.x, GetFinalPosY(isSmall) - transform.position.y, jumpHeight);

            DoMoveY(isSmall, () =>
            {
                if (!isMoving)
                    CheckIfOnPlatform(isSmall);
            });
            DoMoveX(isSmall, () =>
            {
                if (!isMoving)
                    CheckIfOnPlatform(isSmall);
            });
        }


        void CheckIfOnPlatform(bool isSmall)
        {
            if (OnPlayerJumpEnded != null)
                OnPlayerJumpEnded();

            var hit = Physics2D.Raycast(transform.localPosition, Vector3.down, 2);
            if (hit.collider)
            {
                gameManager.AddPoint(isSmall ? 1 : 2);
                var platformLogic = hit.transform.gameObject.GetComponent<PlatformLogic>();
                if (platformLogic != null)
                {
                    platformLogic.DoElasticIn(this.transform);
                }
            }
            else
            {
                LaunchGameOver();
            }
        }

        float jumpHeight = 0.8f;

        void DoMoveY(bool isSmall, Action callback)
        {
            if (isGamePause)
                return;
            float startPosY = transform.position.y;

            float finalPosY = GetFinalPosY(isSmall);

            float timeJump = animTime / 2f;

            var pos = transform.position;
            pos.y = startPosY;
            transform.position = pos;

            isDoingMoveY = true;

            transform.DOMoveY(finalPosY + jumpHeight, timeJump)
                .OnUpdate(() =>
                {
                    isDoingMoveY = true;
                })
                .OnComplete(() =>
                {
                    transform.DOMoveY(finalPosY, timeJump)
                        .SetEase(Ease.Linear)
                        .OnUpdate(() =>
                        {
                            isDoingMoveY = true;
                        })
                        .OnComplete(() =>
                        {
                            pos = transform.position;
                            pos.y = finalPosY;
                            transform.position = pos;

                            isDoingMoveY = false;

                            if (callback != null)
                                callback();
                        });
                });
        }

        void DoMoveX(bool isSmall, Action callback)
        {
            if (isGamePause)
                return;

            float startPosX = transform.position.x;

            float finalPosX = GetFinalPosX(isSmall);

            float timeJump = animTime;

            var pos = transform.localPosition;
            // pos.x = startPosX;
            transform.localPosition = pos;

            isDoingMoveX = true;

            transform.DOMoveX(finalPosX, timeJump)
                .OnUpdate(() =>
                {
                    isDoingMoveX = true;
                })
                .OnComplete(() =>
                {
                    pos = transform.localPosition;
                    //pos.x = finalPosX;
                    transform.position = pos;

                    isDoingMoveX = false;

                    if (callback != null)
                        callback();
                });
        }
    }
}