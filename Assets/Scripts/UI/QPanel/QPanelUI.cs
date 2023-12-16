using UnityEngine;

public class QPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _qPanelCore;
    [SerializeField] private GameObject _qCategoryName;


    private void OnEnable()
    {
        GameScript.ActionGameStarted += QuPanelEnable;
        GameScript.ActionGameEnded += QuPanelDisable;
    }

    private void OnDisable()
    {
        GameScript.ActionGameStarted -= QuPanelEnable;
        GameScript.ActionGameEnded -= QuPanelDisable;
    }

    private void QuPanelEnable()
    {
        _qPanelCore.SetActive(true);
        _qPanelCore.GetComponent<Animator>().SetTrigger("in");

        _qCategoryName.SetActive(true);
        _qCategoryName.GetComponent<Animator>().SetTrigger("in");
    }
    private void QuPanelDisable()
    {
        _qPanelCore.GetComponent<Animator>().SetTrigger("out");
    }
}
