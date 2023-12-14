using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class IntroTextController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text learnText;

    [SerializeField]
    private TMP_Text adaptText;

    [SerializeField]
    private TMP_Text rememberTheMissionText;

    [SerializeField]
    private Image background;

    [SerializeField]
    private float fadeTime = 2f;

    public void PlayIntro() {
        DOTween.Sequence()
               .PrependInterval(fadeTime)
               .AppendCallback(() => learnText.gameObject.SetActive(true))
               .Append(learnText.DOFade(0f, fadeTime).SetEase(Ease.InOutCubic))
               .AppendCallback(() => adaptText.gameObject.SetActive(true))
               .Append(adaptText.DOFade(0f, fadeTime).SetEase(Ease.InOutCubic))
               .AppendCallback(() => rememberTheMissionText.gameObject.SetActive(true))
               .Append(rememberTheMissionText.DOFade(0f, fadeTime * 2).SetEase(Ease.InOutCubic))
               .Join(background.DOFade(0f, fadeTime).SetEase(Ease.InOutCubic))
               .Play()
               .OnComplete(() => GameUtil.GetPlayerGameObject().GetComponent<PlayerInputController>().enabled = true);
    }
}
