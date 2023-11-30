using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shaker : MonoBehaviour
{
   public void Shake(float strength = 1f, float time = .5f, int vibrato = 10) {
        transform.DOShakePosition(time, strength, vibrato);
   }
}