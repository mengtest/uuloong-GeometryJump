using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.BYSDK.Scripts.AdsModule.Configure.AdsDefault
*
*	describe	:	N/A
*	class name	:	AdsContest
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/27 15:59:34				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public enum Vendors {
    GOOGLE = 1,
    GOOGLEP = 2,        /* special google interstitial ads without video */
    CHARTBOOST = 3,
    VUNGLE = 4,
    APPLOVIN = 5,
    UNITY = 6,
    TENCENT = 7,
    BAIDU = 8,
    ADBUDDIZ = 9,
    ADCOLONY = 10,
    FACEBOOK = 11,
    INMOBI = 12
}

public enum AdsShowStratage {
    CHINA = 1,                  // 只在中国显示
    ABROAD = 2,                 // 只在国外显示
    ALL = 3                     // 不区分国度
}