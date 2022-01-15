using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Firebase.Database;
public class Rank : MonoBehaviour
{
    DatabaseReference reference;
    List<User> userList = new List<User>();
    [SerializeField]
    private Text[] rankText;
    [SerializeField]
    private Text userRankText;
    private string[] strRank;
    private string[] rankNum;
    private int[] ranknum;
    private long strLen;
    private bool textLoadBool = false;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        LoadData();
    }
    void Update()
    {
        UserRankInfo();
        if (textLoadBool)
        {
            LoadText();
        }   
    }
    
    void LoadData()
    {
        User user = new User();
        reference.Child("users").OrderByChild("best_score").LimitToLast(100).GetValueAsync().ContinueWith(task =>
        {            
            if(task.IsFaulted) {
                LoadData();
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                strLen = snapshot.ChildrenCount;
                rankNum = new string[strLen];
                strRank = new string[strLen];
                int count = 0;
                foreach (DataSnapshot data in snapshot.Children) {
                    IDictionary rankInfo = (IDictionary)data.Value;
                    strRank[count] = rankInfo["name"].ToString() + " : " + string.Format("{0:}", rankInfo["best_score"]);
                    count++;
                }
                //LateUpdate의 TextLoad() 함수 실행
                Array.Reverse(strRank);
                textLoadBool = true;
            }
        });
    }
    void LoadText()
    {
        textLoadBool = false;
        for (int i = 0; i < rankText.Length; i++) {
            if (strLen <= i) {
                return;      
            }
            rankText[i].text = strRank[i];
        }
        for(int i = 0; i<5;i++){
            ranknum[i] = int.Parse(rankNum[i]);
            print(ranknum[i]);
        }
    }
    public void UserRankInfo()
    {
        userRankText.text = "현재 나의 최고 점수\n" + SceneData._instance.username + " : " + PlayerPrefs.GetInt("MaxScore").ToString();
    }

}
