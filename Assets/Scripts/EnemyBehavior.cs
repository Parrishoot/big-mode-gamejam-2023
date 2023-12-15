using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private int priority = 1;

    [SerializeField]
    private EnemyWaitBehavior enemyWaitBehavior;

    public virtual bool BehaviorRequired() {
        return false;
    }

    public virtual int GetPriority() {
        return priority;
    }

    protected virtual void OnDisable() {
        enemyWaitBehavior?.WaitNext();    
    }
}
