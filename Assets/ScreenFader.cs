using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField]
    private Image screen;

    [SerializeField]
    private float fadeOutTime = 1f;

    [SerializeField]
    private bool fadeSoundIn = true;

    // Start is called before the first frame update
    void Start()
    {
        screen.DOFade(0, fadeOutTime).SetEase(Ease.InOutCubic);

        if(fadeSoundIn) {
            AudioListenerController.Instance?.TurnOff();
            AudioListenerController.Instance?.SetTarget(1, fadeOutTime);
        }
    }
}
