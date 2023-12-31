using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TopDownReticleController : PerspectiveDependentController
{

    [SerializeField]
    private Vector2 mouseSensitivity;

    [SerializeField]
    private float buffer = 10f;

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
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity.x * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity.y * Time.deltaTime;

        transform.localPosition += new Vector3(inputX, inputY, 0);
    }

    public override void OnTransitionToStart()
    {
        float transitionTime = CameraPerspectiveSwapper.Instance.GetAnimationTime();
        DOTween.Sequence()
               .Append(transform.DOMove(startingPosition + Vector3.up * buffer, transitionTime).SetEase(Ease.InOutCubic))
               .Join(transform.DORotate(Vector3.forward * 360, transitionTime, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic))
               .Play();
    }

    public override void OnTransitionToEnd()
    {
        
    }
}
