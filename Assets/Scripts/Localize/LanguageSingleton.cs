using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Lang
{
    public string lang,langLocalize;
    public List<string> value = new List<string>();
}

public class LanguageSingleton : MonoBehaviour 
{
    public static LanguageSingleton instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        else Destroy(this);

        InitLang();
    }

    const string langURL = "https://docs.google.com/spreadsheets/d/1wZrrZq1fzJIp73C9LsNluy3IIlt26Gi8dBmvIs4Sqo8/export?format=tsv"; //언어 데이터가 담겨진 구글 스프레드 시트의 링크
    public event System.Action LocalizeChanged = () => {};  
    public event System.Action LocalizeSettingChanged = () => {};

    //언어가 바뀌는 것을 알려주기 위해 Action 선언.

    public int curLangIndex;
    public List<Lang> Langs;


    void InitLang()
    {
        int langIndex = PlayerPrefs.GetInt("LangIndex", -1);
        int systemIndex = Langs.FindIndex(x => x.lang.ToLower() == Application.systemLanguage.ToString().ToLower());
        if (systemIndex == -1) systemIndex = 0;
        int index = langIndex == -1 ? systemIndex : langIndex;

        SetLangIndex(index);
    }


    public void SetLangIndex(int index)
    {
        curLangIndex = index;   //initlang에서 구한 언어의 인덱스 값을 curLangIndex에 넣어줌 
        PlayerPrefs.SetInt("LangIndex", curLangIndex);  //저장
        LocalizeChanged();  //텍스트들 현재 언어로 변경
        LocalizeSettingChanged();   //드랍다운의 value변경
    }


    [ContextMenu("언어 가져오기")]    //ContextMenu로 게임중이 아닐 때에도 실행 가능 
    void GetLang()
    {
        StartCoroutine(GetLangCo());
    }

    IEnumerator GetLangCo()
    {
        UnityWebRequest www = UnityWebRequest.Get(langURL); //스프레드 시트의 url을 가져오고
        yield return www.SendWebRequest();  //가져올 때 까지 대기 
        SetLangList(www.downloadHandler.text);  //스프레드 시트의 데이터 값을 SetLangList에 넣어준다.
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