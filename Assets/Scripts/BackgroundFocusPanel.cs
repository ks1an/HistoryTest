using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Animator))]
public class BackgroundFocusPanel : MonoBehaviour
{
    private Animator _anim;
    private Button _button;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _anim.SetTrigger("In");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            _button.onClick.Invoke();  
        }
    }
}
