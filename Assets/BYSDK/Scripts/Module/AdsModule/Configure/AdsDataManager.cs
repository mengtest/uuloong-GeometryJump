using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pathfinding.Serialization.JsonFx;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.Configure
*
*	describe	:	N/A
*	class name	:	AdsDataManager
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/9 18:09:02				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
public   class AdsDataManager {

    static  string name = "AdsDataManager";
    List<AdsModel> _list;
    MonoBehaviour order;
    public static string Name() {
        return name;
    }

    public AdsDataManager(MonoBehaviour _order) {
        order = _order;
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.ADS_CHECK, OnEvent);
    }

    void OnEvent(SDKEventArgs resulte) {
        Debug.Log(" AdsDatamanager " + resulte.type.ToString() + " status " + resulte.status.ToString());
        if (resulte.type == SDKEvent.ADS_CHECK && resulte.status == EventStatus.COMPLETE) {
            CheckData();
        }
    }

    /// <summary>
    /// 检测是否需要上报收集到的数据
    /// </summary>
    public void CheckData() {
        var count = DBOperation.GetInstance().GetCount<AdsModel>();
        // var stage = SDKKener.GetInstance().GetConfigByKey(ConfigKey.AdsDataManager);
        if (count >= 50) {
            SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.ADS_DATA_REPORT, EventStatus.START));
            ReportData();
        } else {
            SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.ADS_DATA_REPORT, EventStatus.COMPLETE));
        }
        //} else if( null != stage && count > 0 && count > stage.value ) {

        //}
        //if ( count >= SDKKener.GetInstance().GetConfigByKey(ConfigKey.AdsDataManager).date) {
        //    SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.ADS_DATA_REPORT, EventStatus.START));
        //    ReportData();
        //} else {
        //    SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.ADS_DATA_REPORT,EventStatus.COMPLETE));
        //}
    }

    /// <summary>
    /// 开始上报数据
    /// </summary>
    void ReportData() {
        _list =   DBOperation.GetInstance().FindAll<AdsModel>();
        List<string> dataList = new List<string>();
        for( int i = 0; i < _list.Count; i++ ) {
            dataList.Add(_list[i].data);
        }
        HttpOperation.PostByHttp<string>(order, SDKNetUtls.PostSeriesData(SDKKener.gameinfo.name),dataList, OnSuccess, OnFailed);

    }


    /// <summary>
    /// 返回获取失败列表
    /// </summary>
    /// <param name="msg"></param>
    void OnSuccess(string msg) {
        var ret = JsonReader.Deserialize<List<int>>(msg);
        DeleteData(ret);
    }

    void  OnFailed(string msg) {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.ADS_DATA_REPORT, EventStatus.FAILED));
    }

    /// <summary>
    /// 删除上报处理成功功部分
    /// </summary>
    /// <param name="arr"></param>
    void  DeleteData(List<int> arr) {
        for( int i = 0; arr != null && i < arr.Count; i++ ) {
            var t =  DBOperation.GetInstance().Delete<AdsModel>(_list[arr[i]].Id);
            Debug.Log(" delete ret: " + t + " ID " + _list[arr[i]].Id);
        }
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.ADS_DATA_REPORT, EventStatus.COMPLETE));
    }


    /// <summary>
    /// 保存广告数据
    /// </summary>
    /// <param name="data"></param>
    public static void SaveAdsData(AdsDBData data) {
        AdsDBData m_data = (AdsDBData)data;
        AdsModel model = new AdsModel();
        var t = DBOperation.GetInstance().NewID<AdsModel>();
        if (t == null) {
            model.Id = 1;
        } else {
            model.Id = t.Id + 1;
        }
        model.data = JsonWriter.Serialize(m_data);
        Debug.Log("=========> " + model.data);
        DBOperation.GetInstance().Save<AdsModel>(model);
    }
}
