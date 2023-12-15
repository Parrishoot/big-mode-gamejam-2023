using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlowerSHot : BossRotatingShot
{
    protected override BossMeta.Side[] GetSides()
    {
        return new BossMeta.Side[] {
            BossMeta.Side.RIGHT,
            BossMeta.Side.BOTTOM,
            BossMeta.Side.LEFT,
            BossMeta.Side.TOP,
        };
    }

    protected override void Shoot()
    {
        StartCoroutine(ShootFlutter(GetSides(), simultaneous: false));
    }
}
