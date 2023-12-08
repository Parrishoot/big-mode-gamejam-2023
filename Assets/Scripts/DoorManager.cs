using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorManager : ParameterizedEventIngester<RoomManager.RoomEvent>
{
    [SerializeField]
    private LayerMask neighborLayerMask;

    private float doorAnimationTime = .3f;

    private DoorManager neighbor;

    public void SetRoomManager(RoomManager roomManager) {
        eventTrigger = roomManager;
    }

    private bool closed = true;

    protected override void Start() {
        base.Start();
        FindNeighbor();
        Close();
    }

    private void FindNeighbor() {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 5f, neighborLayerMask, QueryTriggerInteraction.Collide))
        {
            if(collider.gameObject == gameObject) {
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
        if(neighbor != null && closed) {
            PlayOpenAnimation();
            neighbor.PlayOpenAnimation();
        }
    }

    private void Close() {
        
        if(closed) {
            return;
        }

        PlayCloseAnimation();
        
        if(neighbor != null) {
            neighbor.PlayCloseAnimation();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 1, 0, .3f);
        Gizmos.DrawSphere(transform.position, 5f);
    }

    public void PlayOpenAnimation() {
        DOTween.Sequence()
               .Append(transform.DOMove(transform.position + (Vector3.down * 15), doorAnimationTime).SetEase(Ease.InCubic))
               .Play();
        closed = false;
    }

    public void PlayCloseAnimation() {
        DOTween.Sequence()
               .Append(transform.DOMove(transform.position + (Vector3.up * 15), doorAnimationTime).SetEase(Ease.InCubic))
               .Play();
        closed = true;
    }
}
