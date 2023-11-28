using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField]
    private float characterMovementSpeed = 1f;

    [SerializeField]
    private new Rigidbody rigidbody;

    private Vector3 movementVector;

    void Start() {
        if (rigidbody == null) {
            rigidbody = GetComponent<Rigidbody>();
        }
    }

    public void Move(Vector3 newDirection) {
        movementVector = newDirection.normalized;
    }

    void FixedUpdate() {
        rigidbody.velocity = movementVector * characterMovementSpeed * Time.fixedDeltaTime;
    }
}
