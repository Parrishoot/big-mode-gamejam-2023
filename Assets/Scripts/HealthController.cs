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
    private List<HitBox> hitBoxes;

    [SerializeField]
    private int totalHealth = 50;

    void Start()
    {
        foreach(HitBox hitBox in hitBoxes) {
            hitBox.AddOnHitBoxEnteredEvent(TakeDamage);
        }
    }

    private void TakeDamage(int damage) {

        if(!enabled) {
            return;
        }

        totalHealth -= damage;

        if(totalHealth <= 0) {
            TriggerEvent(EventType.DEATH);
            foreach(HitBox hitBox in hitBoxes) {
                hitBox.enabled = false;
            }
        }
        else {
            TriggerEvent(EventType.DAMAGE_TAKEN);
        }
    }

    public int GetCurrentHealth() {
        return totalHealth;
    }

    public void OnDestroy() {
        foreach(HitBox hitBox in hitBoxes) {
            hitBox.RemoveOnHitBoxEnteredEvent(TakeDamage);
        }
    }
}
