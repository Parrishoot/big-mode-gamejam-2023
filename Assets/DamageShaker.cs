using DG.Tweening;
using UnityEngine;

public class DamageInvincibilityController: MonoBehaviour
{

    [SerializeField]
    private HealthController healthController;

    [SerializeField]
    private GameObject parentGameObject;


    [SerializeField]
    private Shaker shaker;

    [SerializeField]
    private MaterialSwapper materialSwapper;

    [SerializeField]
    private Spawner gun;

    [SerializeField]
    private Material damageMaterial;

    [SerializeField]
    private float invincibilityTime = .25f;

    [SerializeField]
    private float deathAnimationTime = 1f;

    private Timer deathAnimationTimer;

    // Start is called before the first frame update
    void Start()
    {
        healthController.AddOnEventTriggeredEvent(ProcessHit);
    }

    void ProcessHit(HealthController.EventType eventType) {
        switch(eventType) {
            case HealthController.EventType.DAMAGE_TAKEN:
                ProcessDamage();
                break;
            case HealthController.EventType.DEATH:
                ProcessDeath();
                break;
        }
    }

    public void ProcessDamage() {

        healthController.enabled = false;
        materialSwapper.SetAll(damageMaterial);
        
        shaker.Shake(time:invincibilityTime).OnComplete(() =>  {
            healthController.enabled = true;
            materialSwapper.RevertAll();
        });
    }

    public void ProcessDeath() {

        healthController.enabled = false;
        gun.enabled = false;
        
        shaker.Shake(time:deathAnimationTime);
    }
}
