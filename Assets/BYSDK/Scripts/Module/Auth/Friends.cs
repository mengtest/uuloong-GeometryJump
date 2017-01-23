using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pathfinding.Serialization.JsonFx;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.Auth
*
*	describe	:	N/A
*	class name	:	Friends
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/24 15:19:49				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
public class Friends {

    MonoBehaviour order;

    public Friends(MonoBehaviour _order) {
        order = _order;
        RegistEvent();
    }

    void RegistEvent() {
        SDKKener.GetInstance().RegistEventHandler(this, SDKEvent.PUSGMSG_FRIEND, PushMessageToFirend);
    }
    #region 向好友推送消息
    /// <summary>
    /// 向好友发送邀请 - 推送消息
    /// </summary>
    void PushMessageToFirend(SDKEventArgs args) {
        if (args.type == SDKEvent.PUSGMSG_FRIEND && args.status == EventStatus.ALLOW) {
            FriendMSG_Struct data = (FriendMSG_Struct)args.data;
            HttpOperation.PostByHttp<string>(order, SDKNetUtls.PushMessageFriend, JsonWriter.Serialize(data), OnMsgToFriedSuccess, OnMsgToFriedFailed);
        }
    }

    void OnMsgToFriedSuccess(string msg) {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.PUSGMSG_FRIEND, EventStatus.SUCCESS, msg));
    }

    void OnMsgToFriedFailed(string msg) {
        SDKKener.GetInstance().EventNotict(new SDKEventArgs(SDKEvent.PUSGMSG_FRIEND, EventStatus.SUCCESS, msg));
    }

    #endregion
}
