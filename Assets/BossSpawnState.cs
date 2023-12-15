using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnState : EnemyBehavior
{
    [SerializeField]
    private float timeBetweenLayers = .5f;

    [SerializeField]
    private BossAnimationController bossAnimationController;

    private bool spawned = false;

    public override bool BehaviorRequired()
    {
        return !spawned;
    }

    public override int GetPriority()
    {
        return spawned ? 0 : 1;
    }

    void OnEnable() {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {

        for(int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(timeBetweenLayers);
            bossAnimationController.Spawn(y: i);
        }

        enabled = false;
        spawned = true;

        yield return null;
    } 
}
