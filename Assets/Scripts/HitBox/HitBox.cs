using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBox: MonoBehaviour
{
    public delegate void OnHit(int damage);

    private OnHit onHitBoxEnteredEvent;

    public void AddOnHitBoxEnteredEvent(OnHit newOnHitBoxEnteredEvent) {
        onHitBoxEnteredEvent += newOnHitBoxEnteredEvent;
    }

    private void OnTriggerEnter(Collider other) {
        
        HurtBox hurtBox = other.GetComponent<HurtBox>();
        
        if(hurtBox != null) {
            onHitBoxEnteredEvent?.Invoke(hurtBox.Damage);
            hurtBox.Process();
        }
    }
}
