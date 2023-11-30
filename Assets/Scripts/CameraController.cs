using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraController : MonoBehaviour
{
    [SerializeField]
    private Shaker shaker;

    public abstract Vector3 GetHorizontalMovementVector();

    public abstract Vector3 GetVecticalMovementVector();

    public Vector3 GetShootVector() {
        return transform.forward;
    }

    public Shaker GetShaker() {
        return shaker;
    }
}
