using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsManager : Singleton<CreditsManager>
{
    [SerializeField]
    private PlayerInputController playerInputController;

    [SerializeField]
    private Image blackScreen;

    [SerializeField]
    private Image whiteScreen;

    [SerializeField]
    private TMP_Text michaelParrishGameText;

    [SerializeField]
    private TMP_Text madeForBigModeText;

    [SerializeField]
    private TMP_Text thanksForPlayingText;

    [SerializeField]
    private float creditsFadeTime = 3f;

    [SerializeField]
    private float creditsStayTime = 3f;

    [SerializeField]
    private float creditsPauseTime = 1f;

    [SerializeField]
    private AudioSource ambientSound;

    public void RollCredits() {

        ambientSound.Stop();

        playerInputController.enabled = false;

        blackScreen.gameObject.SetActive(true);

        DOTween.Sequence()
               .AppendInterval(creditsPauseTime * 3)
               .Append(michaelParrishGameText.DOFade(1, creditsFadeTime).SetEase(Ease.InOutCubic))
               .AppendInterval(creditsStayTime)
               .Append(michaelParrishGameText.DOFade(0, creditsFadeTime).SetEase(Ease.InOutCubic))
               .AppendInterval(creditsPauseTime)
               .Append(madeForBigModeText.DOFade(1, creditsFadeTime).SetEase(Ease.InOutCubic))
               .AppendInterval(creditsStayTime)
               .Append(madeForBigModeText.DOFade(0, creditsFadeTime).SetEase(Ease.InOutCubic))
               .AppendInterval(creditsPauseTime)
               .Append(thanksForPlayingText.DOFade(1, creditsFadeTime).SetEase(Ease.InOutCubic))
               .AppendInterval(creditsStayTime)
               .Append(whiteScreen.DOFade(1, creditsPauseTime * 3).SetEase(Ease.InOutCubic))
               .Play()
               .OnComplete(() => SceneManager.LoadScene("Title"));
    }
}
