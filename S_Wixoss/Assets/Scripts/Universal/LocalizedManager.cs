using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

/// <summary>本地化</summary>
public class LocalizedManager
{
    #region properties

    /// <summary>当前语言</summary>
    static Language currentLanguage;

    /// <summary>本地化语言包</summary>
    static Dictionary<Language, Dictionary<string, string>> languagePack;

    #endregion

    #region Public Method

    // 改为静态构造函数， 系统自动调用
    static LocalizedManager()
    {
        // 从配置文件加载
        currentLanguage = Language.Chinese;
        languagePack = new Dictionary<Language, Dictionary<string, string>>();
        foreach (Language language in Enum.GetValues(typeof(Language)))
        {
            LoadLanguagePack(language);
        }
    }

    /// <summary>
    /// 获取语言本地化结果
    /// </summary>
    /// <param name="key">本地化key</param>
    /// <returns>本地化结果</returns>
    public static string Localizer(string key)
    {
        // 没翻译过的直接返回原key
        if (!languagePack[currentLanguage].ContainsKey(key))
        {
            return key;
        }

        return languagePack[currentLanguage][key];
    }

    /// <summary>
    /// 获取当前使用的语言
    /// </summary>
    /// <returns>当前使用的语言</returns>
    public static Language GetLanguage()
    {
        return currentLanguage;
    }

    #endregion

    #region Private Method

    /// <summary>
    /// 加载本地化语言资源
    /// </summary>
    private static void LoadLanguagePack(Language language)
    {
        // 从Resources/Languages下读取语言资源
        var languageContent = Resources.Load<TextAsset>($"TextData/Languages/{language}");
        var languageMap = new Dictionary<string, string>();
        Debug.Log($"Load Language: {language}");
        // 解析语言资源并放入languageMap中
        var separator = new[] { " " };
        foreach (var translate in languageContent.text.Split('\n'))
        {
            if (string.IsNullOrEmpty(translate))
            {
                continue;
            }

            try
            {
                var sourceAndTranslatedText = translate.Split(separator, StringSplitOptions.None);
                languageMap[sourceAndTranslatedText[0]] = sourceAndTranslatedText[1];
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error: Language: {language}, message: {ex.Message}");
            }
        }
        Debug.Log($"{language}: key-value count: {languageMap.Count}");
        // 最后赋值
        languagePack[language] = languageMap;
    }

    #endregion
}