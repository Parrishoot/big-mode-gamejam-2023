using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HealthLossFeedbackController : MonoBehaviour
{
    [SerializeField]
    private HealthController playerHealthController;

    [SerializeField]
    private float shakeAmount = 10f;

    [SerializeField]
    private float shakeTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthController.AddOnEventTriggeredEvent(UpdateUI);    
    }

    private void UpdateUI(HealthController.EventType eventType) {
        
        playerHealthController.enabled = false;

        CameraPerspectiveSwapper.Instance
                                .GetCurrentCameraController()
                                .GetShaker()
                                .Shake(strength: shakeAmount, time: shakeTime)
                                .OnComplete(() => {
                                    playerHealthController.enabled = true;
                                });

    }
}
