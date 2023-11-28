using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraController : MonoBehaviour
{
    public abstract Vector3 GetHorizontalMovementVector();

    public abstract Vector3 GetVecticalMovementVector();

    public Vector3 GetShootVector() {
        return Vector3.forward;
    }
}
