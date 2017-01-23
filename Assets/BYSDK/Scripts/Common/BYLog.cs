#define BYSDK_DEBUG
#define BYSDK_INFO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*********************************************************************//**
*	namespace	:	Assets.Scripts.BYSDK.Common
*
*	describe	:	N/A
*	class name	:	BYLog
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/8/5 15:05:55				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

class BYLog {

    public static void  Log(object message) {
#if BYSDK_DEBUG
        Debug.Log( "[BYSDK - C# ]  | " + System.DateTime.Now.Date.ToShortDateString() + " | Mesage >>>>> " + message);
#endif
    }

    public static void InfoLog(object message) {
#if BYSDK_INFO
        Debug.Log( "[BYSDK - C# ]  | " + System.DateTime.Now.Date.ToShortDateString() + " | Mesage >>>>> " + message);
#endif
    }
}
