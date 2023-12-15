using UnityEngine;

public class QPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _qPanelCore;


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
    }
    private void QuPanelDisable()
    {
        _qPanelCore.GetComponent<Animator>().SetTrigger("out");
    }
}
