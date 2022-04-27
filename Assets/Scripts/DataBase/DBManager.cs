using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class DBManager : MonoBehaviour
{
    public static DBManager _instance;
    public FirebaseAuth auth;
    public DatabaseReference reference;
    private FirebaseUser firebaseUser;
    public Text usernameText;
    public bool isVaildName = false;
    private string databaseUrl = "https://test-ecd95-default-rtdb.firebaseio.com/";

    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
        }
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(databaseUrl);
    }
    private void Start()
    {
        Login();
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "2.Main") {
            UserName();
        }
    }
    private void Login()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(
            task => { 
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled) {
                    firebaseUser = task.Result;
                }
                else if(!task.IsCanceled && !task.IsFaulted) {
                    firebaseUser = task.Result;
                }
                else
                    Login();
            }
        );
    }
    public void IsVaildName(string name) 
    {
        reference.Child("users").OrderByChild("name").EqualTo(name).GetValueAsync().ContinueWith(task => 
        {
            if(task.IsFaulted) {
                isVaildName = false;
                return;
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children) {
                    IDictionary userInfo = (IDictionary)data.Value;
                    if(firebaseUser.UserId == data.Reference.Key) {
                        isVaildName = true;
                    }
                    if(name == (string)userInfo["name"] && firebaseUser.UserId != data.Reference.Key) {
                        isVaildName = false;
                    }
                    return;
                }
            }
            isVaildName = true;
        });
    }

    public void CreateUser()
    {
        User user = new User();
        user.SetUserName(PlayerPrefs.GetString("UserName"));
        reference.Child("users").Child(firebaseUser.UserId).SetValueAsync(user.ToDictionary());
    }
    public void UpdateUser()
    {
        string _userID = firebaseUser.UserId;
        User user = new User();
        user.SetUserScore(GameManager._instance.nScore);
        user.SetUserBestScore(GameManager._instance.nMaxScore);
        reference.Child("users").Child(_userID).UpdateChildrenAsync(user.UpdateToDictionary());
    }
    public void UserName()
    {
        usernameText.text = "ID : " + PlayerPrefs.GetString("UserName");
    }
}