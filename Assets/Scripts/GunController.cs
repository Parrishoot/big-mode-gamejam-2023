using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private float reloadTime = .3f;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    [Range(0, 360)]
    protected float spreadAngle = 0f;

    [SerializeField]
    protected int bulletsPerShot = 1;

    [SerializeField]
    private bool uniformSpread = true;

    [SerializeField]
    private float timeBetweenShots = 0f;

    private Timer reloadTimer;

    public delegate void OnShotFiredEvent();

    protected OnShotFiredEvent onShotFiredEvent;

    public void Fire(Vector3 shotDirection) {
        if(reloadTimer == null || reloadTimer.IsFinished()) {
            StartCoroutine(ShootGun(shotDirection));
        }
    }

    public virtual void Reset() {
        enabled = true;
    }

    protected virtual void Update() {
        reloadTimer?.DecreaseTime(Time.deltaTime);
    }

    public void AddOnShotFiredEvent(OnShotFiredEvent newOnShotFiredEvent) {
        onShotFiredEvent += newOnShotFiredEvent;
    }

    public void RemoveOnShotFiredEvent(OnShotFiredEvent newOnShotFiredEvent) {
        onShotFiredEvent -= newOnShotFiredEvent;
    }

    public float GetReloadTime() {
        return reloadTime;
    }

    private List<Vector3> GetUniformShotDirections(Vector3 shotDirection) {

        float spreadPerShot = spreadAngle / bulletsPerShot;
        float minAngle =  -(spreadAngle / 2);

        List<Vector3> directions = new List<Vector3>();

        Vector3 shootingVector = shotDirection.normalized;

        for(int i = 0; i < bulletsPerShot; i++) {
            directions.Add(Quaternion.AngleAxis(minAngle + (i * spreadPerShot), Vector3.up) * shootingVector);
        }

        return directions;
    }

    private List<Vector3> GetRandomShotDirections(Vector3 shotDirection) {

        float minAngle =  -(spreadAngle / 2);
        float maxAngle =  spreadAngle / 2;

        List<Vector3> directions = new List<Vector3>();

        Vector3 shootingVector = shotDirection.normalized;

        for(int i = 0; i < bulletsPerShot; i++) {
            directions.Add(Quaternion.AngleAxis(Random.Range(minAngle, maxAngle), Vector3.up) * shootingVector);
        }

        return directions;
    }

    public IEnumerator ShootGun(Vector3 shotDirection) {

        List<Vector3> directions = uniformSpread ? GetUniformShotDirections(shotDirection) : GetRandomShotDirections(shotDirection);
        
        onShotFiredEvent?.Invoke();

        for(int i = 0; i < bulletsPerShot; i++) {
            GameObject bulletObject = spawner.Spawn();
            bulletObject.GetComponent<BulletController>().Shoot(directions[i]);

            if(timeBetweenShots != 0) {
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }

        reloadTimer = new Timer(reloadTime);

        yield return null;
    }

    public float GetSpreadAngle() {
        return spreadAngle;
    }

    public int GetBulletsPerShot() {
        return bulletsPerShot;
    }
}
