using UnityEngine;

public class TryExitGameButton : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundExit;
    [SerializeField] private GameObject _panelExit;

    private Animator _panelExitAnim;

    private void Awake()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Destroy(gameObject);
        }
        else
        {
            _panelExitAnim = _panelExit.GetComponent<Animator>();
        }
    }

    public void TryToExitGame()
    {
        _backgroundExit.SetActive(true);
        _panelExit.SetActive(true);

        _panelExitAnim.SetTrigger("In");
    }

    public void CancelExit()
    {
        _backgroundExit.SetActive(false);
        _panelExit.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}
