using System.Collections;
using UnityEngine;

public class qCategoryName : MonoBehaviour
{
    public void AnimationInEnd()
    {
        StartCoroutine(TemporaryDisplay());
    }

    public void AnimationOutEnd()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator TemporaryDisplay()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<Animator>().SetTrigger("out");
    }
}
