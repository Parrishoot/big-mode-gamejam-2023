using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BossRevealBehavior : TimedEnemyBehavior
{
    [SerializeField]
    private GameObject topDownHitBoxObject;

    [SerializeField]
    private GameObject firstPersonHitBoxObject;

    [SerializeField]
    private BossAnimationController bossAnimationController;

    [SerializeField]
    private HealthController bossHealthController;

    [SerializeField]
    private GameObject bottomRow;

    [SerializeField]
    private GameObject middleRow;

    [SerializeField]
    private GameObject topRow;
    
    [SerializeField]
    private GameObject core;

    [SerializeField]
    private float coreSlideTime = .5f;

    [SerializeField]
    private BossDeathBehavior bossDeathBehavior;

    public void Start() {
        bossHealthController.AddOnEventTriggeredEvent(OnHit);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        bool topDown = GameUtil.RandomBool();

        if(!topDown) {  
            firstPersonHitBoxObject.SetActive(true);
            core.transform.DOLocalMoveY(bottomRow.transform.localPosition.y, coreSlideTime).SetEase(Ease.InOutCubic);
        }
        else {
            topDownHitBoxObject.SetActive(true);
            bossAnimationController.Despawn(y : 2);
            core.transform.DOLocalMoveY(topRow.transform.localPosition.y, coreSlideTime).SetEase(Ease.InOutCubic);
        }

        BossMeta.Side sideToDespawn = GameUtil.GetRandomValueFromList(BossMeta.ALL_SIDES.ToList());

        switch(sideToDespawn) {
            case BossMeta.Side.LEFT:
                bossAnimationController.Despawn(x : 0);
                break;
            case BossMeta.Side.RIGHT:
                bossAnimationController.Despawn(x : 2);
                break;
            case BossMeta.Side.TOP:
                bossAnimationController.Despawn(z : 2);
                break;
            case BossMeta.Side.BOTTOM:
                bossAnimationController.Despawn(z : 0);
                break;
        }
    }

    protected override void BehaviorDuringTime()
    {
        
    }

    protected override void OnDisable() {
        base.OnDisable();

        topDownHitBoxObject.SetActive(false);
        firstPersonHitBoxObject.SetActive(false);

        bossAnimationController.Spawn();
        core.transform.DOLocalMoveY(middleRow.transform.localPosition.y, coreSlideTime).SetEase(Ease.InOutCubic);
    }

    public void OnHit(HealthController.EventType eventType) {

        if(eventType == HealthController.EventType.DEATH) {
            enemyWaitBehavior = null;
            bossDeathBehavior.SetDead();
        }

        enabled = false;
    }
}
