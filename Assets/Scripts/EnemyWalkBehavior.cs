using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkBehavior : TimedEnemyBehavior
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private float range = 10f;

    [SerializeField]
    private float stoppingDistance = .1f;

    private Vector3 targetPosition;

    public enum WalkTargetType {
        RANDOM,
        PREDATOR,
        SCAREDY_CAT
    }

    [SerializeField]
    private WalkTargetType walkTargetType = WalkTargetType.RANDOM;

    protected override void BehaviorDuringTime()
    {
        if(Vector3.Distance(transform.position, targetPosition) <= stoppingDistance) {
            BeginWalk();
        }
    }

    private Vector3 ChooseTargetPosition() {
        
        targetPosition = Vector3.zero;

        switch(walkTargetType) {
            
            case WalkTargetType.RANDOM:
                targetPosition = GetRandomPositionInRange();
                break;

            case WalkTargetType.PREDATOR:
                targetPosition = GetPredatorPositionInRange();
                break;

            case WalkTargetType.SCAREDY_CAT:
                targetPosition = GetScaredyCatPositionInRange();
                break;
        }

        return targetPosition;
    }

    private Vector3 GetPredatorPositionInRange() {
        
        Vector3 direction = (GameUtil.GetPlayerGameObject().transform.position - transform.position).normalized * range;
        direction += transform.position;
        
        return GetSampledPositionForDestination(direction);

    }

    private Vector3 GetScaredyCatPositionInRange() {
        
        Vector3 direction = (GameUtil.GetPlayerGameObject().transform.position - transform.position).normalized * range;
        direction = (transform.position - direction);
        
        return GetSampledPositionForDestination(direction);

    }

    private Vector3 GetRandomPositionInRange() {
        
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;
        
        return GetSampledPositionForDestination(randomDirection);
    }

    private Vector3 GetSampledPositionForDestination(Vector3 destination) {
        NavMeshHit hit;
        
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(destination, out hit, range, 1)) {
            finalPosition = hit.position;            
        }
        
        return finalPosition;
    }

    protected override void OnEnable() {

        navMeshAgent.updateRotation = false;

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

        int attempts = 0;

        do {
            navMeshAgent.SetDestination(ChooseTargetPosition());
            attempts++;
        }
        while(navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete && attempts < 20);
    }


    private void OnDrawGizmos()
    {
        if (navMeshAgent.destination != null)
        {
            Gizmos.color = Color.green;
            {
                // Draw lines joining each path corner
                Vector3[] pathCorners = navMeshAgent.path.corners;
                
                for (int i = 0; i < pathCorners.Length - 1; i++)
                {
                    Gizmos.DrawLine(pathCorners[i], pathCorners[i + 1]);
                }

            }
        }
    }

    public override bool BehaviorRequired() {
        
        GameObject playerObject = GameUtil.GetPlayerGameObject();

        if(playerObject == null) {
            return false;
        }

        if(walkTargetType == WalkTargetType.PREDATOR && 
           Vector3.Distance(transform.position, playerObject.transform.position) >= range * 3) {
            return true;
        }

        if(walkTargetType == WalkTargetType.SCAREDY_CAT && 
           Vector3.Distance(transform.position, playerObject.transform.position) <= range * 2) {
            return true;
        }

        return false;
    }
}
