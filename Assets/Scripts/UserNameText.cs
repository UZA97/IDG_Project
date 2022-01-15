using UnityEngine;
using UnityEngine.UI;

public class UserNameText : MonoBehaviour
{
    public Text username;
    private void Start()
    {
        username = GetComponent<Text>();
    }
    void Update()
    {
        username.text = "반갑습니다 " + SceneData._instance.username.ToString() + "님"; 
    }
}
