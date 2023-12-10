using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : ParameterizedEventTrigger<HealthController.EventType>
{

    public enum EventType {
        DEATH,
        DAMAGE_TAKEN
    }

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

        TriggerEvent(EventType.DAMAGE_TAKEN);

        totalHealth -= damage;

        if(totalHealth <= 0) {
            Death();
        }
    }

    private void Death() {
        TriggerEvent(EventType.DEATH);
        Destroy(gameObject);
    }
}
