using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomManager : Singleton<BoomManager>
{
    [SerializeField] 
    private AudioSource boomAudio; 

    public void PlayBoom() {
        boomAudio.Play();
    }
}
