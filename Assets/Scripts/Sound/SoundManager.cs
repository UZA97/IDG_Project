using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    [Header ("BGM")]
    [SerializeField]
    private AudioSource BGM;
    public AudioSource inGameBGM;
    public AudioSource feverBGM;

    [Header ("Sound")]
    public AudioSource btnClick;
    public AudioSource ChickSound;
    public AudioSource feverSound;
    public AudioSource failSound;
    public AudioSource gameOverSound;
    [SerializeField]
    private Slider BGMSlider;
    public Slider effectSoundSlider;
    [SerializeField]
    private Image BGMImage;
    [SerializeField]
    private Image effectImage;
    [SerializeField]
    private Image BGMXImage;
    [SerializeField]
    private Image effectXImage;
    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        //DontDestroyOnLoad(gameObject);
        if (effectSoundSlider != null)
            effectSoundSlider.value = PlayerPrefs.GetFloat("S_BtnClick");

        if(BGMSlider != null)
            BGMSlider.value = PlayerPrefs.GetFloat("S_BGM");

    }

	private void Update()
	{
        if (SceneManager.GetActiveScene().name != "3.Game")
        {
            if (!PlayerPrefs.HasKey("S_BGM") && !PlayerPrefs.HasKey("S_BtnClick"))
            {
                PlayerPrefs.SetFloat("S_BGM", 1.0f);
                PlayerPrefs.SetFloat("S_BtnClick", 1.0f);
            }
            else
            {
                BGM.volume = PlayerPrefs.GetFloat("S_BGM");
            }
        }
    }

	public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("S_BGM",volume);
        BGM.volume = volume;
        feverBGM.volume = volume;
        inGameBGM.volume = volume;
        if(BGMSlider.value == 0) {
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
        btnClick.volume = volume;
        ChickSound.volume = volume;
        failSound.volume = volume;
        feverSound.volume = volume;
        gameOverSound.volume = volume;
        if(effectSoundSlider.value == 0) {
            effectImage.enabled = false;
            effectXImage.enabled = true;
        }
        else {
            effectImage.enabled = true;
            effectXImage.enabled = false;
        }
    }
}
