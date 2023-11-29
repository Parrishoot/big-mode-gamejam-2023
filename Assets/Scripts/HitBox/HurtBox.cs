using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HurtBox : MonoBehaviour
{
    public delegate void OnHit();

    private OnHit onHurtBoxHitEvent;

    public void AddOnHurtBoxHitEvent(OnHit newOnHurtBoxHitEvent) {
        onHurtBoxHitEvent += newOnHurtBoxHitEvent;
    }

    public void Process() {
        onHurtBoxHitEvent?.Invoke();
    }

    [field:SerializeReference]
    public int Damage { get; set; } = 10;
}
