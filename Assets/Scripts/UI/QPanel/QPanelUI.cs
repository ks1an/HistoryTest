using UnityEngine;

public class QPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _qPanelCore;
    [SerializeField] private GameObject _qCategoryName;
    [SerializeField] private GameObject _qProgressBar;


    private void OnEnable()
    {
        GameScript.ActionGameStarted += QPanelEnable;
        GameScript.ActionGameEnded += QPanelDisable;
        QCategoryName.ActionQCategoryAnimEnded += QProgressBarEnable;
    }

    private void OnDisable()
    {
        GameScript.ActionGameStarted -= QPanelEnable;
        GameScript.ActionGameEnded -= QPanelDisable;
        QCategoryName.ActionQCategoryAnimEnded -= QProgressBarEnable;
    }

    private void QPanelEnable()
    {
        _qPanelCore.SetActive(true);
        _qPanelCore.GetComponent<Animator>().SetTrigger("in");

        _qCategoryName.SetActive(true);
        _qCategoryName.GetComponent<Animator>().SetTrigger("in");

        _qProgressBar.SetActive(true);
    }

    private void QPanelDisable()
    {
        _qProgressBar.GetComponent<Animator>().SetTrigger("out");
        _qPanelCore.GetComponent<Animator>().SetTrigger("out");
    }

    private void QProgressBarEnable()
    {
        _qProgressBar.GetComponent<Animator>().SetTrigger("in");
    }
}
