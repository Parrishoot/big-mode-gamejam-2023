using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    public Transform followTransform;

    [SerializeField]
    public bool smoothFollow = true;

    [SerializeField]
    private float followSpeed = 10f;

    [SerializeField]
    private bool maintainOffset = false;

    private Vector3 offset;

    void Awake() {
        if(followTransform != null) {
            SetFollow(followTransform);
        }
    }

    public void SetFollow(Transform newFollowTransform, bool smoothFollow=true) {

        Vector3 targetPosition = newFollowTransform.position;
        offset = Vector3.zero;

        if(maintainOffset) {
            offset = newFollowTransform.position - transform.position;
            targetPosition = offset;
        }

        transform.position = targetPosition;
        this.followTransform = newFollowTransform;
        this.smoothFollow = smoothFollow;
    }

    void Update() {


        Vector3 targetPosition = followTransform.position - offset;

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
