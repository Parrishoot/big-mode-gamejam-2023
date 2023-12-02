using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceScaler : MonoBehaviour
{
    [SerializeField]
    private Transform originTransform;

    [SerializeField]
    private float scaleFactor = .5f;

    [SerializeField]
    private float maxScale = 5f;

    void Start() {
        if(originTransform == null) {
            originTransform = GetOriginTransform();
        }
    }

    void Update() {
        float distance = Mathf.Abs(Vector3.Distance(transform.position, originTransform.position));
        float scale = Mathf.Min(maxScale, distance * scaleFactor);
        transform.localScale = Vector3.one * scale;
    }

    protected virtual Transform GetOriginTransform() {
        return GameUtil.GetPlayerGameObject().transform;
    }
}
