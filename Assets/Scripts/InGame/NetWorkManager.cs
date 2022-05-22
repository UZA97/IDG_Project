using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetWorkManager : MonoBehaviour
{
    public GameObject NetworkPanel;
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
		Application.targetFrameRate = 60;

        DontDestroyOnLoad(gameObject);
        if(PlayerPrefs.HasKey("UserName")) {
            SceneManager.LoadScene("2.Main");
        }
    }
    void Update()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable) {
            NetworkPanel.SetActive(true);
        }
        else
            NetworkPanel.SetActive(false);   
    }
}
