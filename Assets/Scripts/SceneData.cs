using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    public static SceneData _instance;
    public string username;
    public float S_BGMVolume;
    public float S_BtnClickVolume;
    private void Awake()
    {
        Application.targetFrameRate = 60;   // 프레임   60고정
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        S_BGMVolume = PlayerPrefs.GetFloat("S_BGM");
        S_BtnClickVolume = PlayerPrefs.GetFloat("S_BtnClick");
        
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
                SceneManager.LoadScene("Login");
                Destroy(SceneData._instance.gameObject);
            }
            else
                return;
        }
        
    }
}
