using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private float reloadTime = .3f;

    [SerializeField]
    private Spawner spawner;

    private Timer reloadTimer;

    public void Fire() {
        if(reloadTimer == null || reloadTimer.IsFinished()) {
            GameObject bulletObject = spawner.Spawn();
            bulletObject.GetComponent<BulletController>().Shoot(CameraPerspectiveSwapper.Instance.GetCurrentCameraController().GetShootVector());
            reloadTimer = new Timer(reloadTime);
        }
    }

    public virtual void Reset() {
        enabled = true;
    }

    protected virtual void Update() {
        reloadTimer?.DecreaseTime(Time.deltaTime);
    }
}
