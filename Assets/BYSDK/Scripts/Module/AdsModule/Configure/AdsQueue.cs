using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AdsQueue  {

    List<AdsData>   _adsIndexList;


    ///<example> "type,wight;type1,wight1"
    public AdsQueue() {
        _adsIndexList = new List<AdsData>();
    }

    public void Add(AdsData _t) {

        if( null == _adsIndexList )
            _adsIndexList = new List<AdsData>();
        bool  _mutexIndex = false;
        for( int j = 0 ; j < _adsIndexList.Count ; j++ ) {
            if( _adsIndexList[j]._index == _t._index ) {
                _mutexIndex = true;
            }
        }

        if( _mutexIndex )
            return;
        var temp = (AdsData)_t.Clone();
        _adsIndexList.Add(temp);
        //_adsIndexList.Sort();
    }

    public void Mprint() {
        for( int i = 0 ; i < _adsIndexList.Count ; i++ ) {
            BYLog.Log("************* index " + _adsIndexList[i]._index + " *********** w " + _adsIndexList[i]._wight);
        }
    }
    public AdsData Pop() {
        AdsData ret ;
        if( _adsIndexList.Count > 0 ) {
            ret = _adsIndexList[0];
            _adsIndexList.RemoveAt(0);
        } else {
            ret = null;
        }
        return ret;
    }

    public int Length() {
        return _adsIndexList.Count;
    }

    public void Clear() {
        _adsIndexList.Clear();
    }

    public void RemoveAt(int index) {
        _adsIndexList.RemoveAt(index);
    }

    public void Sort() {
        _adsIndexList.Sort();
    }
}
