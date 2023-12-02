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
               .Append(transform.DOLocalRotate(new Vector3(-recoilAmount, 0, 0), gunController.GetReloadTime() / 4, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic))
               .Append(transform.DOLocalRotate(new Vector3(360 + recoilAmount, 0, 0), 3 * (gunController.GetReloadTime() / 4), RotateMode.LocalAxisAdd).SetEase(Ease.InOutCubic))
               .OnComplete(() => {
                    transform.localEulerAngles = Vector3.zero;
               })
               .Play();
    }
}
