using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : ParameterizedEventIngester<HealthController.EventType>
{
    [SerializeField]
    private EnemySpawnAnimator enemySpawnAnimator;

    [SerializeField]
    private GameObject parentObject;

    protected override void OnEventTrigger(HealthController.EventType eventType)
    {
        if(eventType != HealthController.EventType.DEATH) {
            return;
        }

        enemySpawnAnimator.PlayDespawnAnimation(() => {
            Destroy(parentObject);
        });
    }
}
