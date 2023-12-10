using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinShotGunManager : EnemyGunManager
{
    [SerializeField]
    private float sinAmp = 30f;

    [SerializeField]
    private float sinFrequency = 10f;

    protected override Vector3 GetShootingVector()
    {
        Vector3 directionOfPlayer = base.GetShootingVector();
        return Quaternion.AngleAxis(Mathf.Sin(Time.time * sinFrequency) * sinAmp, Vector3.up) * directionOfPlayer;
    }
}
