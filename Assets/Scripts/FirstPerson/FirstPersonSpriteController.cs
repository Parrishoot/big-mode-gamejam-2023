using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FirstPersonSpriteController : PerspectiveDependentController
{
    public override void OnPerspectiveEnd()
    {
        
    }

    public override void OnPerspectiveStart()
    {
        
    }

    public override void OnTransitionToEnd()
    {
        
    }

    public override void OnTransitionToStart()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        transform.DOLookAt(playerGameObject.transform.position, GetTransitionTime() - (GetTransitionTime() * .1f)).SetEase(Ease.InOutCubic);
    }

    void LateUpdate() {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); 
    }
}
