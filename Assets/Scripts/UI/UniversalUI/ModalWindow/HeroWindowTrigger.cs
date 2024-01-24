using System;
using UnityEngine;
using UnityEngine.Events;

public class HeroWindowTrigger : MonoBehaviour
{
    public UnityEvent onContinueEvent;
    public UnityEvent onCancelEvent;
    public UnityEvent onAlternateEvent;

    public string title;
    public Sprite sprite;
    public string message;
    public bool triggerOnEnable;

    private void OnEnable()
    {
        if (!triggerOnEnable)
            return;

        Action continueCallback = null;
        Action cancelCallback = null;
        Action alternateCallback = null;

        if (onContinueEvent.GetPersistentEventCount() > 0)
            continueCallback = onContinueEvent.Invoke;
        if(onCancelEvent.GetPersistentEventCount() > 0)
            cancelCallback = onCancelEvent.Invoke;
        if (onAlternateEvent.GetPersistentEventCount() > 0)
            alternateCallback = onAlternateEvent.Invoke;

        ModalWindowController.instance.modalWindow.ShowVerticalContinueOrBack(title, sprite, message, continueCallback, cancelCallback);
    }
}
