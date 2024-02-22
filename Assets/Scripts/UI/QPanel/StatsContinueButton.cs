using UnityEngine;
using UnityEngine.UI;

public class StatsContinueButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.W))
        {
            if (_button.gameObject.activeSelf)
                _button.onClick.Invoke();
        }
    }
}
