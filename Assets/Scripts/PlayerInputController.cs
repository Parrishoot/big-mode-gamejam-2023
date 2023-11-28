using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    [SerializeField]
    private CharacterMovementController characterMovementController;

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
    }

    private void CheckMovement() {        
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical"); 

        CameraController currentCameraController = CameraPerspectiveSwapper.Instance.GetCurrentCameraController();

        Vector3 movementVector = Vector3.zero;

        movementVector += xMovement * currentCameraController.GetHorizontalMovementVector();
        movementVector += yMovement * currentCameraController.GetVecticalMovementVector();

        characterMovementController.Move(movementVector);
    }
}
