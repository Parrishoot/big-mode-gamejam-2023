using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossDeathBehavior : EnemyBehavior
{
    [SerializeField]
    BossAnimationController bossAnimationController;

    [SerializeField]
    private float delayTime = .3f;

    [SerializeField]
    private Shaker containerShaker;

    [SerializeField]
    private HealthController bossHealthController;

    [SerializeField]
    private AudioSource spawnAudio;

    [SerializeField]
    private AudioSource deathAudio;

    private bool spawning = false;

    private bool nextAnimation = true;

    private bool dead = false;

    void OnEnable() {
        containerShaker.Shake(strength: .3f, fadeOut: false ).SetLoops(-1);
        deathAudio.Play();
        spawnAudio.volume = 0;
        GameManager.Instance.GameBeaten();
    }


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

    public override bool BehaviorRequired()
    {
        return dead;
    }

    public override int GetPriority()
    {
        return dead ? 1 : 0;
    }

    public void SetDead() {
        dead = true;
    }
}
