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
    private TransformFollower cameraFollower;

// 
    [SerializeField]
    private CharacterMovementController playerMovementController;

    [SerializeField]
    private MatrixBlender matrixBlender;

    private CameraTweener cameraTweener;

    private int currentPerspectiveIndex = 0;

    private Sequence activeSequence;

    private CameraController currentCameraController;

    public delegate void PerspectiveSwitchEvent(PerspectiveMode fromPerspectiveMode, PerspectiveMode toPerspectiveMode);

    private PerspectiveSwitchEvent onPerspectiveTransitionBegin;

    private PerspectiveSwitchEvent onPerspectiveSwitched;

    void Start() {
        cameraTweener = new CameraTweener(matrixBlender);
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

        cameraTweener.TweenToPerspectiveMode(anchor.PerspectiveMode, 0f);

        currentCameraController.enabled = true;
        cameraFollower.SetFollow(anchor.Transform, anchor.SmoothFollowAnchor);

        onPerspectiveSwitched?.Invoke(nextPerspective == PerspectiveMode.TOP_DOWN ? PerspectiveMode.FPS : PerspectiveMode.TOP_DOWN, nextPerspective);
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

        PerspectiveMode currentPerspectiveMode = perspectiveAnchors[currentPerspectiveIndex].PerspectiveMode;
        PerspectiveMode nextPerspectiveMode = nextPerspectiveAnchor.PerspectiveMode;

        playerMovementController.enabled = false;
        cameraFollower.enabled = false;

        onPerspectiveTransitionBegin?.Invoke(currentPerspectiveMode, nextPerspectiveMode);

        cameraTweener.TweenToPerspectiveMode(nextPerspectiveMode, GetAnimationTime());

        activeSequence = DOTween.Sequence();
        activeSequence.Append(transform.DOMove(nextPerspectiveAnchor.Transform.position, perspectiveTransitionTime).SetEase(Ease.InOutCubic));
        activeSequence.Append(transform.DORotate(nextPerspectiveAnchor.Transform.rotation.eulerAngles, perspectiveTransitionTime).SetEase(Ease.InOutCubic));
        activeSequence.Play().OnComplete(() => {
            
            currentCameraController.enabled = true;
            cameraFollower.enabled = true;
            cameraFollower.SetFollow(nextPerspectiveAnchor.Transform, nextPerspectiveAnchor.SmoothFollowAnchor);
            playerMovementController.enabled = true;

            onPerspectiveSwitched?.Invoke(currentPerspectiveMode, nextPerspectiveMode);
        });

        UpdateControllerValues(nextPerspectiveAnchor);
    }

    private void UpdateControllerValues(PerspectiveAnchor anchor) {
        currentPerspectiveIndex = perspectiveAnchors.IndexOf(anchor);
        currentCameraController = anchor.CameraController;
    }

    private PerspectiveAnchor GetAnchorForPerspective(PerspectiveMode perspectiveMode) {
        return perspectiveAnchors.Find(x => x.PerspectiveMode == perspectiveMode) ?? perspectiveAnchors[0];
    }

    public CameraController GetCurrentCameraController() {
        return currentCameraController;
    }

    public void AddOnPerspectiveSwitchEvent(PerspectiveSwitchEvent onPerspectiveSwitchEvent) {
        onPerspectiveSwitched += onPerspectiveSwitchEvent;
    }

    public void RemoveOnPerspectiveSwitchEvent(PerspectiveSwitchEvent onPerspectiveSwitchEvent) {
        onPerspectiveSwitched -= onPerspectiveSwitchEvent;
    }

    public void AddOnPerspectiveTransitionBeginEvent(PerspectiveSwitchEvent onPerspectiveTransitionBeginEvent) {
        onPerspectiveTransitionBegin += onPerspectiveTransitionBeginEvent;
    }

    public void RemoveOnPerspectiveTransitionBeginEvent(PerspectiveSwitchEvent onPerspectiveSwitchEvent) {
        onPerspectiveTransitionBegin -= onPerspectiveSwitchEvent;
    }

    public float GetAnimationTime() {
        return perspectiveTransitionTime * 2;
    }

    public PerspectiveMode GetCurrentPerspectiveMode() {
        return perspectiveAnchors[currentPerspectiveIndex].PerspectiveMode;
    }
}
