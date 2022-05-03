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
        DontDestroyOnLoad(gameObject);
        EffectSoundslider.value = PlayerPrefs.GetFloat("S_BtnClick");
        S_BtnClick.volume = EffectSoundslider.value;
        BGMslider.value = PlayerPrefs.GetFloat("S_BGM");
        S_BGM.volume = BGMslider.value;
    }

	private void Update()
	{
        if (SceneManager.GetActiveScene().name == "3.Game")
        {
            S_BGM.enabled = false;
        }
        else if (SceneManager.GetActiveScene().name != "3.Game")
        {
            if (!PlayerPrefs.HasKey("S_BGM") && !PlayerPrefs.HasKey("S_BtnClick"))
            {
                PlayerPrefs.SetFloat("S_BGM", 1.0f);
                PlayerPrefs.SetFloat("S_BtnClick", 1.0f);
            }
            else
            {
                S_BGM.volume = PlayerPrefs.GetFloat("S_BGM");
                S_BGM.enabled = true;
            }
        }
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
