using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts
*
*	describe	:	N/A
*	class name	:	AdsStage
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/8 0:44:54				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

// 广告策略实体  该数据存入数据库中
public class AdsStage {
    /// <summary>
    /// 广告厂商标示
    /// </summary>
    public int vendors_id {
        get;
        set;
    }

    /// <summary>
    /// 广告厂商排序
    /// </summary>
    public int z_index {
        get;
        set;
    }

    /// <summary>
    /// 权重
    /// </summary>
    public int weight {
        get;
        set;
    }

    /// <summary>
    /// 显示间隔或者切换时间
    /// </summary>
    public long mins {
        get;
        set;
    }

    /// <summary>
    /// 每天限制显示次数
    /// </summary>
    public int times {
        get;
        set;
    }
}



public class StrategyModel {
    public List<AdsStage> Banner {
        get;
        set;
    }

    public List<AdsStage> Interstitial {
        get;
        set;
    }

    public List<AdsStage> Video {
        get;
        set;
    }

    public bool CanUse() {
        return Banner.Count > 0 && Interstitial.Count > 0 && Video.Count > 0;
    }
}

public class Ads_Strategy {
    public int Id {
        get;
        set;
    }

    public string Strategy {
        get;
        set;
    }

    public bool Save() {
        return   DBOperation.GetInstance().Save<Ads_Strategy>(this);
    }
}
