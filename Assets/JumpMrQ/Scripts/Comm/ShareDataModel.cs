using UnityEngine;
using System.Collections;

public class ShareDataModel {

	//单例
	private static ShareDataModel _instance = null;

	//评论次数
	private int _commentCount;

	//分享次数
	private int _shareCount;

	//时间戳作为Key值，保证每天次数重置
	private string _timeStr;

	public static ShareDataModel GetInstance () {
		if (_instance == null) {
			_instance = new ShareDataModel ();
			_instance.Init ();
		}
		return _instance;
	}

	private void Init () {
		_timeStr = System.DateTime.Now.ToShortDateString ();

		CommentCount = PlayerPrefs.GetInt ("CommentCount",0);
		ShareCount = PlayerPrefs.GetInt (_timeStr,0);
	}

	public int CommentCount {
		get {
			return _commentCount;
		} set {
			_commentCount = value;
			PlayerPrefs.SetInt ("CommentCount",_commentCount);
		}
	}

	public int ShareCount {
		get {
			return _shareCount;
		} set {
			_shareCount = value;
			PlayerPrefs.SetInt (_timeStr, _shareCount);
		}
	}
}
