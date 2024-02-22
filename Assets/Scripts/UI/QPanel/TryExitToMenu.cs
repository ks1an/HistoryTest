using UnityEngine;
using UnityEngine.UI;

public class TryExitToMenu : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (!GameTimer.stop)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
            {
                if (_button.gameObject.activeSelf)
                    _button.onClick.Invoke();
            }
        }
    }

}
