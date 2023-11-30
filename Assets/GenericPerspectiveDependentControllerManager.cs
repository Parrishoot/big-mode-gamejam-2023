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
    void Awake()
    {
        CameraPerspectiveSwapper.Instance.AddOnPerspectiveSwitchEvent(OnSwap);
        CameraPerspectiveSwapper.Instance.AddOnPerspectiveTransitionBeginEvent(OnTransition);
    }

    private void OnSwap(PerspectiveMode fromPerspectiveMode, PerspectiveMode toPerspectiveMode) {
        currentController = toPerspectiveMode == PerspectiveMode.TOP_DOWN ? topDownController : fpsController;
        currentController.enabled = true;
    }

    private void OnTransition(PerspectiveMode fromPerspectiveMode, PerspectiveMode toPerspectiveMode) {
        
        if(fromPerspectiveMode == PerspectiveMode.TOP_DOWN) {
            topDownController.OnTransitionToEnd();
            fpsController.OnTransitionToStart();
        }
        else {
            topDownController.OnTransitionToStart();
            fpsController.OnTransitionToEnd();
        }

        if(currentController != null) {
            currentController.enabled = false;
        }
    }

    public T GetCurrentController() {
        return currentController;
    }

    private void OnDestroy() {
        CameraPerspectiveSwapper.Instance.RemoveOnPerspectiveSwitchEvent(OnSwap);
        CameraPerspectiveSwapper.Instance.RemoveOnPerspectiveTransitionBeginEvent(OnTransition);
    }
}
