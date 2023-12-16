using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerTopDownMeshController : PerspectiveDependentController
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
        // foreach(MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>()) {
        //     meshRenderer.material.DOFade(1, GetTransitionTime()).SetEase(Ease.InOutCubic);
        // }

        transform.DOScale(Vector3.one, GetTransitionTime()).SetEase(Ease.InOutCubic);
    }
}
