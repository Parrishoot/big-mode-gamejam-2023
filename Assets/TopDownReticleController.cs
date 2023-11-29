using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TopDownReticleController : PerspectiveDependentController
{
    [SerializeField]
    private Transform topDownGunControllerTransform;

    public override void OnPerspectiveEnd()
    {

    }

    public override void OnPerspectiveStart()
    {
        
    }

    private void Update() {
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, topDownGunControllerTransform.position);
    }
}
