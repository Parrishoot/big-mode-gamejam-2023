using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerController : Singleton<AudioListenerController>
{
    private float targetVolume;

    private float previousVolume;

    private float startingVolume;

    private Timer timer;

    protected override void Awake() {

        if(AudioListenerController.Instance != null) {
            Destroy(gameObject);
            return;
        }

        base.Awake();

        DontDestroyOnLoad(this.gameObject);

        startingVolume = AudioListener.volume;
        previousVolume = startingVolume;
        this.targetVolume = startingVolume;

        AudioListener.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer != null && !timer.IsFinished()) {
            AudioListener.volume = Mathf.Lerp(previousVolume, targetVolume, timer.GetPercentageFinished());
            timer.DecreaseTime(Time.deltaTime);
        }
    }

    public void SetTarget(float targetVolume, float transitionTime) {
        timer = new Timer(transitionTime);
        this.previousVolume = AudioListener.volume;
        this.targetVolume = startingVolume * targetVolume;
    }

    public void Reset() {
        AudioListener.volume = 1;
        timer = null;
    }

    public void TurnOff() {
        AudioListener.volume = 0;
        timer = null;
    }
}
