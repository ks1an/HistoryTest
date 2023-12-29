using System;
using System.Collections;
using UnityEngine;

public class QCategoryName : MonoBehaviour
{
    public static event Action ActionQCategoryAnimEnded;

    public void AnimationInEnd()
    {
        StartCoroutine(TemporaryDisplay());
    }

    public void AnimationOutEnd()
    {
        ActionQCategoryAnimEnded?.Invoke();
        this.gameObject.SetActive(false);
    }

    private IEnumerator TemporaryDisplay()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<Animator>().SetTrigger("out");
    }
}
