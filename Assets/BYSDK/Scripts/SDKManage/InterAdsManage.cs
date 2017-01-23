using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.SDKManage
*
*	describe	:	N/A
*	class name	:	InterAdsManage
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/17 14:30:55				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public    class InterAdsManage {
    List<InterstitialAds>   _WorkList               = new List<InterstitialAds>();               // 工作队列
    List<InterstitialAds> _TryList                  = new List<InterstitialAds>();                 // 二次尝试队列
    List<InterstitialAds>   _FreedList              = new List<InterstitialAds>();
    InterstitialAds         _interAdsV              = null;
    List<AdsStage>           _ConfigureData;


    bool hasSpInter = true;
    int _confIndex = -1;                                                 // 记录当前最后实例化对象在策略数组的下标
    int _step = 2;                                                  // 每次下标移动的步长
    BYVideoResulte _VideoResulte;                                   // 视屏回调
    public InterAdsManage() {
        _confIndex = -1;
    }
    public void Init( List<AdsStage> _stage) {
        _ConfigureData = _stage;
        if (SDKKener.GetInstance().GetConfigByKey(ConfigKey.GoogleVideo).value > 0) {
            _interAdsV = AdsFactory.CreateInterstitialAd(Vendors.GOOGLE);
            _interAdsV.SetIndividualData(20,0, Vendors.GOOGLE.ToString() + "_InterAds",true);
        }
        SetWorkList();
    }

    /// <summary>
    /// 切换工作句柄实例和空闲句柄实例,队列转移完成进行一次集中checkads
    /// </summary>
    public void SetWorkList() {
        if (_ConfigureData.Count <= _step && _WorkList.Count > 0)               // 策略数据小于2个则不进行切换,当前梯队没有进行过播放则不进行切换
            return;
        int tempStep = _confIndex + 1;                                   // 移动下标到一下一个准备实例化的实例
        for (; tempStep < _ConfigureData.Count && tempStep - _confIndex <= _step; tempStep++) {
            var temp = AdsFactory.CreateInterstitialAd((Vendors)_ConfigureData[tempStep].vendors_id);
            Vendors type = (Vendors)_ConfigureData[tempStep].vendors_id;
            temp.SetIndividualData(_ConfigureData[tempStep].mins,
                                   _ConfigureData[tempStep].times,
                                   type.ToString() + "_InterAds",
                                   true);
            _WorkList.Add(temp);
        }

        if (_ConfigureData.Count <= _step) {
            return;
        }

        if (tempStep >= _ConfigureData.Count) {
            // 配置数据已经使用完，准备使用空闲队列的实例进行填充
            int count = _step - (tempStep - _confIndex - 1);            // 计算需要使用的空闲队列实例的个数
            _confIndex = _ConfigureData.Count - 1;
            int fixCount = Math.Min(count, _ConfigureData.Count);       // 一般的 coun < _ConfigureData.Count ,这里取两者中的最小值，以免越界
            for (int j = 0; j < fixCount && _FreedList.Count > 0; j++) {
                _WorkList.Add(_FreedList[0]);                           // 每次拿取下标0的空闲实例，并且取出后删除该实例
                _FreedList[0] = null;
                _FreedList.RemoveAt(0);
            }
        } else {
            _confIndex = tempStep - 1;
        }
        CheckAds(this, null);                                           // 工作队列准备好后需要进行一次检测
    }

    /// <summary>
    ///  如过广告没有播放过，则在需要进行检查时进行加载广告
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void CheckAds(object sender, EventArgs args) {
        if( null != _interAdsV ) {
            _interAdsV.CheckAds();
        }


        #region 检测工作队列
        if ( null != _WorkList && _WorkList.Count > 0) {
            for (int i = 0; i < _WorkList.Count; i++) {
                if (!_WorkList[i].IsReady()) {
                    _WorkList[i].CheckAds();
                }
            }
        }
        #endregion
        #region  检测二次重试队列
        if ( null != _TryList &&   _TryList.Count > 0) {
            for (int i = 0; i < _TryList.Count; i++) {
                if (!_TryList[i].IsReady()) {
                    _TryList[i].CheckAds();
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// 从预备队列里面显示插页广告
    /// </summary>
    public void displayInterstitialAds(BYVideoResulte resulte = null) {
        if (IsReadyFormTryList()) {
			DisplayFormTryList(resulte);
        } else if (IsReadyFromWorkList()) {
			DisplayFromWorkList(resulte);
        }
        if (_WorkList.Count >= 2) {
            CheckAds(this, null);
        } else {
            SetWorkList();
        }
    }

    /// <summary>
    /// 从预备队列里面显示插页广告。包含含有插页视屏的插页广告
    /// </summary>
    public void displayInterstitialAdsWithVideo(BYVideoResulte resulte = null) {
        if( IsReadyFormTryList()) {
            DisplayFormTryList(resulte);
        } else if( IsReadyFromWorkList()) {
            DisplayFromWorkList(resulte);
        } else if (null != _interAdsV && _interAdsV.IsReady()) {

            if( null != resulte ) {
                _interAdsV.AdClosed += delegate {
                    resulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Success));
                };
            }
            _interAdsV.Display();
        }

        if ( _WorkList.Count >= 2) {
            CheckAds(this,null);
        } else {
            SetWorkList();
        }
    }

    bool IsReadyFromWorkList() {
        if( null == _WorkList || _WorkList.Count <= 0) {
            return false;
        }
        int i = 0;
        for (; i < _WorkList.Count && _WorkList.Count > 0;) {
            // 查询已经准备就绪的广告，并将没有准备就绪的广告实例转移到二次尝试队列
            if (_WorkList[0].IsReady()) {
                break;
            } else {
                _WorkList[0].CheckAds();                                            // 检测一次，激发其进行广告加载
                _TryList.Add(_WorkList[0]);
                _WorkList[0] = null;
                _WorkList.RemoveAt(0);
            }
        }
        if (_WorkList.Count > 0) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 播放工作队列中已经准备就绪的广告
    /// </summary>
    void DisplayFromWorkList(BYVideoResulte resulte = null) {
        if (null == _WorkList || _WorkList.Count <= 0) {
            return;
        }

        if( null != resulte ) {
            _WorkList[0].AdClosed += delegate {
                resulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Success));
            };
        }

        _WorkList[0].Display();
        _FreedList.Add(_WorkList[0]);
        _WorkList[0] = null;
        _WorkList.RemoveAt(0);
    }
    /// <summary>
    ///  检测二次尝试队列中是否有广告下载完成，将没有下载完成的放到空闲队列
    /// </summary>
    /// <returns> true - 已经准备就绪, false - 没有准备就绪</returns>
    bool IsReadyFormTryList() {
        if (null == _TryList || _TryList.Count == 0)
            return false;
        int i = 0;
        for (; i < _TryList.Count && _TryList.Count > 0;) {
            if (_TryList[i].IsReady()) {
                break;
            } else {
                _FreedList.Add(_TryList[i]);
                _TryList[i] = null;
                _TryList.RemoveAt(0);
            }
        }
        if (_TryList.Count > 0) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 从二次尝试队列进行广告播放
    /// </summary>
    void DisplayFormTryList(BYVideoResulte resulte = null) {
        if (null == _TryList || _TryList.Count <= 0)
            return;
        if (null != resulte) {
            _TryList[0].AdClosed += delegate {
                resulte.resultCallback(new BYResulte(BYSDKManage.ActionType.Video, BYSDKManage.ActionResulet.Success));
            };
        }
        _TryList[0].Display();
        _FreedList.Add(_TryList[0]);
        _TryList[0] = null;
        _TryList.RemoveAt(0);
    }
    /// <summary>
    ///  获取已经准备就绪的广告下标
    /// </summary>
    /// <returns> -1 没有广告就绪,100 特殊插页广告就绪 </returns>
    public bool IsReady() {
        if(IsReadyFormTryList() || IsReadyFromWorkList() ||( null != _interAdsV &&  _interAdsV.IsReady())) {
            return true;
        }
        if( _WorkList.Count >= 2 ) {
            CheckAds(this,null);
        } else {
            SetWorkList();
        }
        return false;
    }
}

public class InterstitialAdsEventObserver {
    public void HandleOnAdLoaded(object sender, AdsEventArgs args) {
        BYLog.Log("[Interstitial Ad] Loaded " + args.Message);
    }

    public void HandleOnAdFailedToLoad(object sender, AdsEventArgs args) {
        BYLog.Log("[Interstitial Ad] Failed to Load" + args.Message);
    }

    public void HandleOnAdOpened(object sender, AdsEventArgs args) {
        BYLog.Log("[Interstitial Ad] Opened " + args.Message);
    }

    public void HandleOnAdClosed(object sender, AdsEventArgs args) {
        BYLog.Log("[Interstitial Ad] Closed " + args.Message);
    }
}