using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorManager : ParameterizedEventIngester<RoomManager.RoomEvent>
{
    [SerializeField]
    private LayerMask neighborLayerMask;

    [SerializeField]
    private Material doorMaterial;

    [SerializeField]
    private Material wallMaterial;

    [SerializeField]
    private GameObject rightSide;

    [SerializeField]
    private GameObject leftSide;

    [SerializeField]
    private float doorOpenAmount = .5f;

    [SerializeField]
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
            if(collider.gameObject == leftSide.gameObject || collider.gameObject == rightSide.gameObject) {
                continue;
            }

            DoorManager neighborDoorManager = collider.gameObject.transform.parent.GetComponent<DoorManager>();

            if(neighborDoorManager) {
                neighbor = neighborDoorManager;
            }
        }

        SetDoorVsWall();
    }

    private void SetDoorVsWall() {
        if(neighbor == null) {
            leftSide.GetComponent<MeshRenderer>().material = wallMaterial;
            rightSide.GetComponent<MeshRenderer>().material = wallMaterial;
            leftSide.transform.localScale = Vector3.one;
            rightSide.transform.localScale = Vector3.one;
        }
        else {
            leftSide.GetComponent<MeshRenderer>().material = doorMaterial;
            rightSide.GetComponent<MeshRenderer>().material = doorMaterial;
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
               .Append(leftSide.transform.DOScaleX(1 - (doorOpenAmount), doorAnimationTime).SetEase(Ease.InOutCubic))
               .Join(rightSide.transform.DOScaleX(1 - (doorOpenAmount), doorAnimationTime).SetEase(Ease.InOutCubic))
               .Play();
        closed = false;
    }

    public void PlayCloseAnimation() {
        DOTween.Sequence()
               .Append(leftSide.transform.DOScaleX(1, doorAnimationTime).SetEase(Ease.InOutCubic))
               .Join(rightSide.transform.DOScaleX(1, doorAnimationTime).SetEase(Ease.InOutCubic))
               .Play();
        closed = true;
    }
}
