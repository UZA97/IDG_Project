using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
public class Rank : MonoBehaviour
{
    private DatabaseReference reference;
    private List<string> m_UserRankList = new List<string>();
    [SerializeField]
    private Text[] m_RankText;
    [SerializeField]
    private Text m_UserRankText;
    private bool m_TextLoadBool = false;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        LoadData();
        m_UserRankList = new List<string>();
    }
    void Update()
    {
        UserRankInfo();
        if (m_TextLoadBool)
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
                m_UserRankList.Clear();
                DataSnapshot snapshot = task.Result;
                int count = 0;
                foreach (DataSnapshot data in snapshot.Children) {
                    IDictionary rankInfo = (IDictionary)data.Value;
                    m_UserRankList.Add(rankInfo["name"].ToString() + " : " + rankInfo["best_score"].ToString());
                    count++;
                }
                m_UserRankList.Reverse();
                m_TextLoadBool = true;
            }
        });
    }
    void LoadText()
    {
        m_TextLoadBool = false;
        for(int i = 0; i<m_UserRankList.Count; i++) {
            m_RankText[i].text = (i+1) +"등 "+ m_UserRankList[i];
        }
    }
    public void UserRankInfo()
    {
        m_UserRankText.text = "현재 나의 최고 점수\n" + SceneData._instance.username + " : " + PlayerPrefs.GetInt("MaxScore").ToString();
    }
}
