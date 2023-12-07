using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderGunManager : EnemyGunManager
{
    private bool rotated = false;

    protected override Vector3 GetShootingVector()
    {
        rotated = !rotated;

        if(rotated) {
            return Vector3.forward;
        }

        float rotateAngle = (gunController.GetSpreadAngle() / gunController.GetBulletsPerShot()) / 2;
        return Quaternion.AngleAxis(rotateAngle, Vector3.up) * Vector3.forward;
    }
}
