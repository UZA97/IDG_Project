using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField]
    private InputField Input_UserName;
    [SerializeField]
    private GameObject RankGroup;
    [SerializeField]
    private GameObject OptionGroup;
    private string userName;

    private void Start()
    {
        PlayerPrefs.DeleteKey("UserName");
        if(!PlayerPrefs.HasKey("UserName")) {
            print("AS");    // "최초 아이디를 입력해 주세요" 토스트메시지 작성 
        }
        else {
            userName = PlayerPrefs.GetString("UserName");
            SceneData._instance.username = userName;
            if(SceneManager.GetActiveScene().name == "InputName"){
                SceneManager.LoadScene("Login");
                Destroy(SceneData._instance.gameObject);
            }
            else
                return;
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Main");
    }
    public void Restart()
    {
        SceneManager.LoadScene("Main");
        GameManager._instance.IsOver = false;
    }

    public void InputUserName()
    {
        DBManager._instance.IsVaildName(Input_UserName.text);
        if(Input_UserName.text == "") {
            Debug.Log("같은 이름이 있거나 비어있어요!!");   // 토스트 메시지
            return;
        }
        else if(DBManager._instance.mIsVaildName){
            PlayerPrefs.SetString("UserName", Input_UserName.text);
            SceneData._instance.username = PlayerPrefs.GetString("UserName");
            SceneManager.LoadScene("Login");
        }
    }
    public void ShowOption()
    {
        GameManager._instance.IsPause = true;
        GameManager._instance.ChickGroup.SetActive(false);
        OptionGroup.SetActive(true);
    }
    public void GameToMain()
    {
        SceneManager.LoadScene("Login");
        Destroy(SceneData._instance.gameObject);
    }
    public void Exit()
    {
        GameManager._instance.IsPause = false;
        GameManager._instance.ChickGroup.SetActive(true);
        OptionGroup.SetActive(false);
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void ShowRank()
    {
        RankGroup.SetActive(true);
    }
    public void RanktoMain()
    {
        RankGroup.SetActive(false);
    }
}
