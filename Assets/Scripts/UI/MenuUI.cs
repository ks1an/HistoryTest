using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menuCore;

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
        _menuCore.SetActive(true);
    }

    private void MenuDisable()
    {
        _menuCore.SetActive(false);
    }

}