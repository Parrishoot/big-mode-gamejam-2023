using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBeatenSoundController : ParameterizedEventIngester<RoomManager.RoomEvent>
{
    [SerializeField]
    private AudioSource audioSource;

    protected override void OnEventTrigger(RoomManager.RoomEvent roomEvent)
    {
        if(roomEvent != RoomManager.RoomEvent.ROOM_COMPLETED) {
            return;
        }

        audioSource.Play();
    }
}
