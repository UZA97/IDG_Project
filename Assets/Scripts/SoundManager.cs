using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    public AudioSource S_BtnClick;
    public AudioSource S_BGM;
    public Slider EffectSoundslider;
    public Slider BGMslider;
    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        //else
            //Destroy(gameObject);
        if(SceneManager.GetActiveScene().name != "InputName") {
            EffectSoundslider.value = PlayerPrefs.GetFloat("S_BtnClick");
            S_BtnClick.volume = EffectSoundslider.value;
            BGMslider.value = PlayerPrefs.GetFloat("S_BGM");
            S_BGM.volume = BGMslider.value;
        }
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("S_BGM",volume);
        S_BGM.volume = volume;
        //SceneData._instance.S_BGMVolume = S_BGM.volume;
    }
    public void SetEffectVolume(float volume)
    {
        PlayerPrefs.SetFloat("S_BtnClick",volume);
        S_BtnClick.volume = volume;
        //SceneData._instance.S_BtnClickVolume = S_BtnClick.volume;
    }
}
