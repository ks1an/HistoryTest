using UnityEngine;
using UnityEngine.UI;

public class AnswerButtons : MonoBehaviour
{
    [SerializeField] private Button[] _button;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_button[0].gameObject.activeSelf)
                _button[0].onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_button[1].gameObject.activeSelf)
                _button[1].onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_button[2].gameObject.activeSelf)
                _button[2].onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (_button[3].gameObject.activeSelf)
                _button[3].onClick.Invoke();
        }
    }
}
