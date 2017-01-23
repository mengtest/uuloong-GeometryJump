using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;
using BaiyiGame.JumpMrQ;
using BYSDKManage;
namespace BaiyiGame.JumpMrQ
{
    public class HomePageManager : MonoBehaviour
    {

        private GameObject soundManager;
        public Image settingPage;
        public Image homePage;
        public Image RoleSelPage;

        private CanvasGroup canvasGroup;

        [SerializeField]
        private RateDiamondManager rateDiamondManager;

        // 当钻石数量足够时，显示解锁对话框，调用此方法
        public delegate void UnLockRoleClick(GameObject role);
        public static event UnLockRoleClick OnUnLockRoleClick;

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(1f, 1f);

            // 解锁默认角色
            if (PlayerData.IsRoleLock(Constants.DEFAULE_ROLE))
            {
                PlayerData.UnLockRole(Constants.DEFAULE_ROLE);
            }
            SDKEventManager.Instance.Init();
        }

        void OnEnable()
        {
            RoleSelectManager.OnRoleSelectedClick += OnRoleSelectedClick;
        }

        void OnDisable()
        {
            RoleSelectManager.OnRoleSelectedClick -= OnRoleSelectedClick;
        }

        /// <summary>
        /// 当选中的角色再次点击的时候开始游戏
        /// </summary>
        void OnRoleSelectedClick()
        {
            StartGame();
        }


        /// <summary>
        /// 设置按钮点击事件
        /// </summary>
        public void SettingClick()
        {
            settingPage.gameObject.SetActive(true);
            homePage.gameObject.SetActive(false);
        }


        /// <summary>
        /// 设置界面的返回按钮
        /// </summary>
        public void SettingToHome()
        {
            homePage.gameObject.SetActive(true);
            settingPage.gameObject.SetActive(false);
        }

        /// <summary>
        /// 角色选择界面返回主界面
        /// </summary>
        public void RoleToHome()
        {
            homePage.gameObject.SetActive(true);
            RoleSelPage.gameObject.SetActive(false);
        }

        /// <summary>
        /// 进入角色选择界面
        /// </summary>
        public void EntryRoleSelect()
        {
            RoleSelPage.gameObject.SetActive(true);
            homePage.gameObject.SetActive(false);
        }

        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        public void StartGame()
        {
            Application.LoadLevel("GameScene");
        }

        public void ShowLeaderBoard()
        {
            Debug.Log("ShowLeaderBoard");
            GameCenterM.GetInstance().ShowLeaderboard();
        }

        public void ShowMoreGame()
        {
            Debug.Log("ShowMoreGame");
            ShareManage._instance.MoreGame();
        }
    }
}