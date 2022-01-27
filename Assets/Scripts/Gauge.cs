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
    [SerializeField]
    private Animator animator;
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
        S2_SoundManager._instance.S_BGM.Pause();
        S2_SoundManager._instance.S_FeverBGM.Play();
        print("Fever~!\n");
        Feverpad.SetActive(true);
        ChickGroup.SetActive(false);
        IsFever = true;
        yield return new WaitForSeconds(5.5f);
        Feverpad.SetActive(false);
        ChickGroup.SetActive(true);
        IsFever = false;
        S2_SoundManager._instance.S_FeverBGM.Stop();
        S2_SoundManager._instance.S_BGM.Play();
    }
    public void FeverTouch()
    {
        S2_SoundManager._instance.S_Fever.Play();
        animator.SetTrigger("Click");
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
            case int n when(0<=n && n <6000):
                sec = (1/5.0f);
                break;
            case int n when(6000<=n && n <8000):
                sec = (1/3.0f);
                break;
            default:
                sec = (1/2f);
                break;
        }
        if(!GameManager._instance.IsOver && !GameManager._instance.IsPause && !IsFever) {
            fDecrement += sec * Time.deltaTime;
            if (TimeBar.localScale.x <= 0) {
                S2_SoundManager._instance.S_Fail.Play();
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