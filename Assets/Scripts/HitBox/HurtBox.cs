using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HurtBox : MonoBehaviour
{
    public delegate void OnHit();

    private OnHit onHurtBoxHitEvent;

    public void AddOnHurtBoxHitEvent(OnHit newOnHurtBoxHitEvent) {
        onHurtBoxHitEvent += newOnHurtBoxHitEvent;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<HitBox>() == null) {
            onHurtBoxHitEvent?.Invoke();
        }
    }

    public void Process() {
        onHurtBoxHitEvent?.Invoke();
    }

    [field:SerializeReference]
    public int Damage { get; set; } = 10;
}
