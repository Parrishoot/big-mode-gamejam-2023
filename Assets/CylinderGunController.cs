using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CylinderGunController : GunController
{
    public bool rotated = false;

    protected override Vector3 GetShootingVector()
    {
        rotated = !rotated;

        if(rotated) {
            return Vector3.forward;
        }

        float rotateAngle = (spreadAngle / bulletsPerShot) / 2;

        return Quaternion.AngleAxis(rotateAngle, Vector3.up) * Vector3.forward;
    }
}
