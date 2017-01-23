using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Localization {
    public const string LANGUAGE_ENGLISH = "EN";
    public const string LANGUAGE_CHINESE = "CN";
    public const string LANGUAGE_JAPANESE = "JP";
    public const string LANGUAGE_FRENCH = "FR";
    public const string LANGUAGE_GERMAN = "GE";
    public const string LANGUAGE_ITALY = "IT";
    public const string LANGUAGE_KOREA = "KR";
    public const string LANGUAGE_RUSSIA = "RU";
    public const string LANGUAGE_SPANISH = "SP";

    private string CSVFilePath;
    private SystemLanguage language = SystemLanguage.Chinese;
    private Dictionary<string, string> textData = new Dictionary<string, string>();

    public static Localization mInstance;

    private Localization() {
        //  CSVFilePath = Application.dataPath + "/Resources/Localization/Localization.csv";
    }

    private static string GetLanguageAB(SystemLanguage language) {
        switch (language) {
        case SystemLanguage.Afrikaans:
        case SystemLanguage.Arabic:
        case SystemLanguage.Basque:
        case SystemLanguage.Belarusian:
        case SystemLanguage.Bulgarian:
        case SystemLanguage.Catalan:
            return LANGUAGE_ENGLISH;
        case SystemLanguage.Chinese:
        case SystemLanguage.ChineseTraditional:
        case SystemLanguage.ChineseSimplified:
            return LANGUAGE_CHINESE;
        case SystemLanguage.Czech:
        case SystemLanguage.Danish:
        case SystemLanguage.Dutch:
        case SystemLanguage.English:
        case SystemLanguage.Estonian:
        case SystemLanguage.Faroese:
        case SystemLanguage.Finnish:
            return LANGUAGE_ENGLISH;
        case SystemLanguage.French:
            return LANGUAGE_FRENCH;
        case SystemLanguage.German:
            return LANGUAGE_GERMAN;
        case SystemLanguage.Greek:
        case SystemLanguage.Hebrew:
        case SystemLanguage.Icelandic:
        case SystemLanguage.Indonesian:
            return LANGUAGE_ENGLISH;
        case SystemLanguage.Italian:
            return LANGUAGE_ITALY;
        case SystemLanguage.Japanese:
            return LANGUAGE_JAPANESE;
        case SystemLanguage.Korean:
            return LANGUAGE_KOREA;
        case SystemLanguage.Latvian:
        case SystemLanguage.Lithuanian:
        case SystemLanguage.Norwegian:
        case SystemLanguage.Polish:
        case SystemLanguage.Portuguese:
        case SystemLanguage.Romanian:
            return LANGUAGE_ENGLISH;
        case SystemLanguage.Russian:
            return LANGUAGE_RUSSIA;
        case SystemLanguage.SerboCroatian:
        case SystemLanguage.Slovak:
        case SystemLanguage.Slovenian:
            return LANGUAGE_ENGLISH;
        case SystemLanguage.Spanish:
            return LANGUAGE_SPANISH;
        case SystemLanguage.Swedish:
        case SystemLanguage.Thai:
        case SystemLanguage.Turkish:
        case SystemLanguage.Ukrainian:
        case SystemLanguage.Vietnamese:
        case SystemLanguage.Unknown:
            return LANGUAGE_ENGLISH;
        }
        return LANGUAGE_CHINESE;
    }

    private void ReadData() {
        TextAsset tempAsset = (TextAsset)Resources.Load("Localization/Localization", typeof(TextAsset));
        if (null == tempAsset) {
            tempAsset = (TextAsset)Resources.Load("LTLocalization/" + "EN", typeof(TextAsset));
        }
        if (null == tempAsset) {
            Debug.LogError("未检测到语言配置文件");
            return;
        }

        string csvData = tempAsset.text;

        // convert string to stream
        byte[] byteArray = Encoding.UTF8.GetBytes(csvData);
        //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
        MemoryStream stream = new MemoryStream(byteArray);

        List<string> columns = new List<string>();
        List<LocalizationData> localDataLst = new List<LocalizationData>();
        using (var reader = new CsvFileReader(stream)) {
            int rows = 0;
            while (reader.ReadRow(columns)) {
                // 第一行
                if (rows == 0) {
                    for (int i = 1; i < columns.Count; i++) {
                        LocalizationData languageData = new LocalizationData();
                        // 获取第一行数据(语言类型)
                        languageData.LanguageType = columns[i];
                        languageData.LanguageData = new Dictionary<string, string>();
                        localDataLst.Add(languageData);
                        if (GetLanguageAB(language).Equals(languageData.LanguageType)) {
                            textData = languageData.LanguageData;
                        }
                    }
                } else {
                    string key = null;
                    for (int i = 0; i < columns.Count; i++) {
                        // 第一列为key
                        if (i == 0) {
                            key = columns[i];
                        } else {
                            try {
                                localDataLst[i - 1].LanguageData.Add(key, columns[i]);
                            } catch(ArgumentException) { }
                        }
                    }
                }
                rows++;
            }
        }
    }

    /// <summary>
    /// 设置CSV文件的路径
    /// </summary>
    /// <param name="filRelativePath"></param>
    public void SetCSVFilePath(string filRelativePath) {
        CSVFilePath = Application.dataPath + "/" + filRelativePath;
    }

    private void SetLanguage(SystemLanguage language) {
        this.language = language;
    }

    public SystemLanguage GetLanguage() {
        return this.language;
    }

    public static void Init() {
        mInstance = new Localization();
        mInstance.SetLanguage(Application.systemLanguage);
        mInstance.ReadData();
    }

    public static void ManualSetLanguage(SystemLanguage setLanguage) {
        if (null == mInstance) {
            mInstance = new Localization();
        }
        mInstance.SetLanguage(setLanguage);
        mInstance.ReadData();
    }

    public static string GetText(string key) {
        if (null == mInstance) {
            Init();
        }
        if (mInstance.textData.ContainsKey(key)) {
            return mInstance.textData[key];
        }
        return "[NoDefine]" + key;
    }

}
