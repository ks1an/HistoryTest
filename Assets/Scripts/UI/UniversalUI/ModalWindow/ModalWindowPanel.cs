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
    [SerializeField] private Button _confirmButton, _alternateButton, _declineButton;
    [SerializeField] private TextMeshProUGUI _confirmText, _alternateText, _declineText;

    private Action onConfirmAction, onAlternateAction, onDeclineAction;

    public void ShowVertical(string title, Sprite imageToShow, string message, string confirmMessage, string declineMessage, string alternateMessage, Action confirmAction, Action declineAction, Action alternateAction = null)
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

        onConfirmAction = confirmAction;
        _confirmText.text = confirmMessage;

        _declineButton.gameObject.SetActive(declineAction != null);
        _declineText.text = declineMessage;
        onDeclineAction = declineAction;

        _alternateButton.gameObject.SetActive(alternateAction != null);
        _alternateText.text = alternateMessage;
        onAlternateAction = alternateAction;
    }

    public void ShowHorizontal(string title, Sprite imageToShow, string message, string confirmMessage, string declineMessage, string alternateMessage, Action confirmAction, Action declineAction, Action alternateAction = null)
    {
        if (imageToShow == null)
            _iconContainer.gameObject.SetActive(false);
        else
            _iconContainer.gameObject.SetActive(true);

        _horizontalLayoutArea.gameObject.SetActive(true);
        _verticalLayoutArea.gameObject.SetActive(false);

        _headerArea.gameObject.SetActive(!string.IsNullOrEmpty(title));
        _titleField.text = title;

        _iconImage.sprite = imageToShow;
        _iconText.text = message;

        onConfirmAction = confirmAction;
        _confirmText.text = confirmMessage;

        bool hasDecline = (declineAction != null);
        _declineButton.gameObject.SetActive(hasDecline);
        _declineText.text = declineMessage;
        onDeclineAction = declineAction;

        bool hasAlternate = (alternateAction != null);
        _alternateButton.gameObject.SetActive(hasAlternate);
        _alternateText.text = alternateMessage;
        onAlternateAction = alternateAction;

    }
   
    #region Samples ShowVertical

    public void ShowVerticalNoChoice(string title, Sprite imageToShow, string message, Action confirmAction)
    {
        ShowVertical(title, imageToShow, message, "Далее","","",confirmAction, null, null);
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
        gameObject.SetActive(false);
    }
}
