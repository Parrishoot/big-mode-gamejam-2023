using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{

    [SerializeField]
    private GunController gunController;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float recoilAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gunController.AddOnShotFiredEvent(AnimateCamera);    
    }

    private void AnimateCamera() {

        Vector3 originalPosition = cameraTransform.localPosition;

        DOTween.Sequence()
               .Append(cameraTransform.DOLocalMove(originalPosition - (Vector3.back * recoilAmount), gunController.GetReloadTime() / 4))
               .Append(cameraTransform.DOLocalMove(originalPosition, gunController.GetReloadTime() / 4))
               .Play();
    }
}
