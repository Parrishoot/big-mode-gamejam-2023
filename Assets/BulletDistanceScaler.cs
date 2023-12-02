using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDistanceScaler : DistanceScaler
{
    protected override Transform GetOriginTransform()
    {
        return GameObject.FindGameObjectWithTag("GunSprite").transform;
    }
}
