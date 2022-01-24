using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    public static SceneData _instance;
    public AudioSource S_BGM;

    private void Awake()
    {
        Application.targetFrameRate = 60;   // 프레임   60고정
        if(_instance == null) {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name=="Game") {
            S_BGM.enabled = false;
        }
        else if(SceneManager.GetActiveScene().name !="Game"){
            S_BGM.volume = PlayerPrefs.GetFloat("S_BGM");
            S_BGM.enabled = true;
        }
    }
    private void Start()
    {
        //PlayerPrefs.DeleteKey("UserName");
        if(!PlayerPrefs.HasKey("UserName")) {
            return;
        }
        else {
            if(SceneManager.GetActiveScene().name == "InputName"){
                SceneManager.LoadScene("Main");
            }
        }
    }
}
