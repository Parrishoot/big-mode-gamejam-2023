using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FloorBuilder : MonoBehaviour
{
    [SerializeField]
    private int numberOfRooms = 10;

    [SerializeField]
    private Transform roomTransform;

    [SerializeField]
    private int maxRoomSize = 2;

    [SerializeField]
    private GameObject roomPrefab;

    [SerializeField]
    private FloorMiniMapBuilder miniMapBuilder;

    [SerializeField]
    private List<GameObject> startRooms;

    private RoomDetails[,] roomCoordinateDetails;

    private List<RoomDetails> roomDetails;

    private Queue<RoomDetails> roomQueue;

    private Queue<GameObject> startRoomQueue;

    private Queue<GameObject> endRoomQueue;

    private List<RoomResizer> roomResizers;

    private int numberOfCreatedRooms = 0; 

    private Vector2Int FAIL = new Vector2Int(-1, -1);

    public class RoomDetails {

        public Vector2Int Size { get; set; }

        public List<DoorDetails> AvailableDoors { get; set; }

        public Vector2Int Origin { get; set; }

        public RoomManager RoomManager { get; set; }

        public RoomDetails(RoomManager roomManager, Vector2Int size, Vector2Int origin) {
            RoomManager = roomManager;
            Size = size;
            Origin = origin;

            InitDoors();
        }

        private void InitDoors() {

            AvailableDoors = new List<DoorDetails>();

            for(int x = 0; x < Size.x; x++) {
                for(int y = 0; y < Size.y; y++) {
                    Vector2Int doorLocation = new Vector2Int(x ,y);

                    if(x == 0) {
                        AvailableDoors.Add(new DoorDetails(this, doorLocation, DoorDetails.DOOR_DIRECTION.LEFT));
                    }

                    if(x == Size.x - 1) {
                        AvailableDoors.Add(new DoorDetails(this, doorLocation, DoorDetails.DOOR_DIRECTION.RIGHT));
                    }

                    if(y == 0) {
                        AvailableDoors.Add(new DoorDetails(this, doorLocation, DoorDetails.DOOR_DIRECTION.BOTTOM));
                    }

                    if(y == Size.y - 1) {
                        AvailableDoors.Add(new DoorDetails(this, doorLocation, DoorDetails.DOOR_DIRECTION.TOP));
                    }
                }
            }
        }

        public DoorDetails GetRandomDoor() {
            return GameUtil.GetRandomValueFromList(AvailableDoors);
        }
    }

    public class DoorDetails {

        public Vector2Int Origin { get; set; }

        public enum DOOR_DIRECTION {
            LEFT,
            RIGHT,
            BOTTOM,
            TOP
        }

        public DOOR_DIRECTION DoorDirection { get; set; }

        public RoomDetails Room { get; set; }

        public DoorDetails(RoomDetails roomDetails, Vector2Int origin, DOOR_DIRECTION doorDirection) {
            Room = roomDetails;
            Origin = origin;
            DoorDirection = doorDirection;
        }

        public Vector2Int GetGlobalOrigin() {
            return Origin + Room.Origin;
        }

        public Vector2Int GetNeighborOrigin() {
            
            switch(DoorDirection) {
                case DOOR_DIRECTION.LEFT:
                    return GetGlobalOrigin() + Vector2Int.left;
                case DOOR_DIRECTION.RIGHT:
                    return GetGlobalOrigin() + Vector2Int.right;
                case DOOR_DIRECTION.TOP:
                    return GetGlobalOrigin() + Vector2Int.up;
                case DOOR_DIRECTION.BOTTOM:
                    return GetGlobalOrigin() + Vector2Int.down;
            }

            // Impossible
            return Vector2Int.left;
        }
    }

    public List<RoomDetails> BuildFloor() {

        while(!TryCreate()) {

        }

        miniMapBuilder.BuildUI(roomCoordinateDetails);

        return roomDetails;
    }

    private bool TryCreate() {
        
        roomCoordinateDetails = new RoomDetails[numberOfRooms * maxRoomSize * 2, numberOfRooms * maxRoomSize * 2];
        roomQueue = new Queue<RoomDetails>();
        startRoomQueue = new Queue<GameObject>(startRooms);
        roomResizers = new List<RoomResizer>();
        roomDetails = new List<RoomDetails>();

        GameUtil.ClearChildren(roomTransform);

        numberOfCreatedRooms = 0;

        do {

            Vector2Int nextRoomSize;
            GameObject roomOverride = null;

            if(startRoomQueue.Count > 0) {
                roomOverride = startRoomQueue.Dequeue();
                nextRoomSize = roomOverride.GetComponent<RoomResizer>().GetSize();
            }
            else {
                nextRoomSize = GetNextRoomSize();
            }
        
            Vector2Int nextRoomOrigin = GetNextRoomOrigin(nextRoomSize);

            if(nextRoomOrigin == FAIL) {
                return false;
            }

            CreateRoom(nextRoomOrigin, nextRoomSize, roomOverride);

        } while(numberOfCreatedRooms < numberOfRooms);

        CenterRooms();

        return true;
    }

    public Vector2Int GetNextRoomOrigin(Vector2Int newRoomSize) {
        if(roomQueue.Count == 0) {
            // For the first room, return the center
            return GetCenter();
        }
        else {
            while(roomQueue.Count > 0) {
                
                // Find the next room to check
                RoomDetails nextRoom = roomQueue.Dequeue();
                
                // While there are still doors to check
                while(nextRoom.AvailableDoors.Count > 0) {
                    DoorDetails doorToCheck = nextRoom.GetRandomDoor();
                    Vector2Int newRoomOrigin = CanFitNewRoom(doorToCheck, newRoomSize);
                    if(newRoomOrigin != FAIL) {
                        return newRoomOrigin;
                    }
                    else {
                        nextRoom.AvailableDoors.Remove(doorToCheck);
                    }
                }
            }
        }

        // This should be rare...
        Debug.Log("UH OH IT HAPPENED!");
        return FAIL;
    }

    private Vector2Int GetNextRoomSize() {
        return new Vector2Int(Random.Range(1, maxRoomSize + 1),
                              Random.Range(1, maxRoomSize + 1));
    }

    private void CreateRoom(Vector2Int roomOrigin, Vector2Int roomSize, GameObject roomObject) {


        Vector3 spawnPosition = new Vector3(roomOrigin.x * RoomMeta.BASE_ROOM_SIZE, 0, roomOrigin.y * RoomMeta.BASE_ROOM_SIZE);
        roomObject = Instantiate(roomObject == null ? roomPrefab : roomObject, spawnPosition, Quaternion.identity, roomTransform);

        
        RoomResizer roomResizer = roomObject.GetComponent<RoomResizer>();
        RoomManager roomManager = roomObject.GetComponent<RoomManager>();

        roomResizer.Resize(roomSize);
        roomResizers.Add(roomResizer);

        RoomDetails newRoomDetails = new RoomDetails(roomManager, roomSize, roomOrigin);

        for(int x = roomOrigin.x; x < roomOrigin.x + roomSize.x; x++) {
            for(int y = roomOrigin.y; y < roomOrigin.y + roomSize.y; y++) {
                roomCoordinateDetails[x, y] = newRoomDetails;
            }
        }

        roomQueue.Enqueue(newRoomDetails);
        roomDetails.Add(newRoomDetails);
        numberOfCreatedRooms++;
    }

    private Vector2Int CanFitNewRoom(DoorDetails doorToCheck, Vector2Int newRoomSize) {

        Vector2Int newRoomOrigin = doorToCheck.GetNeighborOrigin();

        int startingX = newRoomOrigin.x;
        int endingX = newRoomOrigin.x + newRoomSize.x;

        if(doorToCheck.DoorDirection == DoorDetails.DOOR_DIRECTION.LEFT) {
            startingX -= (newRoomSize.x - 1);
            endingX = (newRoomOrigin.x + 1);
        }

        int startingY = newRoomOrigin.y;
        int endingY = newRoomOrigin.y + newRoomSize.y;

        if(doorToCheck.DoorDirection == DoorDetails.DOOR_DIRECTION.BOTTOM) {
            startingY -= (newRoomSize.y - 1);
            endingY = (newRoomOrigin.y + 1);
        }

        for(int x = startingX; x < endingX; x++) {
            for(int y = startingY; y < endingY; y++) {
                if(roomCoordinateDetails[x,y] != null) {
                    return FAIL;
                }
            }
        }

        return new Vector2Int(startingX, startingY);
    }

    public List<RoomResizer> GetRoomResizers() {
        return roomResizers;
    }

    private Vector2Int GetCenter() {
        return new Vector2Int(roomCoordinateDetails.GetLength(0) / 2, roomCoordinateDetails.GetLength(1) / 2);
    }

    private void CenterRooms() {
        roomTransform.position -= new Vector3(GetCenter().x * RoomMeta.BASE_ROOM_SIZE + (RoomMeta.BASE_ROOM_SIZE / 2),
                                              0,
                                              GetCenter().y * RoomMeta.BASE_ROOM_SIZE + (RoomMeta.BASE_ROOM_SIZE / 2));
    }
}
