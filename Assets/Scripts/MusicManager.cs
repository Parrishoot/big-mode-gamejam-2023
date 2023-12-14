using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private FloorBuilder floorBuilder;

    [SerializeField]
    private AudioSource inCombatMusicSource;

    [SerializeField]
    private AudioSource outOfCombatMusicSource;

    [SerializeField]
    private float fadeTime;

    private float inCombatBaseVolume;

    private float outOfCombatBaseVolume;

    private bool musicStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        inCombatBaseVolume = inCombatMusicSource.volume;
        outOfCombatBaseVolume = outOfCombatMusicSource.volume;

        inCombatMusicSource.volume = 0;
        outOfCombatMusicSource.volume = 0;

        inCombatMusicSource.Play();
        outOfCombatMusicSource.Play();

        floorBuilder.AddOnEventTriggeredEvent(AddEventListeners);
    }

    void AddEventListeners() {
        foreach(RoomManager roomManager in FindObjectsOfType<RoomManager>()) {
            roomManager.AddOnEventTriggeredEvent(CheckRoomMusicSwap);
        }
    }

    public void CheckRoomMusicSwap(RoomManager.RoomEvent roomEvent) {

        switch(roomEvent) {

            case RoomManager.RoomEvent.ROOM_ENTERED:
                SwapToInCombatMusic();
                break;

            case RoomManager.RoomEvent.ROOM_COMPLETED:
                SwapToOutOfCombatMusic();
                break;

        }

    }

    public void SwapToInCombatMusic() {

        if(!musicStarted) {
            return;
        }

        inCombatMusicSource.DOFade(inCombatBaseVolume, fadeTime).SetEase(Ease.InOutCubic);
        outOfCombatMusicSource.DOFade(0, fadeTime).SetEase(Ease.InOutCubic);;
    }

    public void SwapToOutOfCombatMusic() {
        outOfCombatMusicSource.DOFade(inCombatBaseVolume, fadeTime).SetEase(Ease.InOutCubic);;
        inCombatMusicSource.DOFade(0, fadeTime).SetEase(Ease.InOutCubic);;

        musicStarted = true;
    }
}
