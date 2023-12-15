using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossCrossShootBehavior : BossGunBehavior
{
    [SerializeField]
    private float rotateTime = 1f;

    [SerializeField]
    private GameObject containerObject;

    private bool shootingStarted = false;

    private float rotationAmount;

    private BossMeta.Side[] sides = new BossMeta.Side[] {
        BossMeta.Side.LEFT,
        BossMeta.Side.RIGHT,
        BossMeta.Side.TOP, 
        BossMeta.Side.BOTTOM
    };

    void OnEnable() {

        Debug.Log("Boss Cross Shot");

        BeginTransition();
    }

    private void BeginTransition() {

        shootingStarted = false;

        rotationAmount = GameUtil.RandomBool() ? 45 : 90;

        containerObject.transform.DOLocalRotate(containerObject.transform.eulerAngles + Vector3.up * rotationAmount, rotateTime)
                                 .SetEase(Ease.InOutCubic)
                                 .OnComplete(() => {
                                    shootingStarted = true;
                                    StartCoroutine(Shoot(sides, onComplete: OnShootingComplete));
                                 });
    }

    public void OnShootingComplete() {
         containerObject.transform.DOLocalRotate(containerObject.transform.eulerAngles - Vector3.up * rotationAmount, rotateTime)
                                 .SetEase(Ease.InOutCubic)
                                 .OnComplete(() => {
                                    enabled = false;
                                 });
    }
}
