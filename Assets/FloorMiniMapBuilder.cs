using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FloorMiniMapBuilder : MonoBehaviour
{
    [SerializeField] 
    private float cellSize = 30;
    
    [SerializeField]
    private GameObject roomUIPrefab;

    [SerializeField]
    private MiniMapUIController miniMapUIController;

    public void BuildUI(FloorBuilder.RoomDetails[,] roomDetails) {

        Dictionary<FloorBuilder.RoomDetails, List<RoomCellUIElements>> roomUICellElementMap = new Dictionary<FloorBuilder.RoomDetails, List<RoomCellUIElements>>();
    
        for(int x = 0; x < roomDetails.GetLength(0); x++) { 
            for(int y = 0; y < roomDetails.GetLength(1); y++) { 
                
                if(roomDetails[x,y] == null) {
                    continue;
                }
                
                GameObject roomCellUIObject = Instantiate(roomUIPrefab, transform.position, Quaternion.identity,transform);

                roomCellUIObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * cellSize, y * cellSize);
            
                FloorBuilder.RoomDetails currentRoomDetails = roomDetails[x, y];

                Vector2Int localCoordinates = (new Vector2Int(x, y) + Vector2Int.one) - currentRoomDetails.Origin;

                List<RoomWalls.WallType> walls = RoomResizer.GetWallsForRoomCell(localCoordinates, currentRoomDetails.Size);

                RoomCellUIElements uiElements = roomCellUIObject.GetComponent<RoomCellUIElements>();

                
                if(!walls.Contains(RoomWalls.WallType.LEFT)) {
                    uiElements.LeftWalls.SetActive(false);
                }

                if(!walls.Contains(RoomWalls.WallType.RIGHT)) {
                    uiElements.RightWalls.SetActive(false);
                }

                if(!walls.Contains(RoomWalls.WallType.TOP)) {
                    uiElements.TopWalls.SetActive(false);
                }

                if(!walls.Contains(RoomWalls.WallType.BOTTOM)) {
                    uiElements.BottomWalls.SetActive(false);
                }

                if(!roomUICellElementMap.ContainsKey(currentRoomDetails)) {
                    roomUICellElementMap[currentRoomDetails] = new List<RoomCellUIElements>();    
                }

                roomUICellElementMap[currentRoomDetails].Add(uiElements);
            }   
        }

        miniMapUIController.SetRoomUIElements(roomUICellElementMap);
    }
}
