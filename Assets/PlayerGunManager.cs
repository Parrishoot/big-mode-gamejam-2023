using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunManager : MonoBehaviour
{
    [SerializeField]
    private GunController topDownGunController;

    [SerializeField]
    private GunController fpsGunController;

    private GunController currentGunController;


    // Start is called before the first frame update
    void Start()
    {
        CameraPerspectiveSwapper.Instance.AddOnPerspectiveSwitchEvent(SwapGuns);
    }

    public void SwapGuns(PerspectiveMode perspectiveMode) {

        if(currentGunController != null) {
            currentGunController.enabled = false;
        }

        currentGunController = perspectiveMode == PerspectiveMode.TOP_DOWN ? topDownGunController : fpsGunController;
        currentGunController.Reset();
    }

    public GunController GetCurrentGunController() {
        return currentGunController;
    }
}
