using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.SDKManage
*
*	describe	:	N/A
*	class name	:	VideoAdsManage
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/17 14:42:08				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
namespace BYSDKManage {
public class VideoAdsManage {

    List<AbstractVideo> _WorkList = new List<AbstractVideo>();      // 当前正在使用的实例队列
    List<AbstractVideo> _TryList = new List<AbstractVideo>();       // 二次尝试队列
    List<AbstractVideo> _FreedList = new List<AbstractVideo>();     // 已经使用过，并且空闲的队列（不参与CheckAds）
    List<AdsStage> _ConfigureData;

    int _confIndex;                                                 // 记录当前最后实例化对象在策略数组的下标
    int _step = 2;                                                  // 每次下标移动的步长
    BYVideoResulte _VideoResulte;                                   // 视屏回调
    public VideoAdsManage() {
        _confIndex = -1;
    }

    /// <summary>
    ///  初始化视屏广告管理器
    /// </summary>
    public void Init(List<AdsStage> _strge) {
        _ConfigureData = _strge;
        SetWorkList();
    }

    /// <summary>
    /// 切换工作句柄实例和空闲句柄实例
    /// </summary>
    public void SetWorkList() {
        if (null == _ConfigureData)
            return;

        if (_ConfigureData.Count <= _step && _WorkList.Count > 0)               // 策略数据小于2个则不进行切换,当前梯队没有进行过播放则不进行切换
            return;


        int tempStep = _confIndex +1;                                           // 移动下标到一下一个准备实例化的实例
        for(; tempStep < _ConfigureData.Count && tempStep - _confIndex <= _step; tempStep++) {
            var temp = AdsFactory.CreateVideoAds((Vendors)_ConfigureData[tempStep].vendors_id);
            Vendors type = (Vendors)_ConfigureData[tempStep].vendors_id;
            temp.SetIndividualData(_ConfigureData[tempStep].mins,
                                   _ConfigureData[tempStep].times,
                                   type.ToString()+"_VideoAds",
                                   true);
            _WorkList.Add(temp);
        }

        if (_ConfigureData.Count <= _step) {
            return;
        }

        if ( tempStep >= _ConfigureData.Count) {                         // 配置数据已经使用完，准备使用空闲队列的实例进行填充
            int count = _step - (tempStep - _confIndex - 1);             // 计算需要使用的空闲队列实例的个数
            _confIndex = _ConfigureData.Count - 1;
            int fixCount = Math.Min(count, _ConfigureData.Count);        // 一般的 coun < _ConfigureData.Count ,这里取两者中的最小值，以免越界
            for( int j = 0; j< fixCount && _FreedList.Count > 0; j++) {
                _WorkList.Add(_FreedList[0]);                           // 每次拿取下标0的空闲实例，并且取出后删除该实例
                _FreedList[0] = null;
                _FreedList.RemoveAt(0);
            }
        } else {
            _confIndex = tempStep - 1;
        }
        CheckAds(this, null);                                           // 工作队列准备好后需要进行一次检测
    }

    public void Display(Loading.LodingActionHandler loadhandle = null, BYVideoResulte resulte = null) {
        try {
            if( null == _WorkList || _WorkList.Count <= 0) {
                if (loadhandle != null) {
                    loadhandle(ActionType.Video, ActionResulet.NoAds);
                }
                if (null != resulte) {
                    resulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.NoAds));
                }
                SetWorkList();
                return;
            }

