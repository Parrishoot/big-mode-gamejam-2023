using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private int priority = 1;

    [SerializeField]
    protected EnemyWaitBehavior enemyWaitBehavior;

    [SerializeField]
    private bool canRepeat = true;

    public virtual bool BehaviorRequired() {
        return false;
    }

    public virtual int GetPriority() {
        return priority;
    }

    public virtual bool CanRepeat() {
        return canRepeat;
    }

    protected virtual void OnDisable() {
        enemyWaitBehavior?.WaitNext();    
    }
}
