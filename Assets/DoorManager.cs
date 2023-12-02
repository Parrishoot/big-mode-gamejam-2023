using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : ParameterizedEventIngester<RoomManager.RoomEvent>
{
    [SerializeField]
    private LayerMask neighborLayerMask;

    private DoorManager neighbor;

    protected override void Awake()
    {
        base.Awake();
        FindNeighbor();
    }

    protected void Start() {
        Close();
    }

    private void FindNeighbor() {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 5f, neighborLayerMask, QueryTriggerInteraction.Collide))
        {
            if(collider.gameObject == this.gameObject) {
                continue;
            }

            DoorManager neighborDoorManager = collider.gameObject.GetComponent<DoorManager>();

            if(neighborDoorManager) {
                neighbor = neighborDoorManager;
            }
        }
    }

    protected override void OnEventTrigger(RoomManager.RoomEvent eventParams)
    {
        switch(eventParams) {

            case RoomManager.RoomEvent.ROOM_ENTERED:
                Close();
                break;

            case RoomManager.RoomEvent.ROOM_COMPLETED:
                Open();
                break;
        }
    }

    private void Open() {
        if(neighbor != null) {
            gameObject.SetActive(false);
            neighbor.gameObject.SetActive(false);
        }
    }

    private void Close() {
        
        gameObject.SetActive(true);
        
        if(neighbor != null) {
            neighbor.gameObject.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 1, 0, .3f);
        Gizmos.DrawSphere(transform.position, 5f);
    }
}
