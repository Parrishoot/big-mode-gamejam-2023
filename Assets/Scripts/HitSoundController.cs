using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundController : ParameterizedEventIngester<HealthController.EventType>
{
    [SerializeField]
    private AudioSource hitAudioSource;

    [SerializeField]
    private AudioSource deathAudioSource;

    protected override void OnEventTrigger(HealthController.EventType healthEvent)
    {
        switch(healthEvent) {

            case HealthController.EventType.DAMAGE_TAKEN:
                hitAudioSource.Play();
                break;

            case HealthController.EventType.DEATH:
                deathAudioSource.Play();
                break;

        }
    }
}
