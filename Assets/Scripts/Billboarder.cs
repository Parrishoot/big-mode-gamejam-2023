using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarder : MonoBehaviour
{
    private PerspectiveMode currentPerspective = PerspectiveMode.TOP_DOWN;

    public void Start() {
        CameraPerspectiveSwapper.Instance.AddOnPerspectiveTransitionBeginEvent(SwapPerspectives);
    }

    public void SwapPerspectives(PerspectiveMode previousPerspective, PerspectiveMode newPerspective) {
        currentPerspective = newPerspective;
    }

    private void LateUpdate() {

        if(currentPerspective == PerspectiveMode.FPS) {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);     
        }
        else {
            transform.eulerAngles = Vector3.right * 90;
        }

    }
}
