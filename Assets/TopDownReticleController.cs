using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TopDownReticleController : PerspectiveDependentController
{
    [SerializeField]
    private Transform topDownGunControllerTransform;

    private Vector3 startingPosition;

    
    void Awake() {
        startingPosition = transform.position;
    }

    public override void OnPerspectiveEnd()
    {

    }

    public override void OnPerspectiveStart()
    {
        
    }

    private void Update() {
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, topDownGunControllerTransform.position);
    }

    public override void OnTransitionToStart()
    {
        float transitionTime = CameraPerspectiveSwapper.Instance.GetAnimationTime();
        transform.DORotate(Vector3.forward * 360, transitionTime, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic);
    }

    public override void OnTransitionToEnd()
    {
        
    }
}
