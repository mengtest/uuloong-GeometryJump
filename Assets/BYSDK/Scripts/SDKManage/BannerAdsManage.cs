using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.SDKManage
*
*	describe	:	N/A
*	class name	:	BannerAdsManage
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/17 12:01:07				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
public    class BannerAdsManage {

    /// <summary>
    /// 当前使用的Banner广告实例
    /// </summary>
    BannerAds _bannerAds = null;

    /// <summary>
    /// 临时广告 -- 实现广告的无缝对接
    /// </summary>
    BannerAds _tempAds = null;

    List<AdsStage> _adsData;
    int _index;
    bool _isChina = false;
    public BannerAdsManage(bool isChina,List<AdsStage> _stage) {
        _isChina = isChina;
        _adsData = _stage;
        init();

    }

    void init() {
        try {
            if (_adsData.Count <= 0) {
                throw new Exception("BannerAdsManage: _adsData is null,please check");
            }
            BYLog.Log("banner ads manage init");
            _index = 0;
            ChangeAds();
        } catch(Exception e) {
            BYLog.Log(e.Message);
        }

    }

    /// <summary>
    /// 进行广告展示时长检测
    /// </summary>
    public void CheckAds() {
        if (_adsData.Count <= 1 || _bannerAds == null) {
            BYLog.Log("Banner 广告列表为 1 或 者banner还没有初始化，不需要进行轮训播放 ,Banner Size: " + _adsData.Count);

            return;
        }
        BYLog.Log("Banner 进行轮训播放 ,Banner Size: " + _adsData.Count);

        if(_bannerAds.IndividualData.IsTimeWindOpen() ) {
            ++_index;
            ChangeAds();
        }
    }

    /// <summary>
    ///  更换广告SDK
    /// </summary>
    void ChangeAds( ) {

        if ( _adsData.Count <= 0 ) {
            BYLog.Log("BannerManage data count is 0. Please check");
            return;
        }
        if (_index >= _adsData.Count) {
            _index = 0;
        }
        BYLog.Log("开始切换Banner 切换下标 " + _index + "mins " + _adsData[_index].mins);
        _tempAds = AdsFactory.CreateBannerAds((Vendors)_adsData[_index].vendors_id);
        _tempAds.SetIndividualData(_adsData[_index].mins,_adsData[_index].times);
        if( _tempAds == null ) {
            ++_index;
            ChangeAds();
        }

        if( _bannerAds == null ) {              // 第一次加载时直接切换
            BYLog.Log("第一次切换Banner");
            _bannerAds = _tempAds;
            _bannerAds.LoadAndDisplay();
            _tempAds = null;
        } else {
            _tempAds.LoadAndDisplay();
            _tempAds.AdLoaded += OnAdsLod;
        }
    }

    /// <summary>
    ///  banner 广告在加载到之后，进行切换从而实现无缝对接.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void OnAdsLod(object sender,AdsEventArgs args) {
        if( _tempAds != null ) {
            if( _bannerAds != null) {
                _bannerAds.Hide();
                _bannerAds.Destroy();
            }
            _tempAds.AdLoaded -= OnAdsLod;
            _bannerAds = _tempAds;
            _tempAds = null;
        }
    }

    /// <summary>
    ///  即使广告加载失败也需要进行实例切换
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void OnAdsLoadFailed(object sender,AdsEventArgs args) {
        BYLog.Log("BannerManage OnAdsLoadFailed ");
        if (_tempAds != null) {
            if (_bannerAds != null) {
                _bannerAds.Hide();
                _bannerAds.Destroy();
            }
            _tempAds.AdLoaded -= OnAdsLod;
            _bannerAds = _tempAds;
            _tempAds = null;
        }
    }
}
