using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    private CharacterMovementController characterMovementController;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float TTL = 5;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private HurtBox hurtBox;

    void Start() {
        hurtBox.AddOnHurtBoxHitEvent(() => {
            Destroy(gameObject);
        });
    }

    public void Shoot(Vector3 direction) {
        this.direction = direction;
        timer = new Timer(TTL);
        timer.AddOnTimerFinishedEvent(() => {
            Destroy(gameObject);
        });
    }

    public void Update() {
        characterMovementController.Move(direction);
        timer.DecreaseTime(Time.deltaTime);
    }
}
