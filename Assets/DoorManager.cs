using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : ParameterizedEventIngester<RoomManager.RoomEvent>
{
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }
    protected override void OnEventTrigger(RoomManager.RoomEvent eventParams)
    {
        Debug.Log("Ingesting!");

        switch(eventParams) {

            case RoomManager.RoomEvent.ROOM_ENTERED:
                 gameObject.SetActive(true);
                break;

            case RoomManager.RoomEvent.ROOM_COMPLETED:
                 gameObject.SetActive(false);
                break;
        }
    }
}
