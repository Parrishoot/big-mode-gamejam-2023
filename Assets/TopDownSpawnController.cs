using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownHiddenSpawnController : PerspectiveDependentController
{
    [SerializeField]
    private List<GameObject> gameObjects;

    public override void OnPerspectiveEnd()
    {
        
    }

    public override void OnPerspectiveStart()
    {
        foreach(GameObject gameObject in gameObjects) {
            gameObject.SetActive(false);
        }
    }

    public override void OnTransitionToEnd()
    {
        foreach(GameObject gameObject in gameObjects) {
            gameObject.SetActive(true);
        }
    }

    public override void OnTransitionToStart()
    {
        
    }
}
