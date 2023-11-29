using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PerspectiveDependentController : MonoBehaviour
{
    public abstract void OnPerspectiveStart();

    public abstract void OnPerspectiveEnd();

    public abstract void OnTransitionToStart();

    public abstract void OnTransitionToEnd();

    private void OnEnable() {
        OnPerspectiveStart();
    }

    private void OnDisable() {
        OnPerspectiveEnd();
    }
}
