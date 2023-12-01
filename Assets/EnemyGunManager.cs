using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunManager : MonoBehaviour
{
    [SerializeField]
    private GunController gunController;

    [SerializeField]
    private float fireTime = 1f;

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
            gunController.Fire();
            ResetTimer();
        });
    }
}
