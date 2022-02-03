using UnityEngine;
using UnityEngine.UI;
using static LanguageSingleton;

public class LocalizeScript : MonoBehaviour
{
    public string textKey;
    void Start()
    {
        LocalizeChanged();
        S.LocalizeChanged += LocalizeChanged;
    }

    void OnDestroy()
    {
        S.LocalizeChanged -= LocalizeChanged;
    }

    string Localize(string key)
    {
        int keyIndex = S.Langs[0].value.FindIndex(x => x.ToLower() == key.ToLower());
        return S.Langs[S.curLangIndex].value[keyIndex];
    }

    void LocalizeChanged()
    {
        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = Localize(textKey);
    }
}