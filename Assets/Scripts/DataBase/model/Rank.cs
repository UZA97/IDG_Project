using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
public class Rank : MonoBehaviour
{
    private DatabaseReference reference;
    private List<string> UserRankList;
    [SerializeField]
    private Text[] RankText;
    [SerializeField]
    private bool TextLoadBool = false;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        UserRankList = new List<string>();
        LoadData();
    }
    void Update()
    {
        if (TextLoadBool) {
            LoadText();
        }   
    }
    
    void LoadData()
    {
        reference.Child("users").OrderByChild("best_score").LimitToLast(50).GetValueAsync().ContinueWith(task =>
        {            
            if(task.IsFaulted) {
                LoadData();
            }
            else if (task.IsCompleted) {
                UserRankList.Clear();
                DataSnapshot snapshot = task.Result;
                int count = 0;
                foreach (DataSnapshot data in snapshot.Children) {
                    IDictionary rankInfo = (IDictionary)data.Value;
                    Debug.Log("Name: " + rankInfo["name"] + " / Score: " + rankInfo["score"]);
                    UserRankList.Add(rankInfo["best_score"].ToString());
                    UserRankList.Add(rankInfo["name"].ToString());
                    UserRankList.Add("등");
                    count++;
                }
                UserRankList.Reverse();
                TextLoadBool = true;
            }
        });
    }
    private void LoadText()
    {
        User user = new User();
        TextLoadBool = false;
        int j=1;

        for(int i = 0; i < UserRankList.Count; i++) {
            if(i%3==0){
                RankText[i].text = j + UserRankList[i].ToString();
                j++;
            }
            else if(i%3==1) {
                RankText[i].text = UserRankList[i].ToString();
            }
            else if(i%3 ==2) {
                RankText[i].text = "\t"+ UserRankList[i].ToString();
            }
        }
    }
}
