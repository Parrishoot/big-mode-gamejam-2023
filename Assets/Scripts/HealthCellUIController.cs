using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthCellUIController : MonoBehaviour
{
    [SerializeField]
    private Image healthCenterImage;

    [SerializeField]
    private float animationLength = 1f;

    [SerializeField]
    private float fallAmount = 10f;

    [SerializeField]
    private float rotateAmount = 10f;

    [SerializeField]
    private RectTransform rectTransform;

    public void LoseHealth() {
        DOTween.Sequence()
               .Append(transform.DOLocalMove(new Vector3(transform.localPosition.x - fallAmount,
                                                         transform.localPosition.y - fallAmount,
                                                         transform. localPosition.z),
                                            animationLength).SetEase(Ease.OutCubic))
               // .Join(transform.DOLocalRotate(transform.eulerAngles + (Vector3.forward * rotateAmount), animationLength).SetEase(Ease.OutCubic))
               .Join(healthCenterImage.DOFade(0f, animationLength).SetEase(Ease.OutCubic));
    }
}
