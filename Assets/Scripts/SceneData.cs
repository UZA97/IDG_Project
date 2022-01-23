using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    public static SceneData _instance;
    public AudioSource S_BGM;
    public string username;
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
        PlayerPrefs.DeleteKey("UserName");
        if(!PlayerPrefs.HasKey("UserName")) {
            print("AS");    // "최초 아이디를 입력해 주세요" 토스트메시지 작성 
        }
        else {
            username = PlayerPrefs.GetString("UserName");
            SceneData._instance.username = username;
            if(SceneManager.GetActiveScene().name == "InputName"){
                SceneManager.LoadScene("Main");
                Destroy(SceneData._instance.gameObject);
            }
            else
                return;
        }
    }
}
