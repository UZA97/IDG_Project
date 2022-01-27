﻿using System.Collections;
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
    private bool m_TextLoadBool = false;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        LoadData();
        m_UserRankList = new List<string>();
    }
    void Update()
    {
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
                    m_UserRankList.Add(rankInfo["best_score"].ToString());
                    m_UserRankList.Add(rankInfo["name"].ToString());
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
        int j=0;
        for(int i = 0; i < m_UserRankList.Count; i++) {
            if(i%2==0){
                m_RankText[i].text = (i+1+j) +"등     "+m_UserRankList[i].ToString();
                j--;
            }
            else if(i%2==1)
                 m_RankText[i].text = m_UserRankList[i].ToString();
        }
    }
}
