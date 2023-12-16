using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : Singleton<DeathManager>
{
    [SerializeField]
    private MatrixBlender cameraMatrixBlender;

    [SerializeField]
    private float deathTweenTime = 1f;

    [SerializeField]
    private HealthController playerHealthController;

    [SerializeField]
    private Image background;

    [SerializeField]
    private MusicManager musicManager;

    [SerializeField]
    private List<TMP_Text> gameOverTexts;

    [SerializeField]
    private float textWaitTime = 1f;

    [SerializeField]
    private float textFadeTime = .5f;

    private CameraTweener cameraTweener;

    private Timer textWaitTimer;

    void Start() {
        cameraTweener = new CameraTweener(cameraMatrixBlender);
        playerHealthController.AddOnEventTriggeredEvent(CheckDeath);
    }

    public void Died() {
        cameraTweener.TweenToDeath(deathTweenTime);
        AudioListenerController.Instance?.SetTarget(0, deathTweenTime + textWaitTime + textFadeTime);
        background.DOFade(1, deathTweenTime)
                  .SetEase(Ease.InOutCubic)
                  .OnComplete(() => {
                    textWaitTimer = new Timer(textWaitTime);
                    textWaitTimer.AddOnTimerFinishedEvent(() => {
                        GameManager.Instance.SetRestartAvailable();
                        ShowText();
                    });
                  });
        musicManager.Stop();
        BoomManager.Instance.PlayBoom();
    }

    public void CheckDeath(HealthController.EventType eventType) {
        if(eventType == HealthController.EventType.DEATH) {
            Died();
        }
    }

    void Update() {
        textWaitTimer?.DecreaseTime(Time.deltaTime);
    }

    void ShowText() {
        foreach(TMP_Text text in gameOverTexts) {
            text.DOFade(1, textFadeTime).SetEase(Ease.InOutCubic);
        }
    }
}
