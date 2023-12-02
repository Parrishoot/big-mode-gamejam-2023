using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventDespawner : BasicEventIngester
{
    protected override void OnEventTrigger()
    {
        Destroy(gameObject);
    }
}
