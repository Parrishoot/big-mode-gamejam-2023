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

    public void SetFollow(Transform newFollowTransform, bool smoothFollow=true) {
        transform.position = newFollowTransform.position;
        this.followTransform = newFollowTransform;
        this.smoothFollow = smoothFollow;
    }

    void Update() {

        if(smoothFollow) {
            float xDist = (followTransform.position.x - transform.position.x) / (1/followSpeed) * Time.deltaTime;
            float zDist = (followTransform.position.z - transform.position.z) / (1/followSpeed) * Time.deltaTime;

            transform.Translate(xDist, zDist, 0);
        }
        else {
            transform.position = followTransform.position;
        }
    }
}
