#region 程序集 BYSDKManage, Version=1.0.0.0,
/*********************************************************************//**
*	namespace	:	Assets.Scripts.SDKManage
*
*	describe	:	N/A
*	class name	:	ActionResulet
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/6/12 17:06:22				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BYSDKManage {
public enum ActionResulet {
    Success = 1,            // 操作成功
    Failed = 2,             // SDK在播放时出错 如,广告检测错误导致播放失败，广告显示时出错导致失败
    Skipe = 3,              // 跳过操作
    Error = 4,              // 播放广告时出现错误错误
    Close = 5,
    NoAds = 6               // 没有广告
}
}
