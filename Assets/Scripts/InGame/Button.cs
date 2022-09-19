using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using GoogleMobileAds.Api;
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
        SceneManager.LoadScene("3.Game");
    }
    public void Restart()
    {
        SceneManager.LoadScene("3.Game");
        GameManager._instance.isOver = false;
    }

    public void InputUserName()
    {
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

    public void ShowObject(GameObject _obj)
	{
        _obj.SetActive(true);
	}
    public void OffObject(GameObject _obj)
    {
        _obj.SetActive(false);

    }

    public void InGameShowOption()
    {
        GameManager._instance.isPause = true;
        GameManager._instance.ChickGroup.SetActive(false);
        OptionGroup.SetActive(true);
    }
    public void GameToMain()
    {
        SceneManager.LoadScene("2.Main");
    }
    public void InGameExitOption()
    {
        GameManager._instance.isPause = false;
        GameManager._instance.ChickGroup.SetActive(true);
        OptionGroup.SetActive(false);
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void PushBtn()
    {
        if(!GameManager._instance.isPause) {
            switch(chickType)
            {
                case ChickType.A:
                CheckChick(ChickType.A);
                break;
                case ChickType.B:
                CheckChick(ChickType.B);
                break;
                case ChickType.C:
                CheckChick(ChickType.C);
                break;
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
                SoundManager._instance.ChickSound.Play();
                GameManager._instance.DestroyChick();
            }
            else
            {
                SoundManager._instance.failSound.Play();
                GameManager._instance.Wrong();
            }
        }
    }

    //#region 리워드 광고
    //const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    //const string rewardID = "";
    //RewardedAd rewardAd;


    //void LoadRewardAd()
    //{
    //    rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
    //    rewardAd.LoadAd(GetAdRequest());
    //    rewardAd.OnUserEarnedReward += (sender, e) =>
    //    {
    //        LogText.text = "리워드 광고 성공";
    //    };
    //}

    //public void ShowRewardAd()
    //{
    //    rewardAd.Show();
    //    LoadRewardAd();
    //}
    //#endregion
    //AdRequest GetAdRequest()
    //{
    //    return new AdRequest.Builder().AddTestDevice("1DF7B7CC05014E8").Build();
    //}

}
