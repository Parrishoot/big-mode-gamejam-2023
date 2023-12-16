using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotateSpeed = new Vector3(0f, 0f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, Space.Self);
    }
}
