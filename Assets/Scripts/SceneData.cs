using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    public static SceneData _instance;
    public string username;
    private void Awake()
    {
        Application.targetFrameRate = 60;   // 프레임   60고정
        if(_instance == null) {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
