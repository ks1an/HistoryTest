using System;
using UnityEngine;

public class QPanelUI : MonoBehaviour
{
    public static event Action ActionQPanelExited;

    [SerializeField] private GameObject _qPanel;
    [SerializeField] private GameObject _qCategoryName;
    [SerializeField] private GameObject _qProgressBar;
    [SerializeField] private GameObject _qStatsPanel;


    private void Start()
    {
        _qPanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameScript.ActionGameStarted += QPanelEnable;
        GameScript.ActionGameEnded += QStatisticGameEnable;
        QCategoryName.ActionQCategoryAnimEnded += QProgressBarEnable;
    }

    private void OnDisable()
    {
        GameScript.ActionGameStarted -= QPanelEnable;
        GameScript.ActionGameEnded -= QStatisticGameEnable;
        QCategoryName.ActionQCategoryAnimEnded -= QProgressBarEnable;
    }

    public void TryExitQPanel()
    {
        GameTimer.stop = true;

        if (!GameScript.CanExit)
        {
            ModalWindowController.instance.ShowWarningExitToMenu();
        }
        else
        {
            ActionQPanelExited.Invoke();
            QPanelDisable();
        }
    }

    public void ExitQPanel()
    {
        ActionQPanelExited.Invoke();
        GameTimer.stop = false;
        QPanelDisable();
    }

    private void QPanelEnable()
    {
        _qPanel.SetActive(true);
        _qPanel.GetComponent<Animator>().SetTrigger("in");

        _qStatsPanel.SetActive(false);

        _qCategoryName.SetActive(true);
        _qCategoryName.GetComponent<Animator>().SetTrigger("in");

        _qProgressBar.SetActive(true);

        GameTimer.stop = false;
    }

    private void QPanelDisable()
    {
        _qProgressBar.GetComponent<Animator>().SetTrigger("out");
        _qPanel.GetComponent<Animator>().SetTrigger("out");
    }

    private void QProgressBarEnable()
    {
        _qProgressBar.GetComponent<Animator>().SetTrigger("in");
    }

    private void QStatisticGameEnable()
    {
        _qStatsPanel.SetActive(true);
    }
}
