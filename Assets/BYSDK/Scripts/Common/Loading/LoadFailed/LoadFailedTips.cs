using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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


public class LoadFailedTips : MonoBehaviour {
    static readonly string use_zn = "使用";
    static readonly string use_en = "use";

    static readonly string to = "to";
    public GameObject UseText;
    public GameObject AdText;
    public GameObject ToText;

    public GameObject CommontImage;
    public GameObject ShareImage;

    bool IsChineseLanguage = false;
    void OnEnable() {
        IsChineseLanguage = SDKUtiles.GetLocationByLanguae();
        Init();
    }

    void Init() {
        if( IsChineseLanguage) {
            UseText.GetComponent<Text>().text = use_zn;
            AdText.GetComponent<Text>().text = "和";
            ToText.SetActive(false);
        } else {
            UseText.GetComponent<Text>().text = use_en;
            ToText.GetComponent<Text>().text = to;
        }
    }

    public void SetData(LoadFailedArgs args) {
        switch ( args) {
        case LoadFailedArgs.OnlyCommon: {
            ShareImage.SetActive(false);
            AdText.SetActive(false);
        }
        break;
        case LoadFailedArgs.OnlyShare: {
            CommontImage.SetActive(false);
            AdText.SetActive(false);
        }
        break;
        case LoadFailedArgs.None:
            gameObject.SetActive(false);
            break;
        }
    }


}
