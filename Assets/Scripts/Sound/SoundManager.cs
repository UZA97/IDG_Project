using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    public AudioSource S_BtnClick;
    [SerializeField]
    private AudioSource S_BGM;
    [SerializeField]
    private Slider BGMslider;
    public Slider EffectSoundslider;
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
