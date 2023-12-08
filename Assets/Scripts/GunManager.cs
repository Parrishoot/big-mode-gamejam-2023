using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunManager: MonoBehaviour
{
    [SerializeField]
    protected GunController gunController;

    protected abstract Vector3 GetShootingVector();

    public virtual void Fire() {
        GetGunController().Fire(GetShootingVector());
    }

    protected virtual GunController GetGunController() {
        return gunController;
    }
}
