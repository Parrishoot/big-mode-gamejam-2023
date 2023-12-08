using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    [SerializeField]
    private HurtBox hurtBox;

    [SerializeField]
    private new ParticleSystem particleSystem;

    [SerializeField]
    private CharacterMovementController characterMovementController;

    private void Start() {
        hurtBox.AddOnHurtBoxHitEvent(() => {
            characterMovementController.enabled = false;
            particleSystem.Play();
        });
    }
}
