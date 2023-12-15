using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossDebugBehavior : EnemyBehavior
{
    [SerializeField]
    BossAnimationController bossAnimationController;

    [SerializeField]
    private float delayTime = .3f;

    private bool spawning = false;

    private bool nextAnimation = true;

    void Update() {
        if(nextAnimation) {
            nextAnimation = false;
            StartCoroutine(spawning ? PlaySpawnAnimation() : PlayDespawnAnimation());
        }
    }

    public IEnumerator PlaySpawnAnimation() {

        for(int i = 0; i < 3; i++) {
            bossAnimationController.Spawn(y:i);
            yield return new WaitForSeconds(delayTime);
        }

        spawning = false;
        nextAnimation = true;

        yield return null;
    }

    public IEnumerator PlayDespawnAnimation() {

        for(int i = 0; i < 3; i++) {
            bossAnimationController.Despawn(y:i);
            yield return new WaitForSeconds(delayTime);
        }

        spawning = true;
        nextAnimation = true;

        yield return null;
    }
}
