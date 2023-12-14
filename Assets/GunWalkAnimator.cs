using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunWalkAnimator : MonoBehaviour
{
    [SerializeField]
    private float bounceCycleTime = .2f;

    [SerializeField]
    private float bounceAmount = .1f;

    [SerializeField]
    private CharacterMovementController characterMovementController;

    private float baseY;

    private Sequence walkSequence;

    private bool checkWalkSequence = true;

    void Start() {
        
        baseY = transform.localPosition.y;

        walkSequence = DOTween.Sequence()
                              .Join(transform.DOLocalMoveY(baseY + bounceAmount, bounceCycleTime / 2).SetEase(Ease.OutCubic))
                              .Append(transform.DOLocalMoveY(baseY, bounceCycleTime / 2).SetEase(Ease.InCubic))
                              .SetLoops(-1)
                              .Pause();
                        
        
    }

    void Update() {
        if(characterMovementController.IsMoving() && !walkSequence.IsPlaying()) {
            walkSequence.Play();
        }
        else if(!characterMovementController.IsMoving()){
            walkSequence.Pause();
        }
    }
}
