using UnityEngine;

public class BasicEventTrigger : MonoBehaviour
{
    public delegate void OnEventTriggered();

    protected OnEventTriggered onEventTriggered;

    public void AddOnEventTriggeredEvent(OnEventTriggered newOnEventTriggeredEvent) {
        onEventTriggered += newOnEventTriggeredEvent;
    } 

    public void RemoveOnEventTriggeredEvent(OnEventTriggered existingTriggerEvent) {
        onEventTriggered -= existingTriggerEvent;
    } 

    protected void TriggerEvent() {
        onEventTriggered?.Invoke();
    }
}
