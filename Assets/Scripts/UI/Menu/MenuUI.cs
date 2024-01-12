using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private void OnEnable()
    {
        QPanelUI.ActionQPanelExited += MenuEnable;
        qBackground.QuPanelAnimationInEnded += MenuDisable;
    }


    private void OnDisable()
    {
        QPanelUI.ActionQPanelExited -= MenuEnable;
        qBackground.QuPanelAnimationInEnded -= MenuDisable;
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