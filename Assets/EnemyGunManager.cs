using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunManager : MonoBehaviour
{
    [SerializeField]
    private GunController gunController;

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
            gunController.Fire();
            yield return new WaitForSeconds(timeBetweenShots);    
        }

        ResetTimer();
    }
}
