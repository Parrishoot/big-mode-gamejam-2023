using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class RoomResizer : MonoBehaviour
{
    [SerializeField]
    private GameObject roomBasePrefab;

    [SerializeField]
    private GameObject roomParent;

    [SerializeField]
    private RoomManager roomManager;

    [SerializeField]
    private BoxCollider roomTrigger;

    [SerializeField]
    private Spawner enemySpawner;
    
    [SerializeField]
    private NavMeshSurface surface;

    [SerializeField]
    private Vector2Int roomSize = new Vector2Int(1, 1);

    public void Resize() {

        ClearExistingRoomPieces();

        CreateRoomPieces();

        ResizeSurface();
    }

    public void Resize(Vector2Int newRoomSize) {
        roomSize = newRoomSize;
        Resize();
    }

    private void ClearExistingRoomPieces() {
        GameUtil.ClearChildren(roomParent.transform);
    }

    private void CreateRoomPiece(int xCoord, int yCoord) {

        GameObject roomPiece = Instantiate(roomBasePrefab, roomParent.transform);
        roomPiece.transform.localPosition = new Vector3((xCoord - 1) * RoomMeta.BASE_ROOM_SIZE, 0, (yCoord - 1) * RoomMeta.BASE_ROOM_SIZE);

        RoomWalls roomWalls = roomPiece.GetComponent<RoomWalls>();

        List<RoomWalls.WallType> walls = GetWallsForRoomCell(new Vector2Int(xCoord, yCoord), roomSize);

        if(!walls.Contains(RoomWalls.WallType.LEFT)) {
            DestroyImmediate(roomWalls.LeftWalls);
        }

        if(!walls.Contains(RoomWalls.WallType.RIGHT)) {
            DestroyImmediate(roomWalls.RightWalls);
        }

        if(!walls.Contains(RoomWalls.WallType.TOP)) {
            DestroyImmediate(roomWalls.TopWalls);
        }

        if(!walls.Contains(RoomWalls.WallType.BOTTOM)) {
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

    private void ResizeSurface() {

        float width = (roomSize.x * RoomMeta.BASE_ROOM_SIZE) - RoomMeta.BASE_WALL_SIZE;
        float height = (roomSize.y * RoomMeta.BASE_ROOM_SIZE) - RoomMeta.BASE_WALL_SIZE;

        Vector3 center = new Vector3(width, 0, height) / 2;

        width *= .95f;
        height *= .95f;

        roomTrigger.size = new Vector3(width, 10f, height);
        roomTrigger.center = center;

        enemySpawner.SetBounds(new Vector3(width, 0, height));
        enemySpawner.transform.parent.gameObject.transform.localPosition = center;

        surface.BuildNavMesh();
    }

    public Spawner GetEnemySpawner() {
        return enemySpawner;
    }
    
    public static List<RoomWalls.WallType> GetWallsForRoomCell(Vector2Int cellCoords, Vector2Int roomSize) {

        List<RoomWalls.WallType> walls = new List<RoomWalls.WallType>(new RoomWalls.WallType[]{
            RoomWalls.WallType.LEFT,
            RoomWalls.WallType.RIGHT,
            RoomWalls.WallType.BOTTOM,
            RoomWalls.WallType.TOP
        });

        if(cellCoords.x != 1) {
            walls.Remove(RoomWalls.WallType.LEFT);
        }

        if(cellCoords.x != roomSize.x) {
            walls.Remove(RoomWalls.WallType.RIGHT);
        }

        if(cellCoords.y != 1) {
            walls.Remove(RoomWalls.WallType.BOTTOM);
        }

        if(cellCoords.y != roomSize.y) {
            walls.Remove(RoomWalls.WallType.TOP);
        }

        return walls;
    }
}
