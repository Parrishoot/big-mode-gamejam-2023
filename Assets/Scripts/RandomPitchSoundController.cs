using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitchSoundController : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Vector2 pitchBounds = new Vector2(.8f, 1.2f);

    public void PlaySound() {

        audioSource.pitch = GameUtil.GetRandomValueFromBounds(pitchBounds);
        audioSource.Play();

    }
}
