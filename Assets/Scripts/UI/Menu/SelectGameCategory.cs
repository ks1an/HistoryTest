using UnityEngine;
using UnityEngine.UI;

public class SelectGameCategory : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            _button.onClick.Invoke();
        }
    }
}
