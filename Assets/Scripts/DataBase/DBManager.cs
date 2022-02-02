using System.Collections;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class DBManager : MonoBehaviour
{
    public static DBManager _instance;
    public FirebaseAuth mAuth;
    public DatabaseReference mRef;
    private Firebase.Auth.FirebaseUser mUser;
    public Text usernameText;
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
    private void Update()
    {
        // if(SceneManager.GetActiveScene().name == "Main") {
        //     UserName();
        // }
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
                }
                else
                    Login();
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
                        mIsVaildName = true;
                    }
                    if(name == (string)user["name"] && mUser.UserId != data.Reference.Key) {
                        mIsVaildName = false;
                    }
                    return;
                }
            }
            mIsVaildName = true;
        });
    }

    public void CreateUser()
    {
        User user = new User();
        user.SetUserName(PlayerPrefs.GetString("UserName"));
        mRef.Child("users").Child(mUser.UserId).SetValueAsync(user.ToDictionary());
    }
    public void UpdateUser()
    {
        string _userID = mUser.UserId;
        User user = new User();
        user.SetUserScore(GameManager._instance.nScore);
        user.SetUserBestScore(GameManager._instance.nMaxScore);
        mRef.Child("users").Child(_userID).UpdateChildrenAsync(user.UpdateToDictionary());
    }
    public void UserName()
    {
        usernameText.text = "ID : " + PlayerPrefs.GetString("UserName");
    }
}