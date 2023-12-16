using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FirstPersonPlayerGunRotatorController : PerspectiveDependentController
{
    [SerializeField]
    Transform cameraTransform;

    private bool transitioning = false;

    public override void OnPerspectiveEnd()
    {
        
    }

    public override void OnPerspectiveStart()
    {
        transitioning = false;
    }

    public override void OnTransitionToEnd()
    {
        transitioning = true;
    }

    public override void OnTransitionToStart()
    {
        transform.DOScale(Vector3.one, GetTransitionTime());
        transform.DOLocalRotate(cameraTransform.eulerAngles.y * Vector3.up, GetTransitionTime()).SetEase(Ease.InOutCubic);
    }

    private void Update() {
        if(!transitioning) {
            transform.eulerAngles = cameraTransform.eulerAngles;
        }
    }
}
