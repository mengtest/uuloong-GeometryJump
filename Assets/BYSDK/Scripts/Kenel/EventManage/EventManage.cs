using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.Common.AdsDataControl
*
*	describe	:	N/A
*	class name	:	NodeConfig
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/25 16:17:25				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/


public class EventManage {
    Hashtable _eventHandleList;
    List<SDKEventArgs> eventList;
    //Thread              _thread;
    public EventManage() {
        _eventHandleList = new Hashtable();
        eventList = new List<SDKEventArgs>();
        //_thread = new Thread(new ThreadStart(BroadCastEvent));
        //_thread.Start();
    }
    /// <summary>
    /// 注册感兴趣的事件
    /// </summary>
    /// <param name="order">事件注册者</param>
    /// <param name="type">事件类型</param>
    /// <param name="handle">事件回调句柄</param>
    /// <param name="once">是否一次性触发，触发后立即销毁</param>
    /// <returns>false - 注册失败 ，true-注册成功</returns>
    public bool RegistEventHadle(object order,SDKEvent type, Action<SDKEventArgs> handle,bool once = true) {
        var _obj = new EventObject();
        _obj.order = order;
        _obj.handler = handle;
        _obj.RegistOnce = once;
        if ( _eventHandleList.Count <= 0 || !_eventHandleList.ContainsKey(type.ToString())) {
            List<EventObject> _list = new List<EventObject>();
            _list.Add(_obj);
            _eventHandleList.Add(type.ToString(), _list);
            return true;
        } else {
            var _list = (List<EventObject>)_eventHandleList[type.ToString()];
            bool handRegist = false;
            for(int i = 0; i < _list.Count; i++ ) {
                if( _list[i].order == order ) {
                    handRegist = true;
                    break;
                }
            }

            if( !handRegist) {
                _list.Add(_obj);
            } else {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 撤销事件注册句柄
    /// </summary>
    /// <param name="order"> 事件注册者 </param>
    /// <param name="type"> 关注的事件类型 </param>
    public void  UnRegistEventHandle(object order,SDKEvent type) {
        if (_eventHandleList.Count <= 0 || !_eventHandleList.ContainsKey(type.ToString())) {
            return ;
        } else {
            var _list = (List<EventObject>)_eventHandleList[type.ToString()];
            int pos = -1;
            for (int i = 0; i < _list.Count; i++) {
                if (_list[i].order == order) {
                    pos = i;
                    break;
                }
            }
            if( pos  != -1) {
                _list.RemoveAt(pos);
            }
        }
    }


    void  BroadCastEvent() {

    }
    /// <summary>
    ///  事件通知.将事件发送给注册者
    /// </summary>
    /// <param name="resulte">事件结果</param>
    public void EventNotice(SDKEventArgs resulte) {
        if( _eventHandleList.Count > 0 && _eventHandleList.ContainsKey(resulte.type.ToString())) {
            var _list = (List<EventObject>)_eventHandleList[resulte.type.ToString()];
            for( int i = 0; i < _list.Count; i++ ) {
                try {
                    _list[i].handler(resulte);
                } catch(Exception e) {
                    Debug.Log(" error " + e.Message);
                    _list[i] = null;
                    continue;
                }

            }

            //// 通知完成后撤销已经废弃的句柄
            //for (int i = 0; _list.Count > 0 && i < _list.Count;) {
            //    if (_list[i] == null || _list[i].RegistOnce) {
            //        _list.RemoveAt(i);
            //    } else {
            //        i++;
            //    }
            //}
        }
    }

}


