using UnityEngine;

public class ParameterizedEventTrigger<T>: MonoBehaviour
{
    public delegate void OnEventTriggered(T eventParams);

    protected OnEventTriggered onEventTriggered;

    public void AddOnEventTriggeredEvent(OnEventTriggered newOnEventTriggeredEvent) {
        onEventTriggered += newOnEventTriggeredEvent;
    } 

    public void RemoveOnEventTriggeredEvent(OnEventTriggered existingTriggerEvent) {
        onEventTriggered -= existingTriggerEvent;
    }

    protected void TriggerEvent(T eventParams) {
        onEventTriggered?.Invoke(eventParams);
    }
}
