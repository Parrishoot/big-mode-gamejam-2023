using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerFPSMeshController : PerspectiveDependentController
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
        //     meshRenderer.material.DOFade(0, GetTransitionTime()).SetEase(Ease.InOutCubic);
        // }

        transform.DOScale(Vector3.zero, GetTransitionTime()).SetEase(Ease.InOutCubic);
    }
}
