using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundController : RandomPitchSoundController
{
    [SerializeField]
    private List<GunController> gunControllers;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GunController gunController in gunControllers) {
            gunController.AddOnShotFiredEvent(PlaySound);
        }    
    }
}
