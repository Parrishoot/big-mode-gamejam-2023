using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject parentObject;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float radius = 10;

    [SerializeField]
    public int numberOfObjects = 16;

    public void Spawn() {

        GameUtil.ClearChildren(parentObject.transform);

        float degreesPerObject = 360 / numberOfObjects;

        for(int i = 0; i < numberOfObjects; i++) {
            GameObject circleObject = Instantiate(prefab, parentObject.transform);
            circleObject.transform.localPosition = Quaternion.AngleAxis(i * degreesPerObject, Vector3.up) * Vector3.right * radius; 
        }
    }
}
