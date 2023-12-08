using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkBehavior : TimedEnemyBehavior
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private float range = 10f;

    public enum WalkTargetType {
        RANDOM
    }

    [SerializeField]
    private WalkTargetType walkTargetType = WalkTargetType.RANDOM;

    protected override void BehaviorDuringTime()
    {
        if(navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete) {
            Debug.Log("At desintation!");
            BeginWalk();
        }
    }

    private Vector3 ChooseTargetPosition() {
        switch(walkTargetType) {
            
            case WalkTargetType.RANDOM:
                return GetRandomPositionInRange();
            
            // Shouldn't happen
            default: return transform.position;
        }
    }

    private Vector3 GetRandomPositionInRange() {
        
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;
        
        NavMeshHit hit;
        
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, 1)) {
            finalPosition = hit.position;            
        }
        
        return finalPosition;

    }

    private void EnsureOnGround() {
        
        NavMeshHit hit;
        
        if (NavMesh.SamplePosition(transform.position, out hit, 500, 1)) {
            transform.position = hit.position;  
        }
    }

    protected override void OnEnable() {

        BeginWalk();

        base.OnEnable();
    }

    public void OnDisable() {
        if(navMeshAgent.isActiveAndEnabled) {
            navMeshAgent.isStopped = true;
        }
    }

    private void BeginWalk() {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(ChooseTargetPosition());
    }
}
