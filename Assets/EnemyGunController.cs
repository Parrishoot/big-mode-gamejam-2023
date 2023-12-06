using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : GunController
{
    protected override Vector3 GetShootingVector()
    {
        Vector3 shootingVector = GameUtil.GetPlayerGameObject().transform.position - transform.position;
        return new Vector3(shootingVector.x, 0, shootingVector.z);
    }
}
