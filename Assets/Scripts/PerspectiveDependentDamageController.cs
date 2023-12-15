using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveDependentDamageController : MonoBehaviour
{
    [SerializeField]
    private PerspectiveMode perspectiveMode;

    [SerializeField]
    private HitBox hitBox;

    // Start is called before the first frame update
    void Start()
    {
        CameraPerspectiveSwapper.Instance.AddOnPerspectiveSwitchEvent(CheckEnable);
    }

    private void CheckEnable(PerspectiveMode fromMode, PerspectiveMode toMode) {
        hitBox.SetSoftEnabled(perspectiveMode != toMode);
    }

    private void OnDestroy() {
        CameraPerspectiveSwapper.Instance.RemoveOnPerspectiveSwitchEvent(CheckEnable);
    }

    public void OnEnable() {
        CheckEnable(PerspectiveMode.TOP_DOWN, CameraPerspectiveSwapper.Instance.GetCurrentPerspectiveMode());
    }
}
