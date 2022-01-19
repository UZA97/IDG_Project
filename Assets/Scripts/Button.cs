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
    [Header("Audio")]
    [SerializeField]
    private AudioSource S_BtnClick;

    public void GameStart()
    {
        S_BtnClick.Play();
        SceneManager.LoadScene("Main");
    }
    public void Restart()
    {
        S_BtnClick.Play();
        SceneManager.LoadScene("Main");
        GameManager._instance.IsOver = false;
    }

    public void InputUserName()
    {
        S_BtnClick.Play();
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
        S_BtnClick.Play();
        GameManager._instance.IsPause = true;
        GameManager._instance.ChickGroup.SetActive(false);
        OptionGroup.SetActive(true);
    }
    public void GameToMain()
    {
        S_BtnClick.Play();
        SceneManager.LoadScene("Login");
        Destroy(SceneData._instance.gameObject);
    }
    public void Exit()
    {
        S_BtnClick.Play();
        GameManager._instance.IsPause = false;
        GameManager._instance.ChickGroup.SetActive(true);
        OptionGroup.SetActive(false);
    }
    public void EndGame()
    {
        S_BtnClick.Play();
        Application.Quit();
    }

    public void ShowRank()
    {
        RankGroup.SetActive(true);
        S_BtnClick.Play();
    }
    public void RanktoMain()
    {
        RankGroup.SetActive(false);
        S_BtnClick.Play();
    }
}
