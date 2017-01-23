using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BYSDKManage;
using PathologicalGames;
using BaiyiGame.JumpMrQ;
using System;

public class SettingManager : MonoBehaviour {
    //声音按钮
    public Toggle SoundBtn;
	//分享按钮
	public Button ShareBtn;
	//评论按钮
	public Button CommentBtn;
	//联系我们
	public Button ContactBtn;
	//支持我们
	public Button SupportBtn;

    public SoundManager soundManager;
    //声音开启的图片
    public Sprite onSound;
    //声音关闭的图片
    public Sprite offSound;

    void Awake()
    {
        bool isPlayGameMusic = PlayerPrefs.GetInt("isPlayGameMusic", 0) == 1;
        SoundBtn.GetComponent<Toggle>().isOn = isPlayGameMusic;
        swichImg(isPlayGameMusic);
    }
    // Use this for initialization
    void Start () {
        SoundBtn.onValueChanged.AddListener(OnSoundBtnClick);
        ShareBtn.onClick.AddListener (OnShareBtnClick);
		CommentBtn.onClick.AddListener (OnCommentBtnClick);
		ContactBtn.onClick.AddListener (OnContactBtnClick);
		SupportBtn.onClick.AddListener (OnSupportBtnClick);
    }
    private void OnSoundBtnClick(bool isPlayGameMusic)
    {
        soundManager.PlayMusicGame(isPlayGameMusic);
        swichImg(isPlayGameMusic);
        PlayerPrefs.SetInt("isPlayGameMusic", isPlayGameMusic?1:0);
    }

    private void swichImg(bool isPlayGameMusic)
    {
        Image soundImage = SoundBtn.GetComponent<Image>();
        if (isPlayGameMusic)
        {
            soundImage.sprite = onSound;
        }
        else
        {
            soundImage.sprite = offSound;
        }
    }

    //分享
    private void OnShareBtnClick () {
		ShareManage._instance.setShareHandler (OnSDKCallBack);
		ShareManage._instance.CreateSharePanle ();
	}
	//评论
	private void OnCommentBtnClick () {
		try {
			ShareManage._instance.CommentGame ();
		} catch (System.Exception e) {
			print ("SettingView CommentGame fail Message:" + e.Message);
		}
	}
	//联系我们
	private void OnContactBtnClick () {
		try {
			ShareManage._instance.SendEmailByOS ();
		} catch (System.Exception e) {
			print ("ShareView ContactUs fail Message:" + e.Message);
		}
 	}

	private void OnSupportBtnClick () {
		ShareManage._instance.ShowVides (OnSDKCallBack);
	}
	/// <summary>
	/// SDK 回调接口
	/// </summary>
	/// <param name="type">Type.返回类型</param>
	/// <param name="resulet">Resulet.返回结果</param>
	private void OnSDKCallBack (ActionType type, ActionResulet resulet) {
		switch (type) {
		case ActionType.Comment:
			if (ActionResulet.Success == resulet) {

			} else {

			}
			break;
		case ActionType.Share:
			if (ActionResulet.Success == resulet) {
				PlayerData.AddDiamond(BaiyiGame.JumpMrQ.Config.DIAMOND_HOMECOUNT);
				Debug.Log ("Share is successful");
			} else {

			}
			break;
		case ActionType.Video:
			if (ActionResulet.Success == resulet) {
				PlayerData.AddDiamond (BaiyiGame.JumpMrQ.Config.DIAMOND_HOMECOUNT);
				Debug.Log ("Video is successful");
			} else {

				ShowLoadFailed(LoadFailedArgs.None);
			}
			break;
		}

	}

	private void ShowLoadFailed (LoadFailedArgs type) {
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
		obj.CloseEvent += delegate () {
			PoolManager.Pools["UIPool"].Despawn(infoBox.transform);

		};
		obj.SetData(type);
	}
}
