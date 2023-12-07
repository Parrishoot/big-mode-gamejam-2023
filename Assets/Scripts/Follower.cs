using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Follower: MonoBehaviour
{
    [SerializeField]
    protected bool smoothFollow;

    [SerializeField]
    protected float followSpeed;

    protected abstract Vector3 GetTargetPosition();

    // Update is called once per frame
    void Update()
    {
         Vector3 targetPosition = GetTargetPosition();

         if(smoothFollow) {
            float xDist = (targetPosition.x - transform.position.x) / (1/followSpeed) * Time.deltaTime;
            float yDist = (targetPosition.y - transform.position.y) / (1/followSpeed) * Time.deltaTime;
            float zDist = (targetPosition.z - transform.position.z) / (1/followSpeed) * Time.deltaTime;

            transform.Translate(xDist, yDist, zDist, Space.World);
        }
        else {
            transform.position = targetPosition;
        }
    }
}
