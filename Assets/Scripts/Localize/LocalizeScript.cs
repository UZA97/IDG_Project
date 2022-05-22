using UnityEngine;
using UnityEngine.UI;
using static LanguageSingleton;

public class LocalizeScript : MonoBehaviour
{
    public string textKey;
    void Start()
    {
        LocalizeChanged();
        languageSingleton.LocalizeChanged += LocalizeChanged;
    }

    void OnDestroy()
    {
        languageSingleton.LocalizeChanged -= LocalizeChanged;
    }

    string Localize(string key)
    {
        int keyIndex = languageSingleton.Langs[0].value.FindIndex(x => x.ToLower() == key.ToLower());
        return languageSingleton.Langs[languageSingleton.curLangIndex].value[keyIndex];
    }

    void LocalizeChanged()
    {
        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = Localize(textKey);
    }
}