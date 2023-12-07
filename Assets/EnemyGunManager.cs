using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunManager : GunManager
{

    [SerializeField]
    private float fireTime = 1f;

    [SerializeField]
    private Vector2Int numberOfShots = new Vector2Int(1, 1);

    [SerializeField]
    private float timeBetweenShots = 0f;

    private Timer timer;

    void Start() {
        ResetTimer();
    }

    void Update() {
        timer?.DecreaseTime(Time.deltaTime);
    }

    private void ResetTimer() {
        timer = new Timer(GameUtil.GetRandomValueWithVariance(fireTime, .2f));
        timer.AddOnTimerFinishedEvent(() =>  {
            StartCoroutine(Shoot());
        });
    }

    private IEnumerator Shoot() {
        
        int shotsToFire = Random.Range(numberOfShots.x, numberOfShots.y + 1);

        for(int i = 0; i < shotsToFire; i++) {
            Fire();
            yield return new WaitForSeconds(timeBetweenShots);    
        }

        ResetTimer();
    }

    protected override Vector3 GetShootingVector()
    {
        Vector3 shotVector = GameUtil.GetPlayerGameObject().transform.position - gunController.transform.position;
        return new Vector3(shotVector.x, 0, shotVector.z);
    }
}
