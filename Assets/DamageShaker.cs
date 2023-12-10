using DG.Tweening;
using UnityEngine;

public class DamageInvincibilityController: MonoBehaviour
{

    [SerializeField]
    private HealthController healthController;

    [SerializeField]
    private Shaker shaker;

    [SerializeField]
    private MaterialSwapper materialSwapper;

    [SerializeField]
    private Material damageMaterial;

    [SerializeField]
    private float invincibilityTime = .25f;

    // Start is called before the first frame update
    void Start()
    {
        healthController.AddOnEventTriggeredEvent(ProcessDamage);
    }

    public void ProcessDamage(HealthController.EventType eventType) {

        if(eventType != HealthController.EventType.DAMAGE_TAKEN) {
            return;
        }

        healthController.enabled = false;
        materialSwapper.SetAll(damageMaterial);
        
        shaker.Shake(time:invincibilityTime).OnComplete(() =>  {
            healthController.enabled = true;
            materialSwapper.RevertAll();
        });

    }
}
