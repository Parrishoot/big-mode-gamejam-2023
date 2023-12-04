using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomResizer : MonoBehaviour
{
    [SerializeField]
    private GameObject roomBasePrefab;

    [SerializeField]
    private float baseRoomSize = 60f;

    [SerializeField]
    private float baseWallSize = 5f;

    [SerializeField]
    private GameObject roomParent;

    [SerializeField]
    private RoomManager roomManager;

    [SerializeField]
    private BoxCollider roomTrigger;

    [SerializeField]
    private Spawner enemySpawner;

    [SerializeField]
    private Vector2Int roomSize = new Vector2Int(1, 1);

    public void Resize() {

        ClearExistingRoomPieces();

        CreateRoomPieces();

        ResizeSpawnerAndTrigger();
    }

    private void ClearExistingRoomPieces() {
        while(roomParent.transform.childCount > 0) {
            DestroyImmediate(roomParent.transform.GetChild(0).gameObject);
        }
    }

    private void CreateRoomPiece(int xCoord, int yCoord) {

        GameObject roomPiece = Instantiate(roomBasePrefab, roomParent.transform);
        roomPiece.transform.localPosition = new Vector3((xCoord - 1) * baseRoomSize, 0, (yCoord - 1) * baseRoomSize);

        RoomWalls roomWalls = roomPiece.GetComponent<RoomWalls>();

        if(xCoord != 1) {
            DestroyImmediate(roomWalls.LeftWalls);
        }

        if(xCoord != roomSize.x) {
            DestroyImmediate(roomWalls.RightWalls);
        }

        if(yCoord != 1) {
            DestroyImmediate(roomWalls.TopWalls);
        }

        if(yCoord != roomSize.y) {
            DestroyImmediate(roomWalls.BottomWalls);
        }

        foreach(DoorManager doorManager in roomPiece.GetComponentsInChildren<DoorManager>()) {
            doorManager.SetRoomManager(roomManager);
        }
    }

    private void CreateRoomPieces() {
        for(int i = 1; i <= roomSize.x; i++) {
            for(int j = 1; j <= roomSize.y; j++) {
                CreateRoomPiece(i, j);
            }
        }
    }

    private void ResizeSpawnerAndTrigger() {

        float width = (roomSize.x * baseRoomSize) - baseWallSize;
        float height = (roomSize.y * baseRoomSize) - baseWallSize;

        Debug.Log(width + ", " + height);

        Vector3 center = new Vector3(width, 0, height) / 2;

        width *= .95f;
        height *= .95f;

        roomTrigger.size = new Vector3(width, 10f, height);
        roomTrigger.center = center;

        enemySpawner.SetBounds(new Vector3(width, 0, height));
        enemySpawner.gameObject.transform.localPosition = center;
    }
    
}
