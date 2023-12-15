using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossRotatingShot : BossGunBehavior
{
    [SerializeField]
    private float rotateAmount = 360;

    [SerializeField]
    private float rotateSpeed = 5f; 

    [SerializeField]
    private GameObject containerObject;

    private bool finishedRotation = true;

    void OnEnable() {

        finishedRotation = false;

        int clockwise = GameUtil.RandomBool() ? 1 : -1;

        containerObject.transform.DORotate(containerObject.transform.eulerAngles + (Vector3.up * rotateAmount * clockwise), rotateSpeed, RotateMode.FastBeyond360)
                                 .SetEase(Ease.InOutCubic)
                                 .OnComplete(() => finishedRotation = true);
    }

    void Update() {
        if(!IsShooting()) {
            if(finishedRotation) {
                enabled = false;
            }
            else {
               Shoot();
            }
        }
    }

    protected virtual void Shoot() {
        StartCoroutine(Shoot(GetSides()));
    }


    protected virtual BossMeta.Side[] GetSides() {
        return new BossMeta.Side[] {
            BossMeta.Side.TOP, 
            BossMeta.Side.BOTTOM
        };
    }
}
