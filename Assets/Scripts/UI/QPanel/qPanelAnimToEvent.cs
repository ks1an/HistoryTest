using System;
using UnityEngine;

public class qBackground : MonoBehaviour
{
    public static event Action QuPanelAnimationInEnded;

    public void AnimationInEnd()
    {
        QuPanelAnimationInEnded?.Invoke();
    }
    
    public void AnimationOutEnd()
    {
        this.gameObject.SetActive(false);
    }
}
