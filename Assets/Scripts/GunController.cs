using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunController : MonoBehaviour
{
    [SerializeField]
    private float reloadTime = .3f;

    [SerializeField]
    private Spawner spawner;

    private Timer reloadTimer;

    public delegate void OnShotFiredEvent();

    protected OnShotFiredEvent onShotFiredEvent;

    public void Fire() {
        if(reloadTimer == null || reloadTimer.IsFinished()) {
            
            GameObject bulletObject = spawner.Spawn();
            bulletObject.GetComponent<BulletController>().Shoot(GetShootingVector());
            
            reloadTimer = new Timer(reloadTime);

            onShotFiredEvent?.Invoke();
        }
    }

    public virtual void Reset() {
        enabled = true;
    }

    protected virtual void Update() {
        reloadTimer?.DecreaseTime(Time.deltaTime);
    }

    protected abstract Vector3 GetShootingVector();

    public void AddOnShotFiredEvent(OnShotFiredEvent newOnShotFiredEvent) {
        onShotFiredEvent += newOnShotFiredEvent;
    }

    public void RemoveOnShotFiredEvent(OnShotFiredEvent newOnShotFiredEvent) {
        onShotFiredEvent -= newOnShotFiredEvent;
    }

    public float GetReloadTime() {
        return reloadTime;
    }
}
