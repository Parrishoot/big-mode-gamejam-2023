using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : BasicEventTrigger
{

    [SerializeField]
    private HitBox hitBox;

    [SerializeField]
    private int totalHealth = 50;

    // Start is called before the first frame update
    void Start()
    {
        hitBox.AddOnHitBoxEnteredEvent(TakeDamage);
    }

    private void TakeDamage(int damage) {
        totalHealth -= damage;

        if(totalHealth <= 0) {
            Death();
        }
    }

    private void Death() {
        TriggerEvent();
        Destroy(gameObject);
    }
}
