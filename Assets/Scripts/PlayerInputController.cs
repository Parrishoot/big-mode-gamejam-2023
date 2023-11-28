using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    [SerializeField]
    private CharacterMovementController characterMovementController;

    [SerializeField]
    private PlayerGunManager playerGunManager;

    [SerializeField]
    private GunController gunController;

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        CheckShoot();
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

    private void CheckShoot() {
        if(Input.GetMouseButtonDown(0)) {
            playerGunManager.GetCurrentGunController().Fire();
        }
    }
}
