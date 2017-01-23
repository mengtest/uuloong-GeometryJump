using UnityEngine;
using System.Collections;

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

/// 广告事件记录
public class AdsModel  {
    /// <summary>
    /// 主键
    /// </summary>
    public int Id {
        get;
        set;
    }

    /// <summary>
    /// 数据部分
    /// </summary>
    public string data {
        get;
        set;
    }
}

public class AdsDBData {
    /// <summary>
    /// 广告商类型
    /// </summary>
    public int vendors {
        get;
        set;
    }
    /// <summary>
    /// 广告类型
    /// </summary>
    public int adstype {
        get;
        set;
    }

    /// <summary>
    /// 显示次数
    /// </summary>
    public int if_display {
        get;
        set;
    }

    /// <summary>
    /// 是否点击
    /// </summary>
    public int if_clicked {
        get;
        set;
    }

    /// <summary>
    /// 广告播放时长
    /// </summary>
    public long duration {
        get;
        set;
    }

    /// <summary>
    /// 是否下载了广告中推广的信息
    /// </summary>
    public int if_download {
        get;
        set;
    }

    /// <summary>
    /// 是否发生了错误
    /// </summary>
    public int if_failed {
        get;
        set;
    }

    /// <summary>
    ///  附加信息，例如广告播放失败的简要信息
    /// </summary>
    public string info {
        get;
        set;
    }

    public long timestamp {
        get;
        set;
    }
}