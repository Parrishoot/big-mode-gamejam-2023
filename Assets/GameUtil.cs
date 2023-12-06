
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

    public static T GetRandomValueFromList<T>(List<T> list) {

        if(list.Count == 0) {
            return default;
        }

        return list[Random.Range(0, list.Count)];
    }

    public static void ClearChildren(Transform transform) {
        while(transform.childCount > 0) {
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
