using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumBumBum : Singleton<BumBumBum>
{
    [SerializeField]
    private List<AudioSource> audioSources;

    private int currentBums = 0;

    public void PlayNextBum() {

        audioSources[currentBums].Play();

        currentBums = Mathf.Min(3, currentBums + 1);
    }
}
