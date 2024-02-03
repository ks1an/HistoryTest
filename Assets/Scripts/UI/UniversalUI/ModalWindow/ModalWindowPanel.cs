using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowPanel : MonoBehaviour
{
    [SerializeField] private Transform _box;

    [Header("Header")]
    [SerializeField] private Transform _headerArea;
    [SerializeField] private TextMeshProUGUI _titleField;

    [Header("Content")]
    [SerializeField] private Transform _contentArea;
    [SerializeField] private Transform _verticalLayoutArea;
    [SerializeField] private Transform _heroImageContainer;
    [SerializeField] private Image _heroImage;
    [SerializeField] private TextMeshProUGUI _heroText;

    [Space()]
    [SerializeField] private Transform _horizontalLayoutArea;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _iconText;

    [Header("Footer")]
    [SerializeField] private Transform _footerArea;
    [SerializeField] private Button _alternateButton, _declineButton, _confirmButton;
    [SerializeField] private TextMeshProUGUI _alternateText, _declineText, _confirmText;

    private Action onAlternateAction, onDeclineAction, onConfirmAction;

    public void ShowVertical(string title, Sprite imageToShow, string message, string confirmMessage, string declineMessage, string alternateMessage, Action confirmAction, Action declineAction = null, Action alternateAction = null)
    {
        if(imageToShow == null)
        {
            _heroImageContainer.gameObject.SetActive(false);
        }
        else
        {
            _heroImageContainer.gameObject.SetActive(true);
            _heroImage.sprite = imageToShow;
        }

        _horizontalLayoutArea.gameObject.SetActive(false);
        _verticalLayoutArea.gameObject.SetActive(true);

        _headerArea.gameObject.SetActive(!string.IsNullOrEmpty(title));
        _titleField.text = title;

        _heroText.text = message;

        _alternateButton.gameObject.SetActive(alternateAction != null);
        _alternateText.text = alternateMessage;
        onAlternateAction = alternateAction;

        _declineButton.gameObject.SetActive(declineAction != null);
        _declineText.text = declineMessage;
        onDeclineAction = declineAction;

        onConfirmAction = confirmAction;
        _confirmText.text = confirmMessage;
    }

    public void ShowHorizontal(string title, Sprite imageToShow, string message, string confirmMessage, string declineMessage, string alternateMessage, Action confirmAction, Action declineAction, Action alternateAction = null)
    {
        if (imageToShow == null)
        {
            _iconContainer.gameObject.SetActive(false);
        }
        else
        {
            _iconContainer.gameObject.SetActive(true);
            _iconImage.sprite = imageToShow;
        }

        _horizontalLayoutArea.gameObject.SetActive(true);
        _verticalLayoutArea.gameObject.SetActive(false);

        _headerArea.gameObject.SetActive(!string.IsNullOrEmpty(title));
        _titleField.text = title;

        _iconText.text = message;

        onConfirmAction = confirmAction;
        _confirmText.text = confirmMessage;

        _declineButton.gameObject.SetActive(declineAction != null);
        _declineText.text = declineMessage;
        onDeclineAction = declineAction;

        _alternateButton.gameObject.SetActive(alternateAction != null);
        _alternateText.text = alternateMessage;
        onAlternateAction = alternateAction;

    }
   
    #region Samples ShowVertical

    public void ShowVerticalNoChoice(string title, Sprite imageToShow, string message, Action confirmAction)
    {
        ShowVertical(title, imageToShow, message, "Далее","","", confirmAction, null, null);
    }
    public void ShowVerticalContinueOrBack(string title, Sprite imageToShow, string message, Action confirmAction, Action declineAction)
    {
        ShowVertical(title, imageToShow, message, "Продолжить", "Назад", "", confirmAction, declineAction, null);
    }
    public void ShowVerticalExitOrNot(string title, Sprite imageToShow, string message, Action confirmAction, Action declineAction)
    {
        ShowVertical(title, imageToShow, message, "Выйти", "Отмена", "", confirmAction, declineAction, null);
    }

    #endregion

    #region Samples ShowHorizontal

    public void ShowHorizontallNoChoice(string title, Sprite imageToShow, string message, Action confirmAction)
    {
        ShowHorizontal(title, imageToShow, message, "Далее", "", "", confirmAction, null, null);
    }

    #endregion

    public void Confirm()
    {
        onConfirmAction?.Invoke();
    }
    public void Alternate()
    {
        onAlternateAction?.Invoke();
    }
    public void Decline()
    {
        onDeclineAction?.Invoke();
    }
    public void Close()
    {
        GameTimer.stop = false;
        gameObject.SetActive(false);
    }
}
