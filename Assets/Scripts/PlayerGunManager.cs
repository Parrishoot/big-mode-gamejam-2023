using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunManager : GunManager
{
    [SerializeField]
    private Transform gunRotatorTransform;

    [SerializeField]
    private PlayerGunSupplier playerGunSupplier;

    protected override Vector3 GetShootingVector()
    {
        return gunRotatorTransform.forward;
    }

    protected override GunController GetGunController()
    {
        return playerGunSupplier.GetCurrentController().perspectiveGunController;
    }
}
