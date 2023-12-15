using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnAnimator : MonoBehaviour
{
    [SerializeField]
    private float spawnAnimationTime = .5f;

    [SerializeField]
    private float deathAnimationTime = .5f;

    [SerializeField]
    private MaterialSwapper materialSwapper;

    [SerializeField]
    private AudioSource spawnSound;

    [SerializeField]
    private bool playAnimationOnAwake = true;

    private Timer animationTimer;

    private bool spawned = false;

    protected void Start() {

        if(materialSwapper == null) {
            materialSwapper = GetComponent<MaterialSwapper>();
        }

        materialSwapper.SetShaderFloatValue("_DeathFadePercentage", 0f);

        if(playAnimationOnAwake) {
            PlaySpawnAnimation();
        }
    }

    void Update() {
        if(animationTimer != null && !animationTimer.IsFinished()) {
            animationTimer.DecreaseTime(Time.deltaTime);
            materialSwapper.SetShaderFloatValue("_DeathFadePercentage", spawned ? animationTimer.GetPercentageFinished() : animationTimer.GetPercentageRemaining());
        }
    }

    public void PlaySpawnAnimation() {
        PlaySpawnAnimation(null);
    }

    public void PlaySpawnAnimation(Action onAnimationFinished) {

        if(spawned) {
            return;
        }

        if(spawnSound != null) {
            spawnSound.Play();
        }

        materialSwapper.SetAllActive();

        PlayAnimation(true, onAnimationFinished);
    }

    public void PlayDespawnAnimation() {
        PlayDespawnAnimation(null);
    }

    public void PlayDespawnAnimation(Action onAnimationFinished) {

        if(!spawned) {
            return;
        }

        PlayAnimation(false, () => {
            onAnimationFinished?.Invoke();
            materialSwapper.SetAllInActive();
        });
    }

    private void PlayAnimation(bool isSpawning, Action onAnimationFinished) {

        animationTimer = new Timer(isSpawning ? spawnAnimationTime : deathAnimationTime);

        if(onAnimationFinished != null) {    
            animationTimer.AddOnTimerFinishedEvent(() => {
                onAnimationFinished();
            });
        }

        spawned = isSpawning;
    }
}
