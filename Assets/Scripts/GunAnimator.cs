using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunAnimator : MonoBehaviour
{

    [SerializeField]
    private float recoilAmount = 90f;

    [SerializeField]
    private GunController gunController;

    private void Start() {
        gunController.AddOnShotFiredEvent(PlayAnimation);
    }

    private void PlayAnimation() {

        DOTween.Sequence()
               .Append(transform.DOLocalRotate(new Vector3(-recoilAmount, 0, 0),  gunController.GetReloadTime() / 5, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic))
               .Append(transform.DOLocalRotate(new Vector3(recoilAmount * 1.1f, 0, 0), 3 * (gunController.GetReloadTime() / 5), RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic))
               .Append(transform.DOLocalRotate(new Vector3(-recoilAmount * .1f, 0, 0), 2 * (gunController.GetReloadTime() / 5), RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic))
               .OnComplete(() => {
                    transform.localEulerAngles = Vector3.zero;
               })
               .Play();
    }
}
