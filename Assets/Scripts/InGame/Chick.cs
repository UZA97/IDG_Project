using UnityEngine;
public class Chick : MonoBehaviour
{
    public GameManager gameManager;
    public string chickName;
    public Animator chickanimator;
    private void Awake()
    {
        chickanimator = GetComponent<Animator>();
    }
}
