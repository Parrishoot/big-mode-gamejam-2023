using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedEnemyBehavior : EnemyBehavior
{
    [SerializeField]
    private Vector2 timeBounds;

    private Timer timer;

    protected virtual void OnEnable() {

        float timeToPerformBehavior = GameUtil.GetRandomValueFromBounds(timeBounds);

        if(timeToPerformBehavior.Equals(0)) {
            Finish();
        }

        timer = new Timer(timeToPerformBehavior);
        timer.AddOnTimerFinishedEvent(() => {
            Finish();
        });
    }

    void Update()
    {
        timer?.DecreaseTime(Time.deltaTime);

        BehaviorDuringTime();
    }

    protected abstract void BehaviorDuringTime();

    private void Finish() {
        enabled = false;
    }
}
