using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParameterizedEventIngester<T> : MonoBehaviour
{
    [SerializeField]
    protected ParameterizedEventTrigger<T> eventTrigger;

    protected virtual void Awake() {
        eventTrigger.AddOnEventTriggeredEvent(OnEventTrigger);
    }

    protected virtual void OnDestroy() {
        eventTrigger.RemoveOnEventTriggeredEvent(OnEventTrigger);
    }

    abstract protected void OnEventTrigger(T eventParams);
}
