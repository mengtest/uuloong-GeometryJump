using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using BaiyiGame.JumpMrQ;
using BYSDKManage;
using PathologicalGames;

public class RateDiamondManager : MonoBehaviour {

    public Button buttonClose;
	//分享按钮
	public Button ShareBtn;
	//视频按钮
	public Button VideoBtn;
	//评论按钮
	public Button CommentBtn;

    private CanvasGroup BgCanvasGroup;
    public Button buttonPause;
    [SerializeField]
    private SettleAccountControl settleAccountControl;
    // Use this for initialization
    void Start () {
        if (PlayerData.IsCommented())
        {
            CommentBtn.gameObject.SetActive(false);
        }
        else
        {
            CommentBtn.onClick.AddListener(OnCommentBtnClick);
        }
        BgCanvasGroup = GetComponent<CanvasGroup>();
        buttonClose.onClick.AddListener(ButtonCloseClick);
		ShareBtn.onClick.AddListener (OnShareBtnClick);
		VideoBtn.onClick.AddListener (OnVideoBtnClick);
		
	}

	private void OnShareBtnClick () {
		ShareManage._instance.setShareHandler (OnSDKCallBack);
		ShareManage._instance.CreateSharePanle ();
	}

	private void OnVideoBtnClick () {
		ShareManage._instance.ShowVides (OnSDKCallBack);
	}

	private void OnCommentBtnClick () {
		ShareManage._instance.CommentGame ();
		OnSDKCallBack (ActionType.Comment,ActionResulet.Success);
	}

    void ButtonCloseClick() {
        Tweener tw= BgCanvasGroup.DOFade(0f, 0.5f);
        tw.OnComplete<Tweener>(()=> {
            this.gameObject.SetActive(false);
            settleAccountControl.gameObject.SetActive(true);
            settleAccountControl.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
        });
    }

	/// <summary>
	/// SDK 回调接口
	/// </summary>
	/// <param name="type"></param>
	/// <param name="resulet"></param>
	public void OnSDKCallBack(ActionType type, ActionResulet resulet) {
		switch (type) {
		case ActionType.Comment:
			if (ActionResulet.Success == resulet) {
				if (ShareDataModel.GetInstance().CommentCount < BYSDKManage.Constants.LIMITED_COMMENT_COUNT) {
					    //ShareDataModel.GetInstance().GamePoints += BYSDKManage.Constants.POINTS_INCOME_COMMENT;
					    PlayerData.AddDiamond(BaiyiGame.JumpMrQ.Config.DIAMOND_GAMECOUNT);
					    Message(string.Format(BYSDKManage.Constants.GET_POINT, BaiyiGame.JumpMrQ.Config.DIAMOND_GAMECOUNT));
					    ShareDataModel.GetInstance().CommentCount++;
                        PlayerData.SetComment(1);
                        CommentBtn.gameObject.SetActive(false);
                    }
			}
			break;
		case ActionType.Share:
			if (ActionResulet.Success == resulet) {
				if (ShareDataModel.GetInstance().ShareCount < BYSDKManage.Constants.LIMITED_DAY_SHARE_COUNT) {
					//ShareDataModel.GetInstance().GamePoints += BYSDKManage.Constants.POINTS_INCOME_SHARE;
					PlayerData.AddDiamond(BaiyiGame.JumpMrQ.Config.DIAMOND_GAMECOUNT);
					Message(string.Format(BYSDKManage.Constants.GET_POINT, BaiyiGame.JumpMrQ.Config.DIAMOND_GAMECOUNT));
					ShareDataModel.GetInstance().ShareCount++;
				}
			} else {

				Message(string.Format("Share Failed"));
			}
			break;
		case ActionType.Video:
			if (ActionResulet.Success == resulet) {
				//ShareDataModel.GetInstance().GamePoints += BYSDKManage.Constants.POINTS_INCOME_VIDEO_AD;
				PlayerData.AddDiamond(BaiyiGame.JumpMrQ.Config.DIAMOND_GAMECOUNT);
				Message(string.Format(BYSDKManage.Constants.GET_POINT, BaiyiGame.JumpMrQ.Config.DIAMOND_GAMECOUNT));
			} else {
				if (ActionResulet.Skipe == resulet) {

				} else {
					// 评论次数
					int commentCount = ShareDataModel.GetInstance().CommentCount;
					// 今日分享次数
					int todayShareCount = ShareDataModel.GetInstance().ShareCount;
					// 未评论且今日分享次数小于等于3次
					if (commentCount == 0 && todayShareCount < BYSDKManage.Constants.LIMITED_DAY_SHARE_COUNT) {
						ShowLoadFailed (LoadFailedArgs.All);
						return;
					}
					// 没有评论过 且今日分享次数大于3次
					if (commentCount == 0 && todayShareCount >= BYSDKManage.Constants.LIMITED_DAY_SHARE_COUNT) {
						ShowLoadFailed (LoadFailedArgs.OnlyCommon);
						return;
					}
					// 评论过 且今日分享次数小于等于3次
					if (commentCount != 0 && todayShareCount < BYSDKManage.Constants.LIMITED_DAY_SHARE_COUNT) {
						ShowLoadFailed (LoadFailedArgs.OnlyShare);
						return;
					}
					// 提示 加载失败 请稍后再试
					ShowLoadFailed(LoadFailedArgs.None);
				}
			}
			break;
		}
		ButtonCloseClick ();
	}

	private void ShowLoadFailed(LoadFailedArgs type) {
		GameObject publicPanel = GameObject.FindGameObjectWithTag ("PublicPanel");
		Transform transform = PoolManager.Pools["UIPool"].Spawn("LoadFailed");
		transform.SetParent(publicPanel.transform);
		transform.localPosition = new Vector3(0, 0, 0);
		transform.localScale = new Vector3(1, 1, 1);

		GameObject infoBox = transform.gameObject;
		infoBox.transform.localScale = new Vector3(1, 1, 1);
		infoBox.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
		infoBox.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
		LoadFailed obj = infoBox.GetComponent<LoadFailed>();
		obj.CloseEvent +=delegate () {
			PoolManager.Pools["UIPool"].Despawn(infoBox.transform);
		};
		obj.SetData(type);
	}

	private void Message (string count) {
		GameObject infoBox = InstantiateUtil.GenerateFromPrefab (BYSDKManage.Constants.PREFAB_PATH_MSBOX,GameObject.Find("PublicPanel").transform);
		infoBox.transform.localPosition = new Vector3(0, 0, 0);
		infoBox.transform.localScale = new Vector3(1, 1, 1);
		infoBox.GetComponent<InformationCtrl> ().SetContent (count);
	}
}
