using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BYSDKManage;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.Ads.Video
*
*	describe	:	N/A
*	class name	:	BYVideoResulte
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/7/31 11:00:34				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
public class BYVideoResulte {
    public Action<BYResulte> resultCallback {
        get;
        set;
    }
}

public class BYResulte {
    public BYResulte(ActionType t,ActionResulet r) {
        _type = t;
        _resulte = r;
    }
    public ActionType _type;
    public ActionResulet _resulte;
}