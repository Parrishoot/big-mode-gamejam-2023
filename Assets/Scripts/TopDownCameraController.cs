using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : CameraController
{
    public override Vector3 GetHorizontalMovementVector()
    {
        return Vector3.right;
    }

    public override Vector3 GetVecticalMovementVector()
    {
        return Vector3.forward;
    }

    private void OnEnable() {
        Camera.main.orthographic = true;
    }

    private void OnDisable() {
        Camera.main.orthographic = false;
    }
}
