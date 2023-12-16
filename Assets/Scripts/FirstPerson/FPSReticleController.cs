using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FPSReticleController : PerspectiveDependentController
{
    private Vector3 startingPosition;

    
    void Awake() {
        startingPosition = Vector3.zero;;
    }

    public override void OnPerspectiveEnd()
    {
        
    }

    public override void OnPerspectiveStart()
    {
        transform.localPosition = startingPosition;   
    }

    public override void OnTransitionToStart()
    {

        float transitionTime = CameraPerspectiveSwapper.Instance.GetAnimationTime();

        DOTween.Sequence()
               .Append(transform.DOLocalMove(startingPosition, transitionTime).SetEase(Ease.InOutCubic))
               .Join(transform.DORotate(Vector3.forward * 360, transitionTime, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic))
               .Play();
        
    }

    public override void OnTransitionToEnd()
    {
        
    }
}
