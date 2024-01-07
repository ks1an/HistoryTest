using UnityEngine;
using TMPro;

public class VersionTextUI : MonoBehaviour
{
    void Start()
    {
         GetComponent<TextMeshProUGUI>().text = Application.version;
    }
}
