using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownGunRotatorController : PerspectiveDependentController
{
    [SerializeField]
    private Transform topDownReticleTransform;

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

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.ScreenToWorldPoint(topDownReticleTransform.position));
        transform.eulerAngles = Vector3.up * transform.eulerAngles.y;   
    }
}
