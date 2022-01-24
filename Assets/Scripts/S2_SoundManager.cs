using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S2_SoundManager : MonoBehaviour
{
    public static S2_SoundManager _instance;
    public AudioSource S_BtnClick;
    public AudioSource S_BGM;
    public AudioSource S_Chick;
    public AudioSource S_FeverBGM;
    public AudioSource S_Fever;
    public AudioSource S_Fail;
    public AudioSource S_GameOver;
    public Slider EffectSoundslider;
    public Slider BGMslider;
    [SerializeField]
    private Image BGMImage;
    [SerializeField]
    private Image EffectImage;
    [SerializeField]
    private Image BGMXImage;
    [SerializeField]
    private Image EffectXImage;
    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
        }
        EffectSoundslider.value = PlayerPrefs.GetFloat("S_BtnClick");
        S_BtnClick.volume = EffectSoundslider.value;
        BGMslider.value = PlayerPrefs.GetFloat("S_BGM");
        S_BGM.volume = BGMslider.value;
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("S_BGM",volume);
        S_BGM.volume = volume;
        S_FeverBGM.volume = volume;
        if(BGMslider.value == 0) {
            BGMImage.enabled = false;
            BGMXImage.enabled = true;
        }
        else {
            BGMImage.enabled = true;
            BGMXImage.enabled = false;
        }
    }
    public void SetEffectVolume(float volume)
    {
        PlayerPrefs.SetFloat("S_BtnClick",volume);
        S_BtnClick.volume = volume;
        S_Chick.volume = volume;
        S_Fail.volume = volume;
        S_Fever.volume = volume;
        S_GameOver.volume = volume;
        if(EffectSoundslider.value == 0) {
            EffectImage.enabled = false;
            EffectXImage.enabled = true;
        }
        else {
            EffectImage.enabled = true;
            EffectXImage.enabled = false;
        }
    }
}
