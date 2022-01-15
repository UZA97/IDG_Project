using System;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{   
    public static User _instance;
    private string user_name;
    private int user_score;
    private int user_rank;
    private int user_bestScore;
    public DateTime user_createAt;
    public DateTime user_updateTime;
    public string GetUserName() {return user_name;}
    public int GetUserScore() {return user_score;}
    public int GetUserRank() {return user_rank;}
    public int GetUserBestScore() {return user_bestScore;}
    public void SetUserName(string name) {this.user_name = name;}
    public void SetUserScore(int score) {this.user_score = score;}
    public void SetUserRank(int rank){this.user_rank = rank;}
    public void SetUserBestScore(int bestScore) {this.user_bestScore = bestScore;}
    public void SetUserCreateAt(DateTime createAt) {this.user_createAt = createAt;}
    public void SetUserUpdateTime(DateTime updateTime) {this.user_updateTime = updateTime;}    
    private void Awake()
    {
        _instance = this;
    }
    public User()
    {
        this.user_name = "";
        this.user_score = 0;
        this.user_rank = 0;
        this.user_bestScore = 0;
        this.user_createAt = DateTime.Now;
        this.user_updateTime = DateTime.Now;
    }
    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["name"] = this.user_name;
        dic["score"] = this.user_score;
        dic["rank"] = this.user_rank;
        dic["best_score"] = this.user_bestScore;
        dic["create_time"] = Convert.ToString(this.user_createAt);
        dic["update_time"] = Convert.ToString(this.user_updateTime);
        return dic;
    }
}
