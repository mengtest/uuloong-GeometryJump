using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*********************************************************************//**
*	namespace	:	Assets.Scripts
*
*	describe	:	N/A
*	class name	:	GameNodeMoudle
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/8 16:17:00				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/



///游戏节点配置  该数据存入数据库中
///

public class GameNodeData {
    public int Id {
        get;
        set;
    }

    /// <summary>
    /// 节点数据
    /// </summary>
    public string value {
        get;
        set;
    }

    public void Save() {
        DBOperation.GetInstance().Save<GameNodeData>(this);
    }
}
public class GameNode {
    /// <summary>
    /// 节点id
    /// </summary>
    public int node_id {
        get;
        set;
    }

    /// <summary>
    /// 游戏节点名称
    /// </summary>
    public string node_name {
        get;
        set;
    }

    /// <summary>
    /// 附加数值节点id串: eg. 1,2,3
    /// </summary>
    public string attached_id {
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
    /// 广告类型
    /// </summary>
    public int adstype {
        get;
        set;
    }

    /// <summary>
    /// 作为附加节点时的权重
    /// </summary>
    public int attached_weight {
        get;
        set;
    }
}
