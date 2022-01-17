using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class DBManager : MonoBehaviour
{
    public FirebaseAuth mAuth;
    public DatabaseReference mRef;
    private Firebase.Auth.FirebaseUser mUser;
    public static DBManager _instance;
    public bool mIsVaildName = false;
    private string[] mRank;
    private string mDatabaseUrl = "https://squid-9654f-default-rtdb.firebaseio.com/";
    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
        }
        mAuth = FirebaseAuth.DefaultInstance;
        mRef = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(mDatabaseUrl);
    }
    private void Start()
    {
        Login();
    }
    private void Login()
    {
        mAuth.SignInAnonymouslyAsync().ContinueWith(
            task => { 
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled) {
                    mUser = task.Result;
                }
                else if(!task.IsCanceled && !task.IsFaulted) {
                    mUser = task.Result;
                    CreateUser(mUser.UserId);
                }
                else {
                    // 토스트메시지 형식으로 사용자에게 ("인터넷 연결을 확인해 주세요.")메시지 구현 필요
                }
            }
        );
    }
    public void IsVaildName(string name) 
    {
        mRef.Child("users").OrderByChild("name").EqualTo(name).GetValueAsync().ContinueWith(task => 
        {
            if(task.IsFaulted) {
                mIsVaildName = false;
                return;
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children) {
                    IDictionary user = (IDictionary)data.Value;
                    if(mUser.UserId == data.Reference.Key) {
                        Debug.Log((user["name"].ToString()));   // 나중에 지우기
                        mIsVaildName = true;
                    }
                    if(name == (string)user["name"] && mUser.UserId != data.Reference.Key) {
                        mIsVaildName = false;
                        // 토스트메시지 형식으로 사용자에게 ("같은 이름이 있거나 비어있어요!!.")메시지 구현 필요
                    }
                    return;
                }
            }
            mIsVaildName = true;
        });
    }

    // User통으로 넘기기
    public void CreateUser(string _userID)
    {
        User user = new User();
        mRef.Child("users").Child(_userID).SetValueAsync(user.ToDictionary());
    }
    public void UpdateUser()
    {
        string _userID = mUser.UserId;
        User user = new User();
        user.SetUserName(SceneData._instance.username);
        user.SetUserScore(GameManager._instance.nScore);
        user.SetUserBestScore(GameManager._instance.nMaxScore);
        mRef.Child("users").Child(_userID).UpdateChildrenAsync(user.UpdateToDictionary());
    }
}