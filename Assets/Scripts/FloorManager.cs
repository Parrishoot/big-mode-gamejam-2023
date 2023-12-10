using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : Singleton<FloorManager>
{
    [SerializeField]
    private FloorBuilder floorBuilder;

    [SerializeField]
    private MiniMapUIController miniMapUIController;

    private List<FloorBuilder.RoomDetails> roomDetails;

    public void BuildFloor() {
        roomDetails = floorBuilder.BuildFloor();
    }

    public void SetActiveRoom(RoomManager roomManager) {

        FloorBuilder.RoomDetails activeRoom = roomDetails.Find(i => i.RoomManager == roomManager);
        miniMapUIController.SetRoom(activeRoom);
    }
}
