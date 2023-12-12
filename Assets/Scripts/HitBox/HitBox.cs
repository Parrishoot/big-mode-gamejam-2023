using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBox: MonoBehaviour
{
    public delegate void OnHit(int damage);

    private OnHit onHitBoxEnteredEvent;

    [SerializeField]
    private bool softEnabled = false;

    public void AddOnHitBoxEnteredEvent(OnHit newOnHitBoxEnteredEvent) {
        onHitBoxEnteredEvent += newOnHitBoxEnteredEvent;
    }

    private void OnTriggerEnter(Collider other) {
        
        HurtBox hurtBox = other.GetComponent<HurtBox>();
        
        if(hurtBox != null && enabled) {
            onHitBoxEnteredEvent?.Invoke(hurtBox.Damage);
            hurtBox.Process();
        }
        if(softEnabled) {
            hurtBox.Process();
        }
    }

    public void SetSoftEnabled(bool newSoftEnabled) {
        this.enabled = !newSoftEnabled;
        this.softEnabled = newSoftEnabled;
    }
}
