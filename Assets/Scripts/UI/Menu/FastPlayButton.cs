using UnityEngine;
using UnityEngine.UI;

public class FastPlayButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_button.gameObject.activeSelf)
                _button.onClick.Invoke();
        }
    }
}
