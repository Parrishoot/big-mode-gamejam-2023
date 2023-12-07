using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraController : CameraController
{

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector2 mouseSensitivity = new Vector2(2f, 2F);
    
    private float cameraYRotation = 0f;

    public override Vector3 GetHorizontalMovementVector()
    {
        return new Vector3(transform.right.x, 0, transform.right.z);
    }

    public override Vector3 GetVecticalMovementVector()
    {
        return new Vector3(transform.forward.x, 0, transform.forward.z);
    }

    private void OnEnable() {
        cameraYRotation = 0f;
    }

    void Update()
    {

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity.x;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity.y;

        cameraYRotation -= inputY;
        cameraYRotation = Mathf.Clamp(cameraYRotation, -90f, 90f);
        transform.eulerAngles = new Vector3(0, // cameraYRotation, 
                                            transform.localEulerAngles.y + inputX,
                                            0);
    }
}
