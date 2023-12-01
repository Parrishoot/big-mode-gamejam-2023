using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : GunController
{
    protected override Vector3 GetShootingVector()
    {
        return GameUtil.GetPlayerGameObject().transform.position - transform.position;
    }
}
