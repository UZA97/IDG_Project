using System.Collections;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public static Gauge _instance;
    public RectTransform TimeBar;
    public RectTransform feverBar;
    [SerializeField]
    private Transform FeverPos; 
    [SerializeField]
    private GameObject Feverpad;
    [SerializeField]
    private GameObject FeverEffectPrefab;
    [SerializeField]
    private GameObject ChickGroup;
    private ParticleSystem FeverEffect;

    public bool IsFever;
    public float fDecrement;
    public int nIncrement;

    private void Awake()
    {
        _instance = this;
        fDecrement = 0.0f;
        nIncrement = 0;
        IsFever = false;
    }
    private void Update()
    {
        Fever();
        TimeLapse();
    }
    private void Fever()
    {
        if (feverBar.localScale.y >= 480) { 
            StartCoroutine(FeverTime());
            StopCoroutine(FeverTime());
            feverBar.localScale = new Vector3(1f, 0.1f, 1);        
            nIncrement = 0;
        }
        else {
            feverBar.localScale = new Vector3(1, nIncrement, 1);  
        }
    }
    IEnumerator FeverTime()
    {
        print("Fever~!\n");
        Feverpad.SetActive(true);
        ChickGroup.SetActive(false);
        IsFever = true;
        yield return new WaitForSeconds(5.0f);
        Feverpad.SetActive(false);
        ChickGroup.SetActive(true);
        IsFever = false;
    }
    public void FeverTouch()
    {
        GameObject EffectObj = Instantiate(FeverEffectPrefab,FeverPos);
        ParticleSystem instantEffect = EffectObj.GetComponent<ParticleSystem>();
        instantEffect.Play();
        Destroy(EffectObj,1);
        GameManager._instance.nScore += 50;
    }
    private void TimeLapse()
    {
        float sec = 0.0f;
        switch(GameManager._instance.nScore)
        {
            case int n when(0<=n && n <1000):
                sec = (1/60.0f);
                break;
            case int n when(1000<=n && n <1500):
                sec = (1/50.0f);
                break;
            case int n when(1500<=n && n <2000):
                sec = (1/40.0f);
                break;
            case int n when(2000<=n && n <3000):
                sec = (1/30.0f);
                break;
            case int n when(3000<=n && n <4000):
                sec = (1/20.0f);
                break;
            case int n when(4000<=n && n <4500):
                sec = (1/10.0f);
                break;
            case int n when(4500<=n && n <5000):
                sec = (1/5.0f);
                break;
            default:
                sec = (1/2.0f);
                break;
        }
        if(!GameManager._instance.IsOver && !GameManager._instance.IsPause && !IsFever) {
            fDecrement += sec * Time.deltaTime;
            if (TimeBar.localScale.x <= 0) {
                GameManager._instance.Wrong();
                TimeBar.localScale = new Vector3(1, 1, 1);        
                fDecrement = 0.0f;
            }
            else {
                TimeBar.localScale = new Vector3(1-fDecrement, 1, 1); 
            }
        }
    }
}