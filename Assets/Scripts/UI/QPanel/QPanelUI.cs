using UnityEngine;

public class QPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _qPanel;
    [SerializeField] private GameObject _qCategoryName;
    [SerializeField] private GameObject _qProgressBar;

    private void Start()
    {
        _qPanel.SetActive(false);
    }

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
        _qPanel.SetActive(true);
        _qPanel.GetComponent<Animator>().SetTrigger("in");

        _qCategoryName.SetActive(true);
        _qCategoryName.GetComponent<Animator>().SetTrigger("in");

        _qProgressBar.SetActive(true);
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
}
