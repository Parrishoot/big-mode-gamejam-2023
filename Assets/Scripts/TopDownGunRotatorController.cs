using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TopDownGunRotatorController : PerspectiveDependentController
{
    [SerializeField]
    private Transform topDownReticleTransform;

    public override void OnPerspectiveEnd()
    {

    }

    public override void OnPerspectiveStart()
    {
        transform.localScale = Vector3.one * 2;
        transform.eulerAngles = GetRotation();
    }

    public override void OnTransitionToEnd()
    {

    }

    public override void OnTransitionToStart()
    {
        transform.DOScale(Vector3.one * 2, GetTransitionTime());
        transform.DOLocalRotate(GetRotation(), GetTransitionTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.ScreenToWorldPoint(topDownReticleTransform.position));
        transform.eulerAngles = Vector3.up * transform.eulerAngles.y;   
    }

    public Vector3 GetRotation() {
        return new Vector3(0,
                           Mathf.Round(transform.eulerAngles.y / 90) * 90,
                           0);
    }
}
