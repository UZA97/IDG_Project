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
    private void LoadText()
    {
        User user = new User();
        m_TextLoadBool = false;
        string[] arr = new string[]{"1등 ", "2등 " , "3등 "};
        for(int i = 3; i<m_UserRankList.Count; i++) {
            //m_RankText[i].text = (i+1) +"등 "+ m_UserRankList[i];
            for(int j = 0; j< 3; j++){
                m_RankText[j].text = arr[j]+ m_UserRankList[j];
            }
            m_RankText[i].text = m_UserRankList[i];
        }
    }

    public void UserRankInfo()
    {
        m_UserRankText.text = "현재 나의 최고 점수\n" + SceneData._instance.username + " : " + PlayerPrefs.GetInt("MaxScore").ToString();
    }
}
