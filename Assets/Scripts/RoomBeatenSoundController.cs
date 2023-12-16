using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBeatenSoundController : ParameterizedEventIngester<RoomManager.RoomEvent>
{

    protected override void OnEventTrigger(RoomManager.RoomEvent roomEvent)
    {
        if(roomEvent == RoomManager.RoomEvent.ROOM_COMPLETED) {
            BoomManager.Instance.PlayBoom();
        }
    }
}
