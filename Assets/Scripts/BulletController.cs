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

    [SerializeField]
    private bool destroyOnHit = true;

    void Start() {
        hurtBox.AddOnHurtBoxHitEvent(() => {
            if(destroyOnHit) {
                Destroy(gameObject);
            }
        });
    }

    public void Shoot(Vector3 direction) {
        this.direction = direction;
        timer = new Timer(TTL);
        timer.AddOnTimerFinishedEvent(() => {
            Destroy(gameObject);
        });
        characterMovementController.Move(direction);
    }

    public void Update() {
        timer.DecreaseTime(Time.deltaTime);
    }
}
