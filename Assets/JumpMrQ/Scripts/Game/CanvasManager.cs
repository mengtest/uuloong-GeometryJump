/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




#pragma warning disable 0618 

using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

/// <summary>
/// Class in charge to manage all the UI in the game
/// </summary>
namespace BaiyiGame.JumpMrQ
{
    public class CanvasManager : MonoBehaviorHelper
    {
        /// <summary>
        /// We show ads - interstitials - ever 10 game over by default. To change it, change this number. You have to get "Very Simple Ad" from the asset store to use it: http://u3d.as/oWD
        /// </summary>
        public int numberOfPlayToShowInterstitial = 5;

        public string VerySimpleAdsURL = "http://u3d.as/oWD";

        [SerializeField]
        private Text scoreText;

        [SerializeField]
        private CanvasGroup canvasGroupInstruction;
        [SerializeField]
        private CanvasGroup canvasGroupScore;
        [SerializeField]
        private CanvasGroup canvasGroupGameOver;

        [SerializeField]
        private Text diamondText;

        [SerializeField]
        private Button buttonContinueWithLife;
        [SerializeField]
        private Button buttonContinueWithDiamond;
        [SerializeField]
        private Button buttonRestart;
        [SerializeField]
        private Button btnPause;
        [SerializeField]
        private BtnPauseControl btnPauseControl;
        [SerializeField]
        private RateDiamondManager rateDiamondManager;

        public AnimationTransitionManager m_animationTransitionManager;
        public AnimationTransition m_animationTransition;

        public delegate void AnimationTransitionInStart();
        public static event AnimationTransitionInStart OnAnimationTransitionInStart;

        public delegate void AnimationTransitionInEnd();
        public static event AnimationTransitionInEnd OnAnimationTransitionInEnd;

        public delegate void AnimationTransitionOutStart();
        public static event AnimationTransitionOutStart OnAnimationTransitionOutStart;

        public delegate void AnimationTransitionOutEnd();
        public static event AnimationTransitionOutEnd OnAnimationTransitionOutEnd;

        public delegate void OnRestart();
        public static event OnRestart onRestart;

        //暂停游戏
        public delegate void _GamePause();
        public static event _GamePause OnGamePause;

        public void _AnimationTransitionInStart()
        {
            if (OnAnimationTransitionInStart != null)
                OnAnimationTransitionInStart();
        }
        public void _AnimationTransitionInEnd()
        {
            if (OnAnimationTransitionInStart != null)
                OnAnimationTransitionInEnd();
        }
        public void _AnimationTransitionOutStart()
        {
            if (OnAnimationTransitionOutStart != null)
                OnAnimationTransitionOutStart();
        }
        public void _AnimationTransitionOutEnd()
        {
            if (OnAnimationTransitionOutEnd != null)
                OnAnimationTransitionOutEnd();
        }


        float timeAlphaAnim = Config.ANIM_TIME_MID;

        float alphaInstruction
        {
            get
            {
                return canvasGroupInstruction.alpha;
            }

            set
            {
                canvasGroupInstruction.alpha = value;
            }
        }

        void OnEnable()
        {
            GameManager.OnSetPoint += SetPointText;

            PlayerData.OnSetDiamond += SetDiamondText;

            GameManager.OnGameOverEnded += OnGameOverEnded;
        }

        void OnDisable()
        {
            GameManager.OnSetPoint -= SetPointText;

            PlayerData.OnSetDiamond -= SetDiamondText;

            GameManager.OnGameOverEnded -= OnGameOverEnded;
        }
        void Start()
        {
            SetPointText(0);

            alphaInstruction = 1f;

            canvasGroupGameOver.alpha = 0;

            canvasGroupGameOver.gameObject.SetActive(false);

            SetDiamondText(PlayerData.diamond);

            btnPause.onClick.AddListener(BtnPauseClick);
        }

        public void BtnPauseClick()
        {
            btnPauseControl.gameObject.SetActive(true);
            CanvasGroup c = btnPauseControl.GetComponent<CanvasGroup>();
            c.DOFade(0.8f,1f);
            if(OnGamePause != null)
            {
                OnGamePause();
            }
        }

        void ButtonLogic()
        {
            bool adsInitialized = false;

#if APPADVISORY_ADS
			adsInitialized = AdsManager.instance.IsReadyRewardedVideo ();
#endif

            bool haveLife = gameManager.HaveLife();
            bool haveEnoughtDiamond = PlayerData.diamond >= Config.DIAMOND_TO_CONTINUE;

            ActivateButton(buttonContinueWithLife, haveLife);
        }

        void ActivateButton(Button b, bool activate)
        {
            b.GetComponent<CanvasGroup>().alpha = activate ? 1f : 0.9f;
            float a = b.GetComponent<CanvasGroup>().alpha;
            b.interactable = activate;
        }

