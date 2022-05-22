using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("GameObject")]
    [SerializeField]
    private GameObject[] ChickPrefabs;
    [SerializeField]
    private Transform CreatePos;
    [SerializeField]
    private Transform NextCreatePos;
    [SerializeField]
    private GameObject EffectPrefab;
    private GameObject ChickObj;
    private GameObject NextChickObj;
    public Chick chick;
    public GameObject ChickGroup;
    public Gauge gauge;

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
    public int nScore;
    public int nLife;
    public int nMaxScore = 0;

    [Header("=============================")]
    public bool isOver;
    public bool isPause;
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
        isOver = false;
        isPause = false;
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
        if (!isOver)
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
        Destroy(EffectObj,1);
        nScore += 100;
        Destroy(chick.gameObject);
        gauge.fIncrement += 24f;
        gauge.fDecrement = 0.0f;
    }
    public void Wrong()
    {
        isPause = true;
        chick.chickanimator.SetBool("Wrong", true);
        nLife--;
        ImageUILife[nLife].color = new Color(0.2f, 0.2f, 0.2f, 0.4f);
        if (nLife <= 0)
        {
            ChickObj.SetActive(false);
            NextChickObj.SetActive(false);
            GameOver();
        }
        Invoke("Pause", 0.75f);
    }
    private void Pause()
    {
        isPause = false;
        chick.chickanimator.SetBool("Wrong", false);
    }
    private void GameOver()
    {
        SoundManager._instance.gameOverSound.Play();
        isOver = true;
        endGroup.SetActive(true);
        tsubScore.text = ": "+tScore.text;
        nMaxScore = Mathf.Max(PlayerPrefs.GetInt("MaxScore"), nScore);
        PlayerPrefs.SetInt("MaxScore", nMaxScore);
        DBManager._instance.UpdateUser();
    }
}
