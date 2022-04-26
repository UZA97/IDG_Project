using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Lang
{
    public string lang, langLocalize;
    public List<string> value = new List<string>();
}

public class LanguageSingleton : MonoBehaviour
{
    const string langURL = "https://docs.google.com/spreadsheets/d/1wZrrZq1fzJIp73C9LsNluy3IIlt26Gi8dBmvIs4Sqo8/export?format=tsv";
    public event System.Action LocalizeChanged = () => { };
    public event System.Action LocalizeSettingChanged = () => { };
    public int curLangIndex;
    public List<Lang> Langs;

    public static LanguageSingleton S;
    void Awake()
    {
        if (null == S)
        {
            S = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
        InitLang();
    }

    void InitLang()
    {
        int langIndex = PlayerPrefs.GetInt("LangIndex", -1);
        int systemIndex = Langs.FindIndex(x => x.lang.ToLower() == Application.systemLanguage.ToString().ToLower());
        if (systemIndex == -1)
            systemIndex = 0;
        //int index = langIndex == -1 ? systemIndex : langIndex;
        if (langIndex == -1)
            langIndex = systemIndex;;
		SetLangIndex(langIndex);
	}

	public void SetLangIndex(int index)
    {
        curLangIndex = index;
        PlayerPrefs.SetInt("LangIndex", curLangIndex);
        LocalizeChanged();
        LocalizeSettingChanged();
    }

    [ContextMenu("언어 가져오기")]
    void GetLang()
    {
        StartCoroutine(GetLangCo());
    }

    IEnumerator GetLangCo()
    {
        UnityWebRequest www = UnityWebRequest.Get(langURL);
        yield return www.SendWebRequest();
        SetLangList(www.downloadHandler.text);
    }

    void SetLangList(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;
        string[,] Sentence = new string[rowSize, columnSize];

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnSize; j++) 
                Sentence[i, j] = column[j];
        }

        Langs = new List<Lang>();
        for (int i = 0; i < columnSize; i++)
        {
            Lang lang = new Lang();
            lang.lang = Sentence[0, i];
            lang.langLocalize = Sentence[1, i];

            for (int j = 2; j < rowSize; j++) lang.value.Add(Sentence[j, i]);
                Langs.Add(lang);
        }
    }
}