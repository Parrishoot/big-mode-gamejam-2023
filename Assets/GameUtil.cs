using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtil
{
    public static GameObject GetPlayerGameObject() {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public static float GetRandomValueWithVariance(float baseValue, float variance) {
        return Random.Range(baseValue - (baseValue * variance), baseValue + baseValue * variance);        
    }
}
