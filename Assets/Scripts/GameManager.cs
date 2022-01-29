using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    private Chick AfterChick;

    [Header("GameObject")]
    [SerializeField]
    private GameObject[] ChickPrefabs;
    [SerializeField]
    private Transform CreatePos;
    [SerializeField]
    private Transform NextCreatePos;
    [SerializeField]
    private GameObject EffectPrefab;
    private List<GameObject> ListChick;
    private GameObject ChickObj;
    private GameObject NextChickObj;
    private ParticleSystem correctEffect;
    public Chick chick;
    public GameObject ChickGroup;

    [Header("UI")]
    [SerializeField]
    private Text tScore;
    [SerializeField]
    private Text tMaxScore;
    [SerializeField]
    private Image[] ImageUILife;
    [SerializeField]
    private GameObject endGroup;
    [SerializeField]
    private Text tsubScore;
    [SerializeField]
    private Text tSubMaxScore;
    public int nScore;
    public int nLife;
    public int nMaxScore = 0;

    [Header("============================")]

    public bool IsOver;
    public bool IsPause;
    private int ranNextChick;

    private void Awake()
    {
        if (_instance == null) {
            _instance = this;
        }

        if (!PlayerPrefs.HasKey("MaxScore")) {
            PlayerPrefs.SetInt("MaxScore", nMaxScore);
        }
        tMaxScore.text = PlayerPrefs.GetInt("MaxScore").ToString();
        chick = GetComponent<Chick>();
        ChickObj = null;
        NextChickObj = null;
        IsOver = false;
        IsPause = false;
    }

    private void Start()
    {
        MakeChick();
    }
    private void Update()
    {
        tScore.text = nScore.ToString();
    }
    public void MakeChick()
    {
        if (!IsOver)
        {
            if (NextChickObj == null)
            {
                int randChick = Random.Range(0, 3);
                ChickObj = Instantiate(ChickPrefabs[randChick], CreatePos);
                Chick instantChick = ChickObj.GetComponent<Chick>();
                instantChick.gameManager = this;
                chick = instantChick;
                StartCoroutine(WaitNext());
            }
            else
            {
                ChickObj = Instantiate(ChickPrefabs[ranNextChick], CreatePos);
                Chick instantChick = ChickObj.GetComponent<Chick>();
                instantChick.gameManager = this;
                chick = instantChick;
                StartCoroutine(WaitNext());
            }
            NextChick();
        }
    }
    IEnumerator WaitNext()
    {
        while (chick != null) { yield return null; }
        yield return new WaitForSeconds(0.1f);
        Destroy(NextChickObj);
        MakeChick();
    }
    private void NextChick()
    {
        ranNextChick = Random.Range(0, 3);
        NextChickObj = Instantiate(ChickPrefabs[ranNextChick], NextCreatePos);
        NextChickObj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
    public void DestroyChick()
    {
        GameObject EffectObj = Instantiate(EffectPrefab, CreatePos);
        ParticleSystem instantEffect = EffectObj.GetComponent<ParticleSystem>();
        instantEffect.Play();
        nScore += 100;
        Destroy(chick.gameObject);
        Gauge._instance.nIncrement += 32;
        Gauge._instance.TimeBar.localScale = new Vector3(1, 1, 1);
        Gauge._instance.fDecrement = 0.0f;
    }
    public void Wrong()
    {
        IsPause = true;
        chick.Chickanimator.SetBool("Wrong", true);
        nLife--;
        ImageUILife[nLife].color = new Color(0.2f, 0.2f, 0.2f, 0.4f);
        if (nLife <= 0)
        {
            ChickObj.SetActive(false);
            NextChickObj.SetActive(false);
            GameOver();
        }
        Invoke("Pause", 1.0f);
    }
    private void Pause()
    {
        IsPause = false;
        chick.Chickanimator.SetBool("Wrong", false);
    }
    private void GameOver()
    {
        S2_SoundManager._instance.S_GameOver.Play();
        IsOver = true;
        endGroup.SetActive(true);
        tsubScore.text = "최종점수 : " + tScore.text;
        tSubMaxScore.text ="최고기록 : " + PlayerPrefs.GetInt("MaxScore");
        nMaxScore = Mathf.Max(PlayerPrefs.GetInt("MaxScore"), nScore);
        PlayerPrefs.SetInt("MaxScore", nMaxScore);
        DBManager._instance.UpdateUser();
    }
    // public void PushButtonA()
    // {
    //     if (chick == null)
    //         return;
    //     if (!IsPause)
    //     {
    //         if (chick.ChickName == "A") {
    //             S2_SoundManager._instance.S_Chick.Play();
    //             DestroyChick();
    //         }
    //         else {
    //             S2_SoundManager._instance.S_Fail.Play();
    //             Wrong();
    //         }
    //     }
    // }
    // public void PushButtonB()
    // {
    //     if (chick == null)
    //         return;
    //     if (!IsPause)
    //     {
    //         if (chick.ChickName == "B") {
    //             S2_SoundManager._instance.S_Chick.Play();
    //             DestroyChick();
    //         }
    //         else {
    //             S2_SoundManager._instance.S_Fail.Play();
    //             Wrong();
    //         }
    //     }
    // }
    // public void PushButtonC()
    // {
    //     if (chick == null)
    //         return;
    //     if (!IsPause)
    //     {
    //         if (chick.ChickName == "C") {
    //             S2_SoundManager._instance.S_Chick.Play();
    //             DestroyChick();
    //         }
    //         else {
    //             S2_SoundManager._instance.S_Fail.Play();
    //             Wrong();
    //         }
    //     }
    // }
}
