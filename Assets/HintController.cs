using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HintController : MonoBehaviour
{
    [SerializeField]
    private float hintFadeSpeed = .5f;

    [SerializeField]
    private float hintWaitTime = 10f;

    [SerializeField]
    private TMP_Text hintText;

    private Timer hintTimer;

    void Start() {
        hintTimer = new Timer(hintWaitTime);
        hintTimer.AddOnTimerFinishedEvent(FadeInHint);
    }

    void Update() {
        hintTimer?.DecreaseTime(Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space)) {
            FadeOutHint();
        }
    }

    private void FadeInHint() {
        hintText.DOFade(1, hintFadeSpeed).SetEase(Ease.InOutCubic);
    }

    private void FadeOutHint() {
        hintText.DOFade(0, hintFadeSpeed)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => enabled = false);
    }
}
