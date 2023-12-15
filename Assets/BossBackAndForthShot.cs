using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossBackAndForthShot : BossGunBehavior
{
    [SerializeField]
    private int numberOfLoops = 8;

    [SerializeField]
    private float rotateTime = 1f;

    [SerializeField]
    private GameObject containerObject;

    private int numberOfLoopsLeft;

    private bool transitioning;

    void OnEnable() {
        numberOfLoopsLeft = numberOfLoops;
        transitioning = false;
    }

    void Update() {

        if(!IsShooting() && !transitioning) {

            numberOfLoopsLeft--;

            if(numberOfLoopsLeft < 0) {
                enabled = false;
            }
            else {
                float rotateAmount = numberOfLoopsLeft % 2 == 0 ? -45 : 45;
                transitioning = true;
                containerObject.transform.DOLocalRotate(containerObject.transform.eulerAngles + (Vector3.up * rotateAmount), rotateTime)
                                 .SetEase(Ease.InOutCubic)
                                 .OnComplete(() => {
                                    transitioning = false;
                                    StartCoroutine(ShootCheckerboard(BossMeta.ALL_SIDES));
                                 });
            }
        }
    }
}
