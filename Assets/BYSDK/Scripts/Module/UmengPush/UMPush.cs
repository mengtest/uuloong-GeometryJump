using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.UmengPush
*
*	describe	:	N/A
*	class name	:	UMPush
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/30 9:45:45				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public   class UMPush {
    public UMPush() {
        init();
    }

    void init() {
        BYLog.Log("UM init");
#if  UNITY_IPHONE
        UMPushiOS.setChannel("App Store");
#endif
    }
}

