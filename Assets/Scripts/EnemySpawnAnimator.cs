using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnAnimator : ParameterizedEventIngester<HealthController.EventType>
{
    [SerializeField]
    private float spawnAnimationTime = .5f;

    [SerializeField]
    private float deathAnimationTime = .5f;

    [SerializeField]
    private MaterialSwapper materialSwapper;

    [SerializeField]
    private GameObject parentObject;

    [SerializeField]
    private AudioSource audioSource;

    private Timer animationTimer;

    private bool spawning = true;

    protected override void Start() {
        animationTimer = new Timer(spawnAnimationTime);
        audioSource.Play();
        base.Start();
    }

    void Update() {
        if(animationTimer != null && !animationTimer.IsFinished()) {
            animationTimer.DecreaseTime(Time.deltaTime);
            materialSwapper.SetShaderFloatValue("_DeathFadePercentage", spawning ? animationTimer.GetPercentageFinished() : animationTimer.GetPercentageRemaining());
        }
    }

    protected override void OnEventTrigger(HealthController.EventType eventType)
    {
        if(eventType != HealthController.EventType.DEATH) {
            return;
        }

        animationTimer = new Timer(deathAnimationTime);
        animationTimer.AddOnTimerFinishedEvent(() => {
            Destroy(parentObject);
        });
        spawning = false;
    }
}
