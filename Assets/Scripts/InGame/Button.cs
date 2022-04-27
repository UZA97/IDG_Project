using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum ChickType{A,B,C}
public class Button : MonoBehaviour
{
    [SerializeField]
    private InputField Input_UserName;
    [SerializeField]
    private GameObject RankGroup;
    [SerializeField]
    private GameObject OptionGroup;
    public ChickType chickType;
    public Text Notificationtext;
    public bool nameCount = false;

    public void GameStart()
    {
        SoundManager._instance.S_BtnClick.Play();
        SceneManager.LoadScene("3.Game");
    }
    public void Restart()
    {
        S2_SoundManager._instance.S_BtnClick.Play();
        SceneManager.LoadScene("3.Game");
        GameManager._instance.isOver = false;
    }

    public void InputUserName()
    {
        SoundManager._instance.S_BtnClick.Play();

        if(Input_UserName.text == "" || !nameCount) {
            Notificationtext.text = "이름이 비어있거나\n이름이 옳바르지 않아요!!";
            // Notificationtext.text = "There are\ncharacters unavailable.";
            return;
        }
        else if(!DBManager._instance.isVaildName)
        {
            Notificationtext.text = "이미 같은 이름이 있어요!!";
            // Notificationtext.text = "ID Account already exists,\nTry again.";
            return;
        }
        else if(DBManager._instance.isVaildName && nameCount){
            PlayerPrefs.SetString("UserName", Input_UserName.text);
            User user= new User();
            user.SetUserName(PlayerPrefs.GetString("UserName"));
            DBManager._instance.CreateUser();
            SceneManager.LoadScene("2.Main");
        }
    }
    public void CheckUserName(string name)
    {
        string idChecker =name;
        idChecker = Regex.Replace(idChecker,@"[^0-9a-zA-Z가-힣]","");
        if(name.Length <2 || name !=idChecker){
            nameCount = false;
        }
        else if(name.Length>=2 || name == idChecker){
            nameCount = true;
            DBManager._instance.IsVaildName(name);
        }
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
        GameManager._instance.isPause = true;
        GameManager._instance.ChickGroup.SetActive(false);
        OptionGroup.SetActive(true);
    }
    public void GameToMain()
    {
        S2_SoundManager._instance.S_BtnClick.Play();
        SceneManager.LoadScene("2.Main");
        //SceneData._instance.S_BGM.Play();
    }
    public void InGameExitOption()
    {
        S2_SoundManager._instance.S_BtnClick.Play();
        GameManager._instance.isPause = false;
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
    public void PushBtn()
    {
        if(!GameManager._instance.isPause) {
            switch(chickType)
            {
                case ChickType.A:
                //CheckChick("A");
                CheckChick(ChickType.A);
                break;
                case ChickType.B:
                //CheckChick("B");
                CheckChick(ChickType.B);
                break;
                case ChickType.C:
                //CheckChick("C");
                CheckChick(ChickType.C);
                break;
            }
        }
    }
    private void CheckChick(string chickname)
    {
        if (GameManager._instance.chick == null)
            return;
        if (!GameManager._instance.isPause)
        {
            if (GameManager._instance.chick.chickName == chickname) {
                S2_SoundManager._instance.S_Chick.Play();
                GameManager._instance.DestroyChick();
            }
            else {
                S2_SoundManager._instance.S_Fail.Play();
                GameManager._instance.Wrong();
            }
        }
    }
    private void CheckChick(ChickType chickType)
    {
        if (GameManager._instance.chick == null)
            return;
        if (!GameManager._instance.isPause)
        {
            if (GameManager._instance.chick.chickType == chickType)
            {
                S2_SoundManager._instance.S_Chick.Play();
                GameManager._instance.DestroyChick();
            }
            else
            {
                S2_SoundManager._instance.S_Fail.Play();
                GameManager._instance.Wrong();
            }
        }
    }
}
