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

    void Start()
    {
        hitBox.AddOnHitBoxEnteredEvent(TakeDamage);
    }

    private void TakeDamage(int damage) {

        if(!enabled) {
            return;
        }

        totalHealth -= damage;

        if(totalHealth <= 0) {
            TriggerEvent(EventType.DEATH);
            hitBox.enabled = false;
        }
        else {
            TriggerEvent(EventType.DAMAGE_TAKEN);
        }
    }

    public int GetCurrentHealth() {
        return totalHealth;
    }
}
