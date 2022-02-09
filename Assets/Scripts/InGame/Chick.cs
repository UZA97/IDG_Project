using UnityEngine;
public class Chick : MonoBehaviour
{
    public GameManager gameManager;
    public string ChickName;
    public Animator Chickanimator;
    private void Awake()
    {
        Chickanimator = GetComponent<Animator>();
    }
}
