using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class LanguageText : MonoBehaviour
{
    // 默认的英文字体
    private static Font DefaultEnFont = null;
    // 是否是中文
    private static bool isChinese = false;
    // text内容
    public string Text;

    void Awake()
    {
        if (DefaultEnFont == null)
        {
            DefaultEnFont = Resources.Load<Font>("Fonts/HelveticaNeue LT 55 Roman");
            if (Application.systemLanguage == SystemLanguage.Chinese ||
                Application.systemLanguage == SystemLanguage.ChineseSimplified ||
                Application.systemLanguage == SystemLanguage.ChineseTraditional)
            {
                isChinese = true;
            }
            else
            {
                isChinese = false;
            }
        }
    }

    void Start()
    {
        Text textGo = GetComponent<Text>();
        if (!string.IsNullOrEmpty(Text)) {
            textGo.text = Localization.GetText(Text);
        }
        if (!isChinese) {
            textGo.font = DefaultEnFont;
        }

        ////检测到中文语言，适应减小字号（游戏主页和设置页全部用黑体，英文状态下44px，中文状态下-4px；）
        //SystemLanguage language = Localization.mInstance.GetLanguage ();
        //if (language == SystemLanguage.Chinese) {
        //    GetComponent<Text> ().fontSize -= 4;
        //}
    }

}
