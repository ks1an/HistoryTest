using UnityEngine;
using UnityEngine.UI;

public class InterestFact : MonoBehaviour
{
    private Animator _anim;
    private Button _button;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_button.gameObject.activeSelf)
                _button.onClick.Invoke();
        }
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