        void OnGameOverEnded()
        {
            ButtonLogic();

            canvasGroupGameOver.alpha = 0;

            canvasGroupGameOver.gameObject.SetActive(true);
            btnPause.gameObject.SetActive(false);
            btnPause.gameObject.SetActive(false);

            canvasGroupGameOver.DOFade(0.8f, timeAlphaAnim)
                .OnComplete(() =>
                {
                    AddButtonListener();
                });

#if !(UNITY_ANDROID || UNITY_IOS) && UNITY_TVOS
			for(int i = 0; i < tc.childCount; i++)
			{
			var t = tc.GetChild(i);
			if(tc.gameObject.activeInHierarchy)
			{
			var es = FindObjectOfType<EventSystem>();
			es.firstSelectedGameObject = t.gameObject;
			es.SetSelectedGameObject(t.gameObject);

			print("set selected: " + t.name);
			break;
			}
			}
#endif
        }

        void SetPointText(int point)
        {
            scoreText.text = point.ToString();
        }

        void SetDiamondText(int diamond)
        {
            diamondText.text =  diamond.ToString();
        }

        public void SetCanvasGroupInstructionAlpha(float fromA, float toA)
        {
            DOVirtual.Float(fromA, toA, timeAlphaAnim, (float f) =>
            {
                alphaInstruction = f;
            })
                .OnComplete(() =>
                {
                    alphaInstruction = toA;

                    if (toA == 0)
                    {
                        canvasGroupGameOver.gameObject.SetActive(false);
                    }
                });
        }

        public void SetCanvasGroupGameOverAlpha(float fromA, float toA)
        {
            DOVirtual.Float(fromA, toA, timeAlphaAnim, (float f) =>
            {
                canvasGroupGameOver.alpha = f;
            })
                .OnComplete(() =>
                {
                    canvasGroupGameOver.alpha = toA;

                    if (toA == 0)
                    {
                        canvasGroupGameOver.gameObject.SetActive(false);
                    }

                    AddButtonListener();
                });
        }

        void AddButtonListener()
        {
            bool adsInitialized = false;

#if APPADVISORY_ADS
			adsInitialized = AdsManager.instance.IsReadyRewardedVideo ();
#endif

            bool haveLife = gameManager.HaveLife();
            bool haveEnoughtDiamond = PlayerData.diamond >= Config.DIAMOND_TO_CONTINUE;


            ActivateButton(buttonContinueWithLife, haveLife);

            buttonRestart.interactable = true;
        }

        void RemoveListener()
        {
            buttonContinueWithLife.interactable = false;
            buttonRestart.interactable = false;
        }

        public void OnClickedButton(GameObject b)
        {
            if (!b.GetComponent<Button>().interactable)
                return;

            if (b.name.Contains("ContinueWithLife"))
                OnClickedContinueWithLife();
            else if (b.name.Contains("BtnContinue"))
                OnClickedContinueWithDiamond();
            else if (b.name.Contains("ButtonGet3Life"))
                OnClickedGetFreeLife();
            else if (b.name.Contains("ButtonGet300Diamonds"))
                OnClickedGetFreeDiamonds();
            else if (b.name.Contains("GameOver"))
                OnClickedRestart();
        }

        void OnClickedContinueWithLife()
        {
            RemoveListener();
            SetCanvasGroupGameOverAlpha(1, 0);
            gameManager.AddLife(-1);
            playerManager.Continue();
        }

        public void OnClickedContinueWithDiamond()
        { 
            bool haveEnoughtDiamond = PlayerData.diamond >= Config.DIAMOND_TO_CONTINUE;
            if (!haveEnoughtDiamond)
            {
                btnPause.gameObject.SetActive(false);
                canvasGroupGameOver.gameObject.SetActive(false);
                rateDiamondManager.gameObject.SetActive(true);
                rateDiamondManager.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
            }
            else
            {
                btnPause.gameObject.SetActive(true);
                RemoveListener();
                SetCanvasGroupGameOverAlpha(1, 0);
                PlayerData.diamond -= Config.DIAMOND_TO_CONTINUE;
                playerManager.Continue();
            }
        }

        void OnClickedGetFreeLife()
        {
            RemoveListener();
        }

        void OnClickedGetFreeDiamonds()
        {
            RemoveListener();
        }

        /// <summary>
        /// If using Very Simple Ads by App Advisory, show an interstitial if number of play > numberOfPlayToShowInterstitial: http://u3d.as/oWD
        /// </summary>
        public void ShowAds()
        {

        }

        public void OnClickedRestart()
        {
            onRestart();
            var an = FindObjectsOfType<AnimButtonHierarchy>();

            foreach (var a in an)
            {
                if (a.gameObject.activeInHierarchy)
                    a.DoAnimOut();
            }

            DOTween.KillAll();

            ShowAds();

            m_animationTransition.DoAnimationIn(() =>
            {
                RemoveListener();
                StopAllCoroutines();

#if UNITY_5_3_OR_NEWER
                DOTween.KillAll();

                GC.Collect();

                Resources.UnloadUnusedAssets();

                SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

                Resources.UnloadUnusedAssets();

                GC.Collect();
#endif
                Application.LoadLevel(Application.loadedLevel);
            });
        }
    }
}