using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitBehavior : TimedEnemyBehavior
{
    private bool required = false;

    public override bool BehaviorRequired() {
        
        bool doWait = required;
        required = false;

        return doWait;
    }

    protected override void BehaviorDuringTime()
    {
        Debug.Log("Waiting!");
    }

    public void WaitNext() {
        required = true;
    }
}
