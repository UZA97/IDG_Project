// using System.Collections;
// using UnityEngine;
// using Firebase;
// using Firebase.Auth;
// using Firebase.Database;
// using Firebase.Extensions;

// public class DBManager : MonoBehaviour
// {
//     private FirebaseAuth mAuth;
//     private DatabaseReference mRef;
//     private Firebase.Auth.FirebaseUser mUser;
//     public static DBManager _instance;
//     public bool mIsVaildName = false;
//     private string[] mRank;
//     private string mDatabaseUrl = "https://squid-9654f-default-rtdb.firebaseio.com/";
//     private void Awake()
//     {
//         if(_instance == null) {
//             _instance = this;
//         }
//         mAuth = FirebaseAuth.DefaultInstance;
//         mRef = FirebaseDatabase.DefaultInstance.RootReference;
//         FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(mDatabaseUrl);
//     }
//     private void Start()
//     {
//         Login();
//     }
//     private void Login()
//     {
//         mAuth.SignInAnonymouslyAsync().ContinueWith(
//             task => { 
//                 if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled) {
//                     mUser = task.Result;
//                 }
//                 else if(!task.IsCanceled && !task.IsFaulted) {
//                     mUser = task.Result;
//                     CreateUser(mUser.UserId);
//                 }
//                 else {
//                     // 토스트메시지 형식으로 사용자에게 ("인터넷 연결을 확인해 주세요.")메시지 구현 필요
//                 }
//             }
//         );
//     }
//     public void IsVaildName(string name) 
//     {
//         mRef.Child("users").OrderByChild("name").EqualTo(name).GetValueAsync().ContinueWith(
//             task => 
//             {
//                 if(task.IsFaulted) {
//                     mIsVaildName = false;
//                     return;
//                 }
//                 else if (task.IsCompleted) {
//                     DataSnapshot snapshot = task.Result;
//                     foreach (DataSnapshot data in snapshot.Children) {
//                         IDictionary user = (IDictionary)data.Value;
//                         if(mUser.UserId == data.Reference.Key) {
//                             Debug.Log((user["name"].ToString()));   // 나중에 지우기
//                             mIsVaildName = true;
//                         }
//                         if(name == (string)user["name"] && mUser.UserId != data.Reference.Key) {
//                             mIsVaildName = false;
//                             // 토스트메시지 형식으로 사용자에게 ("같은 이름이 있거나 비어있어요!!.")메시지 구현 필요
//                         }
//                         return;
//                     }
//                 }
//                 mIsVaildName = true;
//             }
//         );
//     }

//     // User통으로 넘기기
//     public void CreateUser(string _userID)
//     {
//         User user = new User();
//         mRef.Child("users").Child(_userID).Child("score").SetValueAsync(user.GetUserScore());
//         mRef.Child("users").Child(_userID).Child("name").SetValueAsync(user.GetUserName());
//         // mRef.Child("users").Child(_userID).Child("rank").SetValueAsync(user.GetUserRank());
//         mRef.Child("users").Child(_userID).Child("best_score").SetValueAsync(user.GetUserBestScore());
//         mRef.Child("users").Child(_userID).Child("create_at").SetValueAsync(user.user_createAt);
//         mRef.Child("users").Child(_userID).Child("update_at").SetValueAsync(user.user_updateTime);
//     }

//     // 지울 함수
//     public void PushUserInfo(string _name, int _score)
//     {
//         User user  = new User();
//         // Push를 이용하면, _userID로 적용했던 부분이 임의의 키로 설정되고, 이를 반환해줌
//         string key = mRef.Child("users").Push().Key;
//         mRef.Child("users").Child(key).Child("name").SetValueAsync(_name);
//         // mRef.Child("users").Child(key).Child("rank").SetValueAsync(user.GetUserRank());
//         mRef.Child("users").Child(key).Child("best_score").SetValueAsync(_score);
//     }
    
//     public void UpdateUser()
//     {
//         string _userID = mUser.UserId;
//         User user = new User();
//         user.SetUserName(SceneData._instance.username);
//         user.SetUserScore(GameManager._instance.nScore);
//         user.SetUserBestScore(GameManager._instance.nMaxScore);
//         mRef.Child("users").Child(_userID).UpdateChildrenAsync(user.ToDictionary());
//     }

//     // 읽기
//     // public void ReadUserInfos()
//     // {
//     //     mRef.Child("users").GetValueAsync().ContinueWithOnMainThread(task =>
//     //         {
//     //             if (task.IsCompleted) {
//     //                 DataSnapshot snapshot = task.Result;
//     //                 foreach (DataSnapshot data in snapshot.Children) {
//     //                     IDictionary userInfo = (IDictionary)data.Value;
//     //                     Debug.Log("Name: " + userInfo["name"] + " / Score: " + userInfo["best_score"] + " / ");
//     //                 }
//     //             }
//     //         }
//     //     );
//     // }
// }
