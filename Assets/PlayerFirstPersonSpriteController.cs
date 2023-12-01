using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerFirstPersonSpriteController : PerspectiveDependentController
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Vector3 startingScale; 

    void Awake() {
        startingScale = transform.localScale;
    }

    public override void OnPerspectiveEnd()
    {
        // transform.localScale = startingScale;
    }

    public override void OnPerspectiveStart()
    {
        transform.localScale = Vector3.zero;
    }

    public override void OnTransitionToEnd()
    {
        spriteRenderer.material.DOFade(1f, GetTransitionTime()).SetEase(Ease.InOutCubic);
        transform.DOScale(startingScale, GetTransitionTime()).SetEase(Ease.InOutCubic);
    }

    public override void OnTransitionToStart()
    {
        spriteRenderer.material.DOFade(0f, GetTransitionTime()).SetEase(Ease.InOutCubic);
        transform.DOScale(Vector3.zero, GetTransitionTime()).SetEase(Ease.InOutCubic);
    }
}
