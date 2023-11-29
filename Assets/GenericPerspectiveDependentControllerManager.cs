using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPerspectiveDependentControllerManager<T> : MonoBehaviour where T: PerspectiveDependentController
{
    [SerializeField]
    private T topDownController;

    [SerializeField]
    private T fpsController;

    private T currentController;

    // Start is called before the first frame update
    void Start()
    {
        CameraPerspectiveSwapper.Instance.AddOnPerspectiveSwitchEvent(OnSwap);
    }

    private void OnSwap(PerspectiveMode perspectiveMode) {

        if(currentController != null) {
            currentController.enabled = false;
        }

        currentController = perspectiveMode == PerspectiveMode.TOP_DOWN ? topDownController : fpsController;
        currentController.enabled = true;
    }

    public T GetCurrentController() {
        return currentController;
    }
}
