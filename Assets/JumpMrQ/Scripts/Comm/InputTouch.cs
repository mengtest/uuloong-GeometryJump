/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using System.Collections;

/// <summary>
/// Class in charge to manage input touch and desktop input in the game
/// </summary>
namespace BaiyiGame.JumpMrQ
{
    public class InputTouch : MonoBehaviour
    {
        public delegate void TouchLeft();
        public static event TouchLeft OnTouchLeft;

        public delegate void TouchRight();
        public static event TouchRight OnTouchRight;

        public delegate void TouchScreen();
        public static event TouchScreen OnTouchScreen;

        public delegate void SlideLeft();
        public static event SlideLeft OnSlideLeft;

        public delegate void SlideRight();
        public static event SlideRight OnSlideRight;

#if UNITY_TVOS
		private Vector2 startPosition;
		void OnEnable()
		{
		GameManager.OnGameStart += OnGameStart;

		GameManager.OnGameOver += OnGameOver;
		}
		void OnDisable()
		{
		GameManager.OnGameStart -= OnGameStart;

		GameManager.OnGameOver -= OnGameOver;
		}

		bool gameStarted = true;

		void OnGameStart()
		{
		UnityEngine.Apple.TV.Remote.touchesEnabled = true;
		UnityEngine.Apple.TV.Remote.allowExitToHome = false;

		gameStarted = true;
		}

		void OnGameOver()
		{
		UnityEngine.Apple.TV.Remote.touchesEnabled = false;
		UnityEngine.Apple.TV.Remote.allowExitToHome = true;

		gameStarted = false;
		}

		void Start()
		{
		UnityEngine.Apple.TV.Remote.reportAbsoluteDpadValues = true;
		}


#endif

        //手指动作的敏感度，这里设定为 二十分之一的屏幕宽度.
        private float fingerActionSensitivity = Screen.width * 0.05f;
        private Vector2 beginPos = Vector2.zero;
        private Vector2 endPos = Vector2.zero;
        private bool isSlideHandle = false;

        void Update()
        {
            #region 判断左点右点逻辑
#if UNITY_TVOS
			if(!gameStarted)
			{
			return;
			}

			float h = Input.GetAxis("Horizontal");

			if(h < 0)
			{
			_OnTouchLeft();
			}
			else if(h > 0)
			{
			_OnTouchRight();
			}

			int nbTouches = Input.touchCount;

			if(nbTouches > 0)
			{

			Touch touch = Input.GetTouch(0);

			TouchPhase phase = touch.phase;

			print("" + phase.ToString() + " position = " + touch.position);

			}

#endif

#if (UNITY_ANDROID || UNITY_IOS || UNITY_TVOS)
            int nbTouches = Input.touchCount;

            if (nbTouches > 0)
            {

                Touch touch = Input.GetTouch(0);

                TouchPhase phase = touch.phase;

                if (phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2f)
                    {
                        _OnTouchLeft();
                    }
                    else
                    {
                        _OnTouchRight();
                    }
                }
            }
#endif

#if (!UNITY_ANDROID && !UNITY_IOS && !UNITY_TVOS) || UNITY_EDITOR

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _OnTouchLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _OnTouchRight();
            }

#endif
            #endregion


            #region 判断是否滑动逻辑

#if (UNITY_ANDROID || UNITY_IOS || UNITY_TVOS)
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                TouchPhase phase = touch.phase;
                if (phase == TouchPhase.Began) {
                    beginPos = Input.mousePosition;
                }

                if (!isSlideHandle && phase == TouchPhase.Moved)
                {
                    endPos = Input.mousePosition;
                    if (Mathf.Abs(endPos.x - beginPos.x) >= fingerActionSensitivity)
                    {
                        isSlideHandle = true;
                        if (endPos.x > beginPos.x)
                        {
                            if (OnSlideRight != null)
                            {
                                OnSlideRight();
                            }
                        }
                        else
                        {
                            if (OnSlideLeft != null)
                            {
                                OnSlideLeft();
                            }
                        }
                    }
                }
                if (phase == TouchPhase.Ended || phase == TouchPhase.Canceled)
                {
                    endPos = Vector2.zero;
                    beginPos = Vector2.zero;
                    isSlideHandle = false;
                }
            }
#endif

#if (!UNITY_ANDROID && !UNITY_IOS && !UNITY_TVOS) || UNITY_EDITOR

            if (Input.GetMouseButtonDown(0))
            {
                beginPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                endPos = Input.mousePosition;
                if (Mathf.Abs(endPos.x - beginPos.x) >= fingerActionSensitivity)
                {
                    if (endPos.x > beginPos.x)
                    {
                        if (OnSlideRight != null)
                        {
                            OnSlideRight();
                        }
                    }
                    else
                    {
                        if (OnSlideLeft != null)
                        {
                            OnSlideLeft();
                        }
                    }
                }
            }

#endif
            #endregion
        }

        void _OnTouchLeft()
        {
            if (OnTouchScreen != null)
                OnTouchScreen();

            if (OnTouchLeft != null)
                OnTouchLeft();
        }

        void _OnTouchRight()
        {
            if (OnTouchScreen != null)
                OnTouchScreen();

            if (OnTouchRight != null)
                OnTouchRight();
        }
    }
}