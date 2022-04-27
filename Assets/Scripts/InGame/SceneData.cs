using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    public static SceneData _instance;
    public AudioSource S_BGM;
    public AudioSource S_BtnClick;
    public GameObject NetworkPanel;
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;
        if(_instance == null) {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
        if(PlayerPrefs.HasKey("UserName")) {
            SceneManager.LoadScene("2.Main");
        }
    }
    void Update()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable) {
            NetworkPanel.SetActive(true);
        }
        else
            NetworkPanel.SetActive(false);   

        if(SceneManager.GetActiveScene().name=="3.Game") {
            S_BGM.enabled = false;
        }
        else if(SceneManager.GetActiveScene().name !="3.Game"){
            if(!PlayerPrefs.HasKey("S_BGM") &&!PlayerPrefs.HasKey("S_BtnClick")) {
                PlayerPrefs.SetFloat("S_BGM",1.0f);
                PlayerPrefs.SetFloat("S_BtnClick",1.0f);
            }
            else {
                S_BGM.volume = PlayerPrefs.GetFloat("S_BGM");
                S_BGM.enabled = true;
            }
        }
    }
}
