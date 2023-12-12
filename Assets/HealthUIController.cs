using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    [SerializeField]
    private HealthController playerHealthController;

    [SerializeField]
    private List<HealthCellUIController> healthCellUIControllers;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthController.AddOnEventTriggeredEvent(UpdateUI);    
    }

    private void UpdateUI(HealthController.EventType eventType) {
        healthCellUIControllers[playerHealthController.GetCurrentHealth()].LoseHealth();
    }
}
