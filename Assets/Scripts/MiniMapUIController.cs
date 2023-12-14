using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapUIController : MonoBehaviour
{
    [SerializeField]
    private float cellSize;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private RectTransform miniMapAnchorTransform;

    [SerializeField]
    private Color activeRoomBackgroundColor;

    [SerializeField]
    private Color inactiveUndiscoveredBackgroundRoomColor;

    [SerializeField]
    private Color inactiveDiscoveredBackgroundColor;

    [SerializeField]
    private float uiTransitionTime = .5f;

    private Dictionary<FloorBuilder.RoomDetails, List<RoomCellUIElements>> roomUIElements;

    private Vector2 baseAnchorPosition;

    private FloorBuilder.RoomDetails currentRoom;

    private void Awake() {
        baseAnchorPosition = rectTransform.anchoredPosition;    
    }

    private void Update() {
        miniMapAnchorTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x,
                                                         rectTransform.eulerAngles.y,
                                                         Camera.main.transform.eulerAngles.y);    
    }

    public void SetRoom(FloorBuilder.RoomDetails roomDetails) {
        
        rectTransform.DOAnchorPos(FindCenterOfRoom(roomDetails), uiTransitionTime).SetEase(Ease.InOutCubic);
        
        if(currentRoom != null) {
            SetRoomColor(currentRoom, currentRoom.RoomManager.Discovered() ? inactiveDiscoveredBackgroundColor: inactiveUndiscoveredBackgroundRoomColor);
        }

        currentRoom = roomDetails;
        SetRoomColor(currentRoom, activeRoomBackgroundColor);
    }

    private Vector2 FindCenterOfRoom(FloorBuilder.RoomDetails roomDetails) {

        Vector2 roomCenter = roomDetails.Origin + (roomDetails.Size) / 2;
        return baseAnchorPosition - new Vector2(roomCenter.x * cellSize, roomCenter.y * cellSize);

    }

    public void SetRoomUIElements( Dictionary<FloorBuilder.RoomDetails, List<RoomCellUIElements>> roomUIElements) {
        this.roomUIElements = roomUIElements;

        foreach(FloorBuilder.RoomDetails roomDetails in roomUIElements.Keys) {
            SetInitialColor(roomDetails);
        }
    }

    public void SetRoomColor(FloorBuilder.RoomDetails roomDetails, Color color) {
        foreach(RoomCellUIElements roomCellUIElements in roomUIElements[roomDetails]) {
            roomCellUIElements.Background.GetComponent<Image>().color = color;
        }
    }
    
    public void SetInitialColor(FloorBuilder.RoomDetails roomDetails) {
        SetRoomColor(roomDetails, inactiveUndiscoveredBackgroundRoomColor);
    }
}