            if( IsReadyForTryList()) {
                DisplayFromTryList(loadhandle, resulte);
            } else if( IsReadyForWorkList()) {
                DisplayFromWorkList(loadhandle, resulte);
            }
            if (_WorkList.Count >= 2) {
                CheckAds(this, null);
            } else {
                SetWorkList();
            }
        } catch (Exception e ) {
            throw e;
        }
    }

    /// <summary>
    ///  检测工作队列中是否已经有广告加载完毕，如果某个广告没有加载完毕则直接放到二次重试队列
    /// </summary>
    /// <returns> true - 已经有广告准备就绪，fals - 没有广告准备就绪 （需要重新设置工作队列）</returns>
    bool IsReadyForWorkList() {
        if (null == _WorkList || _WorkList.Count <= 0) {
            return false;
        }

        int i = 0;
        for (; i < _WorkList.Count && _WorkList.Count > 0;) {   // 查询已经准备就绪的广告，并将没有准备就绪的广告实例转移到二次尝试队列
            if (_WorkList[i].IsReady()) {
                break;
            } else {
                _WorkList[i].CheckAds();                                            // 检测一次，激发其进行广告加载
                _TryList.Add(_WorkList[i]);
                _WorkList[i] = null;
                _WorkList.RemoveAt(i);
            }
        }


        if (_WorkList.Count > 0) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 检查工作队列是否准备就绪，只做检查不做加载
    /// </summary>
    /// <returns></returns>
    bool IsWorkListReady() {
        if (null == _WorkList || _WorkList.Count <= 0) {
            return false;
        }
        int i = 0;
        for (; i < _WorkList.Count && _WorkList.Count > 0;) {
            if (_WorkList[i].IsReady()) {
                break;
            } else {
                i++;
            }
        }
        if (i >= _WorkList.Count)
            return false;
        else
            return true;
    }
    /// <summary>
    ///  播放第一个已经准备就绪的广告，对于没有准备就绪的暂时保存在队列中，等待下次重试
    ///  播放成功直接放到空闲队列中
    /// </summary>
    /// <param name="loadhandle"></param>
    /// <param name="resulte"></param>
    void DisplayFromWorkList(Loading.LodingActionHandler loadhandle = null, BYVideoResulte resulte = null) {
        try {
            if( null == _WorkList || _WorkList.Count <= 0 ) {
                if (loadhandle != null) {
                    loadhandle(ActionType.Video, ActionResulet.NoAds);
                }
                if (null != resulte) {
                    resulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.NoAds));
                }

                return;
            }

            _WorkList[0].OnVoidStart +=delegate {
                if (loadhandle != null) {
                    loadhandle(ActionType.Video, ActionResulet.Success);
                }
            };
            _WorkList[0].Display(resulte);
            _FreedList.Add(_WorkList[0]);
            _WorkList[0] = null;
            _WorkList.RemoveAt(0);
        } catch(Exception e) {
            throw new Exception("Video DisplayFromWorkList failed! " + e.Message);
        }

    }
    /// <summary>
    ///  检测二次尝试队列中是否有广告下载完成，将没有下载完成的放到空闲队列
    /// </summary>
    /// <returns> true - 已经准备就绪, false - 没有准备就绪</returns>
    bool  IsReadyForTryList() {
        if (null == _TryList || _TryList.Count == 0)
            return false;
        int i = 0;
        for(; i < _TryList.Count && _TryList.Count > 0;) {
            if( _TryList[i].IsReady()) {
                break;
            } else {
                _FreedList.Add(_TryList[i]);
                _TryList[i] = null;
                _TryList.RemoveAt(0);
            }
        }

        if( _TryList.Count > 0 ) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 检测二次重试队列是否准备就绪，仅仅只做检查不做加载
    /// </summary>
    /// <returns></returns>
    bool IsTryListReady() {
        if (null == _TryList || _TryList.Count == 0)
            return false;
        int i = 0;
        for (; i < _TryList.Count && _TryList.Count > 0;) {
            if (_TryList[i].IsReady()) {
                break;
            } else {
                i++;
            }
        }
        if (i >= _TryList.Count)
            return false;
        else
            return true;
    }
    /// <summary>
    ///  播放二次尝试队列中的广告，需要在 IsReadyForTryList 之后调用。
    /// </summary>
    void DisplayFromTryList(Loading.LodingActionHandler loadhandle = null, BYVideoResulte resulte = null) {
        try {
            if( null == _TryList || _TryList.Count <= 0) {
                if (loadhandle != null) {
                    loadhandle(ActionType.Video, ActionResulet.NoAds);
                    loadhandle = null;
                }
                if (null != resulte) {
                    resulte.resultCallback(new BYResulte(ActionType.Video, ActionResulet.NoAds));
                }

                return;
            }
            _TryList[0].OnVoidStart += delegate {
                if (loadhandle != null) {
                    loadhandle(ActionType.Video, ActionResulet.Success);
                }
                loadhandle = null;
            };
            _TryList[0].Display(resulte);
            _FreedList.Add(_TryList[0]);
            _TryList.RemoveAt(0);
        } catch(Exception e) {
            throw new Exception("Video DisplayFromTryList error: " + e.Message);
        }
    }

    /// <summary>
    ///  如过广告没有播放过，则在需要进行检查时进行加载广告
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void CheckAds(object sender,EventArgs args) {
        int readyCount = 0;     // 准备好的个数 2
        int loadCount = 0;      // 下载的个数 2
        #region 检测工作队列中的广告实例
        for (int i = 0; i < _WorkList.Count; i++) {
            if (readyCount >= 2 || loadCount >= 2)
                return;
            if (!_WorkList[i].IsReady()) {
                _WorkList[i].CheckAds();     // 正在下载的个数
                ++loadCount;

            } else {
                ++readyCount;
            }
        }
        #endregion

        #region 检测二次准备队列的广告实例
        for (int j = 0; j < _TryList.Count; j++) {
            if (readyCount >= 2 || loadCount >= 2)
                return;
            if (!_TryList[j].IsReady()) {
                _TryList[j].CheckAds();
                ++loadCount;
            } else {
                ++readyCount;
            }
        }
        #endregion

        #region
        for (int f = 0; f < _FreedList.Count; f++) {
            if (readyCount >= 2 || loadCount >= 2)
                return;
            if (!_FreedList[f].IsReady()) {
                _FreedList[f].CheckAds();
                ++loadCount;
            } else {
                ++readyCount;
            }
        }
        #endregion
    }

    /// <summary>
    ///  判断是否已经有广告准备就绪
    /// </summary>
    /// <returns> false 没有广告就绪 </returns>
    public bool IsReady() {
        var ret =  IsReadyForTryList() || IsReadyForWorkList();
        if( !ret ) {
            if (_WorkList.Count >= 2) {
                CheckAds(this, null);
            } else {
                SetWorkList();
            }
        }
        return ret;
    }

    /// <summary>
    /// 检测是否有广告准备就绪，不挪动广告所在队列
    /// </summary>
    /// <returns></returns>
    public bool IsReadyForCheck() {
        bool ret = IsWorkListReady() || IsTryListReady();
        if( !ret ) {
            if (_WorkList.Count >= 2) {
                CheckAds(this, null);
            } else {
                SetWorkList();
            }
        }
        return ret;
    }
}
}


public class VideoAdsEventObserver {
    public void HandleOnVideoLoaded(object sender, AdsEventArgs args) {
        BYLog.Log("[Video ads ] loaded " + args.Message);
    }

    public void HandleOnVideoCancel(object sender, AdsEventArgs args) {
        BYLog.Log("[Video ads ] Cancel " + args.Message);
    }

    public void HandleOnVideoClose(object sender, AdsEventArgs args) {
        BYLog.Log("[Video ads ] Show  " + args.Message);
    }

    public void HandleOnVideFailed(object sender, AdsEventArgs args) {
        BYLog.Log("[Video ads ] Failed " + args.Message);
    }
}