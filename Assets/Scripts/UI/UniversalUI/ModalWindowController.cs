using System;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowController : MonoBehaviour
{
    public static ModalWindowController instance;

    public ModalWindowPanel modalWindow => _modalWindow;

    [SerializeField] private ModalWindowPanel _modalWindow;

    private QPanelUI _qPanel;

    private void Awake()
    {
        instance = this;
        _qPanel = GetComponent<QPanelUI>();
    }

    public void ShowInterestFact(string title, Sprite image, string message)
    {
        GameTimer.stop = true;
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowHorizontallNoChoice(title, image, message, CloseInGame);
    }
    public void ShowSelectCategoryPanel(string title, string nameCategory, string categoryMessage, Action confirm, Action decline, Action alt)
    {
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowVertical($"Выберите категорию вопросов {title}", null, $"{nameCategory}\n{categoryMessage}", "Выбрать", "Следующая", "Предыдущая", confirm, decline, alt);
    }
    public void ShowWarningExitToMenu()
    {
        GameTimer.stop = true;
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowVerticalExitOrNot("Внимание!", null, "Вы уверены, что хотите выйти в меню?\n(Весь прогресс за раунд будет утрачен!)", ConfirmExitToMenu, Close);
    }
    public void ShowQuitConfirm()
    {
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowVerticalExitOrNot("", null, "Вы уверены, что хотите выйти?", QuitGame, Close);
    }
    public void CloseInGame()
    {
        GameTimer.stop = false;
        _modalWindow.Close();
    }

    public void Close()
    {
        _modalWindow.Close();
    }

    void ConfirmExitToMenu()
    {
        _qPanel.ExitQPanel();
        Close();
    }

    void QuitGame()
    {
        Application.Quit();
    }

}
