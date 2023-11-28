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

        public PerspectiveAnchor(PerspectiveMode perspectiveMode, Transform transform, CameraController cameraController)
        {
            this.PerspectiveMode = perspectiveMode;
            this.Transform = transform;
            this.CameraController = cameraController;
        }
    }

    [SerializeField]
    private float perspectiveTransitionTime = 1f;

    [SerializeField]
    private PerspectiveMode startingPerspective;

    private int currentPerspectiveIndex = 0;

    private Sequence activeSequence;

    private CameraController currentCameraController;

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

        currentPerspectiveIndex = perspectiveAnchors.IndexOf(anchor);
        currentCameraController = anchor.CameraController;
        currentCameraController.enabled = true;
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

        activeSequence = DOTween.Sequence();
        activeSequence.Append(transform.DOLocalMove(nextPerspectiveAnchor.Transform.localPosition, perspectiveTransitionTime).SetEase(Ease.InOutCubic));
        activeSequence.Append(transform.DORotate(nextPerspectiveAnchor.Transform.rotation.eulerAngles, perspectiveTransitionTime).SetEase(Ease.InOutCubic));
        activeSequence.Play().OnComplete(() => {
            currentCameraController.enabled = true;
        });

        currentPerspectiveIndex = perspectiveAnchors.IndexOf(nextPerspectiveAnchor);
        currentCameraController = nextPerspectiveAnchor.CameraController;
    }

    private PerspectiveAnchor GetAnchorForPerspective(PerspectiveMode perspectiveMode) {
        return perspectiveAnchors.Find(x => x.PerspectiveMode == perspectiveMode) ?? perspectiveAnchors[0];
    }

    public CameraController GetCurrentCameraController() {
        return currentCameraController;
    }
}
