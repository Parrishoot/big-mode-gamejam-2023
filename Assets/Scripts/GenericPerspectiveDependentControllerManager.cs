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

        topDownController.enabled = false;
        fpsController.enabled = false;
    }

    void Start() {
        SetControllerForPerspectiveActive(CameraPerspectiveSwapper.Instance.GetCurrentPerspectiveMode());
    }


    private void OnSwap(PerspectiveMode fromPerspectiveMode, PerspectiveMode toPerspectiveMode) {
        SetControllerForPerspectiveActive(toPerspectiveMode);
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

    public void SetControllerForPerspectiveActive(PerspectiveMode perspectiveMode) {
        currentController = GetControllerForPerspective(perspectiveMode);
        currentController.enabled = true;
    }

    public T GetCurrentController() {
        return currentController;
    }

    private void OnDestroy() {
        CameraPerspectiveSwapper.Instance.RemoveOnPerspectiveSwitchEvent(OnSwap);
        CameraPerspectiveSwapper.Instance.RemoveOnPerspectiveTransitionBeginEvent(OnTransition);
    }

    protected T GetControllerForPerspective(PerspectiveMode perspectiveMode) {
        return perspectiveMode == PerspectiveMode.TOP_DOWN ? topDownController : fpsController;
    }
}
