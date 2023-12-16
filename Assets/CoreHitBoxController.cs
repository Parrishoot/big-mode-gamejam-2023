using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoreHitBoxController : MonoBehaviour
{
    [SerializeField]
    private HitBox hitBox;

    [SerializeField]
    private Shaker cameraShaker;

    [SerializeField]
    private Shaker coreShaker;

    [SerializeField]
    private AudioSource audioSource;

    private int hitTimes = 0;

    private bool canBeHit = true;

    private Timer invincibilityTimer;

    // Start is called before the first frame update
    void Start()
    {
        hitBox.AddOnHitBoxEnteredEvent(OnHit);
    }

    public void OnHit(int damage) {

        if(!canBeHit) {
            return;
        }

        BumBumBum.Instance.PlayNextBum();

        hitTimes++;
        canBeHit = false;

        cameraShaker.Shake(strength: .03f * hitTimes, fadeOut: false).SetLoops(-1);
        coreShaker.Shake(strength: .1f * hitTimes, fadeOut: false).SetLoops(-1);
        
        audioSource.Play();

        invincibilityTimer = new Timer(.5f);
        invincibilityTimer.AddOnTimerFinishedEvent(() => canBeHit = true);

        if(hitTimes == 3) {
            CreditsManager.Instance.RollCredits();
        }
    }

    public void Update() {
        invincibilityTimer?.DecreaseTime(Time.deltaTime);
    }
}
