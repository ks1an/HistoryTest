using System;
using UnityEngine;

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
    #region QPanel

    public void ShowInterestFact(string title, Sprite image, string message)
    {
        GameTimer.stop = true;
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowHorizontallNoChoice(title, image, message, CloseInGame);
    }
    public void ShowWarningExitToMenu()
    {
        GameTimer.stop = true;
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowVerticalExitOrNot("Внимание!", null, "Вы уверены, что хотите выйти в меню?\n(Весь прогресс за раунд будет утрачен!)", ConfirmExitToMenu, Close);
    }

    #endregion

    #region Menu

    public void ShowSelectCategoryPanel(string title, string nameCategory, string categoryMessage, Action confirm, Action decline, Action alt)
    {
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowVertical($"Выберите категорию вопросов {title}", null, $"{nameCategory}\n{categoryMessage}", "Выбрать", "Следующая", "Предыдущая", confirm, decline, alt);
    }

    public void ShowAboutAutor()
    {
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowHorizontallNoChoice("Об авторе", null, "History Quiz: World Wars © 2024 by Unura company, developer: Yusupov \"ks1an\" is licensed under CC BY-ND 4.0 \n Resources used:\n " +
            "Information on the First World War taken from: wikipedia\n" +
            "Information on World War II taken from: ushmm\n" +
            "The game logo is based on: freepik(author: Muhammad Ali)"
            , Close);
    }

    #endregion

    public void ShowQuitConfirm(Action confirmAction)
    {
        _modalWindow.gameObject.SetActive(true);
        _modalWindow.ShowVerticalExitOrNot("", null, "Вы уверены, что хотите выйти?", confirmAction, Close);
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
}
