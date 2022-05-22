using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LanguageSingleton;

public class LocalizeSetting : MonoBehaviour
{
    Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        if (dropdown.options.Count != languageSingleton.Langs.Count) SetLangOption();
        dropdown.onValueChanged.AddListener((d) => languageSingleton.SetLangIndex(dropdown.value));

        LocalizeSettingChanged();
        languageSingleton.LocalizeSettingChanged += LocalizeSettingChanged;
    }

	void OnDestroy()
	{
        languageSingleton.LocalizeSettingChanged -= LocalizeSettingChanged;
    }

	void SetLangOption()
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < languageSingleton.Langs.Count; i++)
            optionDatas.Add(new Dropdown.OptionData() { text = languageSingleton.Langs[i].langLocalize });
        dropdown.options = optionDatas;
    }

    void LocalizeSettingChanged()
    {
        dropdown.value = languageSingleton.curLangIndex;
    }
}