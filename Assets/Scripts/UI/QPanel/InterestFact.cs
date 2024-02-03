using UnityEngine;

public class InterestFact : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Enable()
    {
        _anim.SetTrigger("In");
    }

    public void OutAnimation()
    {
        _anim.SetTrigger("Out");
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
