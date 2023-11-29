using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSReticleController : PerspectiveDependentController
{
    private Vector3 startingPosition;

    
    void Awake() {
        startingPosition = transform.position;
    }

    public override void OnPerspectiveEnd()
    {
        
    }

    public override void OnPerspectiveStart()
    {
        transform.position = startingPosition;   
    }
}
