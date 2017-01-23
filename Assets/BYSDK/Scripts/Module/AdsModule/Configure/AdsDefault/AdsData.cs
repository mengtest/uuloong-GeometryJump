using UnityEngine;
using System.Collections;
using System;

/// <summary>
///  广告策略数据
/// </summary>
public class AdsData : ICloneable, IComparable {

    public AdsData(int indexT, int wigthT) {
        _index = indexT;
        _wight = wigthT;

    }

    public AdsData(string infoStr) {
        string[] info = infoStr.Split(',');
        _index = int.Parse(info[0]);
        _wight = int.Parse(info[1]);
    }

    public AdsData() {

    }
    /// <summary>
    ///  广告实例在列表中的位置
    /// </summary>
    public int _index {
        get;
        set;
    }

    /// <summary>
    ///  广告权重
    /// </summary>
    public int _wight {
        get;
        set;
    }

    /// <summary>
    /// 广告厂商
    /// </summary>
    public Vendors _type {
        get;
        set;
    }

    /// <summary>
    /// 是否在中国显示
    /// </summary>
    public AdsShowStratage _ShowStratage {
        get;
        set;
    }

    /// <summary>
    ///  广告循环显示的时间阀值，针对Banner.对于其他广告便是冷却时间窗
    /// </summary>
    public long _mins {
        get;
        set;
    }

    /// <summary>
    /// 广告显示的起始时间
    /// </summary>
    public long _startTime {
        get;
        set;
    }
    /// <summary>
    ///  广告循环显示的是次数限制
    /// </summary>
    public int _times {
        get;
        set;
    }

    /// <summary>
    ///  广告显示的次数
    /// </summary>
    public int _ShowTimes {
        get;
        set;
    }

    public int _UsefullCount {
        get;
        set;
    }
    public int CompareTo(object obj) {
        try {
            AdsData info = obj as AdsData;
            if (this._wight == info._wight) {
                return 0;
            } else if (this._wight < info._wight) {
                return 1;
            } else {
                return -1;
            }
        } catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public object Clone() {
        var ret             = new AdsData();
        ret._index          = this._index;
        ret._mins           = this._mins;
        ret._ShowStratage   = this._ShowStratage;
        ret._ShowTimes      = this._ShowTimes;
        ret._startTime      = this._startTime;
        ret._times          = this._times;
        ret._type           = this._type;
        ret._wight          = this._wight;


        return ret as object;
    }
}
