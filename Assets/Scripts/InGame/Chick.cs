using UnityEngine;
public class Chick : MonoBehaviour
{
    public GameManager gameManager;
    public string chickName;
    public Animator chickanimator;
    public ChickType chickType;
    private void Awake()
    {
        chickanimator = GetComponent<Animator>();
    }
}
