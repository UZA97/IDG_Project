using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
public class Rank : MonoBehaviour
{
    private DatabaseReference reference;
    private List<string> m_UserRankList;
    [SerializeField]
    private Text[] m_RankText;
    [SerializeField]
    private bool m_TextLoadBool = false;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        m_UserRankList = new List<string>();
        LoadData();
    }
    void Update()
    {
        if (m_TextLoadBool) {
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
                m_UserRankList.Clear();
                DataSnapshot snapshot = task.Result;
                int count = 0;
                foreach (DataSnapshot data in snapshot.Children) {
                    IDictionary rankInfo = (IDictionary)data.Value;
                    m_UserRankList.Add(rankInfo["best_score"].ToString());
                    m_UserRankList.Add(rankInfo["name"].ToString());
                    m_UserRankList.Add("등");
                    print(rankInfo["name"].ToString());
                    print(rankInfo["best_score"].ToString());
                    count++;
                }
                m_UserRankList.Reverse();
                m_TextLoadBool = true;
            }
        });
    }
    private void LoadText()
    {
        User user = new User();
        m_TextLoadBool = false;
        int j=1;

        for(int i = 0; i < m_UserRankList.Count; i++) {
            if(i%3==0){
                m_RankText[i].text = j + m_UserRankList[i].ToString();
                j++;
            }
            else if(i%3==1) {
                m_RankText[i].text = " " +m_UserRankList[i].ToString();
            }
            else if(i%3 ==2) {
                m_RankText[i].text = "\t"+ m_UserRankList[i].ToString();
            }
        }
    }
}
