using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutroController : MonoBehaviour
{
    [SerializeField]
    private AudioListener audioListener;

    [SerializeField]
    private Image loadingScreen;

    [SerializeField]
    private float fadeTime = 5f;

    private float startingVolume;

    private Timer fadeOutTimer;

    public void PlayOutro() {
        startingVolume = AudioListener.volume;
        fadeOutTimer = new Timer(fadeTime);
        fadeOutTimer.AddOnTimerFinishedEvent(() => SceneManager.LoadScene("Ending"));
    }

    void Update() { 
        if(fadeOutTimer != null) {

            Color newColor = loadingScreen.color;
            newColor.a = fadeOutTimer.GetPercentageFinished();
            loadingScreen.color = newColor;

            AudioListener.volume = fadeOutTimer.GetPercentageRemaining() * startingVolume;
        }
        
        fadeOutTimer?.DecreaseTime(Time.deltaTime);
    }
}
