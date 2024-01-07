using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private void OnEnable()
    {
        qBackground.QuPanelAnimationInEnded += MenuDisable;
        GameScript.ActionGameEnded += MenuEnable;
    }
    private void OnDisable()
    {
        qBackground.QuPanelAnimationInEnded -= MenuDisable;
        GameScript.ActionGameEnded -= MenuEnable;
    }

    private void MenuEnable()
    {
        _menu.SetActive(true);
    }

    private void MenuDisable()
    {
        _menu.SetActive(false);
    }
}