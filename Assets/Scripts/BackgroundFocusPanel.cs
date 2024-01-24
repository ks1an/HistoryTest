using UnityEngine;

[RequireComponent (typeof(Animator))]
public class BackgroundFocusPanel : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _anim.SetTrigger("In");
    }
}
