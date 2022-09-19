using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GoogleAdmob : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public bool isTestMode;

    void Start()
    {
        //var requestConfiguration = new RequestConfiguration
        //   .Builder()
        //   .SetTestDeviceIds(new List<string>() { "7ED7B2BD585584C8" }) // test Device ID
        //   .build();

        //MobileAds.SetRequestConfiguration(requestConfiguration);
        //AdRequest request = new AdRequest.Builder().Build();
        //LoadBannerAd();
        //LoadFrontAd();

        LoadRewardAd();
        RewardedAd
    }


    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }



    #region 배너 광고
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "";
    BannerView bannerAd;


    void LoadBannerAd()
    {
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID,
            AdSize.SmartBanner, AdPosition.Bottom);
        bannerAd.LoadAd(GetAdRequest());
        ToggleBannerAd(false);
    }

    public void ToggleBannerAd(bool b)
    {
        if (b) bannerAd.Show();
        else bannerAd.Hide();
    }
    #endregion

    #region 전면 광고
    const string frontTestID = "ca-app-pub-3940256099942544/8691691433";
    const string frontID = "";
    InterstitialAd frontAd;


    void LoadFrontAd()
    {
        frontAd = new InterstitialAd(isTestMode ? frontTestID : frontID);
        frontAd.LoadAd(GetAdRequest());
        frontAd.OnAdClosed += (sender, e) =>
        {
        };
    }

    public void ShowFrontAd()
    {
        frontAd.Show();
        LoadFrontAd();
    }
    #endregion

    #region 리워드 광고
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-2029579933182508/4371569551";
    RewardedAd rewardAd;


    void LoadRewardAd()
    {
        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        AdRequest request = new AdRequest.Builder().Build();
        rewardAd.LoadAd(request);
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            Debug.Log("리워드 광고 성공");
        };
    }

    public void ShowRewardAd()
    {
        rewardAd.Show();
        LoadRewardAd();
    }
    #endregion
}