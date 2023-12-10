using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shaker : MonoBehaviour
{
   public Tweener Shake(float strength = .3f, float time = .5f, int vibrato = 150) {
        return transform.DOShakePosition(time, strength, vibrato);
   }
}