using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollower : Follower
{
    [SerializeField]
    public Transform followTransform;

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

    protected override Vector3 GetTargetPosition()
    {
        return followTransform.position - offset;
    }
}
