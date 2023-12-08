using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonPlayerGunRotatorController : PerspectiveDependentController
{
    [SerializeField]
    Transform cameraTransform;

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
        
    }

    private void LateUpdate() {
        transform.eulerAngles = cameraTransform.eulerAngles;
    }
}
