using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;

public class CameraPerspectiveSwapper : Singleton<CameraPerspectiveSwapper>
{ 

    [SerializeField]
    private List<PerspectiveAnchor> perspectiveAnchors;  

    [Serializable]
    public class PerspectiveAnchor {

        [field:SerializeField]
        public PerspectiveMode PerspectiveMode { get; set; }

        [field:SerializeField] 
        public Transform Transform { get; set; }

        [field: SerializeReference]
        public CameraController CameraController { get; set; }

        [field: SerializeReference]
        public bool SmoothFollowAnchor { get; set; }

        public PerspectiveAnchor(PerspectiveMode perspectiveMode, Transform transform, CameraController cameraController, bool smoothFollowAnchor)
        {
            PerspectiveMode = perspectiveMode;
            Transform = transform;
            CameraController = cameraController;
            SmoothFollowAnchor = smoothFollowAnchor;

        }
    }

    [SerializeField]
    private float perspectiveTransitionTime = 1f;

    [SerializeField]
    private PerspectiveMode startingPerspective;

    [SerializeField]
    private Follower cameraFollower;

    [SerializeField]
    private CharacterMovementController playerMovementController;

    private int currentPerspectiveIndex = 0;

    private Sequence activeSequence;

    private CameraController currentCameraController;

    public delegate void OnPerspectiveSwitch(PerspectiveMode perspectiveMode);

    private OnPerspectiveSwitch onPerspectiveSwitch;

    void Start() {
        HardSetPerspective(startingPerspective);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SwapPerspectives();
        }    
    }

    public void HardSetPerspective(PerspectiveMode nextPerspective) {

        if(currentCameraController != null ) {
            currentCameraController.enabled = false;
        }

        PerspectiveAnchor anchor = GetAnchorForPerspective(nextPerspective);

        transform.position = anchor.Transform.position;
        transform.rotation = anchor.Transform.rotation;

        UpdateControllerValues(anchor);

        currentCameraController.enabled = true;
        cameraFollower.SetFollow(anchor.Transform, anchor.SmoothFollowAnchor);
    }

    public void SwapPerspectives() {
        SetPerspective((currentPerspectiveIndex + 1) % perspectiveAnchors.Count);
    }

    public void SetPerspective(int nextPerspectiveAnchorIndex) {
        SetPerspective(perspectiveAnchors[nextPerspectiveAnchorIndex]);
    }

    public void SetPerspective(PerspectiveMode nextPerspective) {
        PerspectiveAnchor nextPerspectiveAnchor = GetAnchorForPerspective(nextPerspective);
        SetPerspective(nextPerspectiveAnchor);
    }

    public void SetPerspective(PerspectiveAnchor nextPerspectiveAnchor) {

        if(activeSequence != null && activeSequence.IsPlaying()) {
            return;
        }

        if(currentCameraController != null) {
            currentCameraController.enabled = false;
        }

        playerMovementController.enabled = false;
        cameraFollower.enabled = false;

        activeSequence = DOTween.Sequence();
        activeSequence.Append(transform.DOMove(nextPerspectiveAnchor.Transform.position, perspectiveTransitionTime).SetEase(Ease.InOutCubic));
        activeSequence.Append(transform.DORotate(nextPerspectiveAnchor.Transform.rotation.eulerAngles, perspectiveTransitionTime).SetEase(Ease.InOutCubic));
        activeSequence.Play().OnComplete(() => {
            currentCameraController.enabled = true;
            cameraFollower.enabled = true;
            cameraFollower.SetFollow(nextPerspectiveAnchor.Transform, nextPerspectiveAnchor.SmoothFollowAnchor);
            playerMovementController.enabled = true;
        });

        UpdateControllerValues(nextPerspectiveAnchor);
    }

    private void UpdateControllerValues(PerspectiveAnchor anchor) {
        currentPerspectiveIndex = perspectiveAnchors.IndexOf(anchor);
        currentCameraController = anchor.CameraController;
        onPerspectiveSwitch?.Invoke(anchor.PerspectiveMode);
    }

    private PerspectiveAnchor GetAnchorForPerspective(PerspectiveMode perspectiveMode) {
        return perspectiveAnchors.Find(x => x.PerspectiveMode == perspectiveMode) ?? perspectiveAnchors[0];
    }

    public CameraController GetCurrentCameraController() {
        return currentCameraController;
    }

    public void AddOnPerspectiveSwitchEvent(OnPerspectiveSwitch onPerspectiveSwitchEvent) {
        onPerspectiveSwitch += onPerspectiveSwitchEvent;
    }

    public void RemoveOnPerspectiveSwitchEvent(OnPerspectiveSwitch onPerspectiveSwitchEvent) {
        onPerspectiveSwitch -= onPerspectiveSwitchEvent;
    }
}
