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
    [SerializeField]
    private Text Notificationtext;

    public void GameStart()
    {
        SoundManager._instance.S_BtnClick.Play();
        SceneManager.LoadScene("Game");
    }
    public void Restart()
    {
        S2_SoundManager._instance.S_BtnClick.Play();
        SceneManager.LoadScene("Game");
        GameManager._instance.IsOver = false;
    }

    public void InputUserName()
    {
        SoundManager._instance.S_BtnClick.Play();
        if(Input_UserName.text == "") {
            Notificationtext.text = "이름이 비어있어요!!";
            return;
        }
        if(!DBManager._instance.mIsVaildName){
            Notificationtext.text = "같은 이름이 있어요!!";
            return;
        }
        else if(DBManager._instance.mIsVaildName){
            PlayerPrefs.SetString("UserName", Input_UserName.text);
            SceneData._instance.username = PlayerPrefs.GetString("UserName");
            SceneManager.LoadScene("Main");
        }
    }
    public void CheckUserName(string name)
    {
        DBManager._instance.IsVaildName(name);
    }
    public void ShowOption()
    {
        SoundManager._instance.S_BtnClick.Play();
        OptionGroup.SetActive(true);
    }
    public void ExitOption()
    {
        SoundManager._instance.S_BtnClick.Play();
        OptionGroup.SetActive(false);
    }
    public void InGameShowOption()
    {
        S2_SoundManager._instance.S_BtnClick.Play();
        GameManager._instance.IsPause = true;
        GameManager._instance.ChickGroup.SetActive(false);
        OptionGroup.SetActive(true);
    }
    public void GameToMain()
    {
        S2_SoundManager._instance.S_BtnClick.Play();
        SceneManager.LoadScene("Main");
        SceneData._instance.S_BGM.Play();
    }
    public void InGameExitOption()
    {
        S2_SoundManager._instance.S_BtnClick.Play();
        GameManager._instance.IsPause = false;
        GameManager._instance.ChickGroup.SetActive(true);
        OptionGroup.SetActive(false);
    }
    public void EndGame()
    {
        SoundManager._instance.S_BtnClick.Play();
        Application.Quit();
    }

    public void ShowRank()
    {
        RankGroup.SetActive(true);
        SoundManager._instance.S_BtnClick.Play();
    }
    public void RanktoMain()
    {
        RankGroup.SetActive(false);
        SoundManager._instance.S_BtnClick.Play();
    }
}
