using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TopDownSpriteController : PerspectiveDependentController
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
        transform.DORotate(Vector3.right * 90, GetTransitionTime()).SetEase(Ease.InOutCubic);
    }
}
