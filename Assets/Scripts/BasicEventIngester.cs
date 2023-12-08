using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEventIngester: MonoBehaviour
{
    [SerializeField]
    protected BasicEventTrigger eventTrigger;

    private void Awake() {
        eventTrigger.AddOnEventTriggeredEvent(OnEventTrigger);
    }

    private void OnDestroy() {
        eventTrigger.RemoveOnEventTriggeredEvent(OnEventTrigger);
    }

    abstract protected void OnEventTrigger();
}
