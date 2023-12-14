using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameController : MonoBehaviour
{
    [SerializeField]
    private Image fadeScreen;

    [SerializeField]
    private float fadeTime;

    void Update() {

        if(Input.GetKeyDown(KeyCode.Space)) {
            fadeScreen.DOFade(1, fadeTime).OnComplete(() => SceneManager.LoadScene("Game"));
        }
    }
}
