using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : GunController
{
    protected override Vector3 GetShootingVector() {
        return CameraPerspectiveSwapper.Instance.GetCurrentCameraController().GetShootVector();
    }

    // Start is called before the first frame update
    void Start()
    {
        AddOnShotFiredEvent(() => {
            CameraPerspectiveSwapper.Instance.GetCurrentCameraController().GetShaker().Shake(time:.2f, strength:.2f, vibrato:50);
        });
    }
}
