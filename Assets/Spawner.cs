using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private Vector3 bounds = Vector3.zero;

    [SerializeField]
    private GameObject spawnObject;

    public GameObject Spawn(bool parented=false, bool randomWithinBounds=true) {
        Vector3 randomSpawnLocation = randomWithinBounds ? GetRandomSpawnLocationWithinBounds() : transform.position;
        Debug.Log(randomSpawnLocation);
        return Instantiate(spawnObject, randomSpawnLocation, Quaternion.identity, parented ? transform : null);
    }

    public Vector3 GetRandomSpawnLocationWithinBounds() {

        Vector3 basePosition = transform.position;

        return new Vector3(Random.Range(basePosition.x - bounds.x, basePosition.x + bounds.x),
                           Random.Range(basePosition.y - bounds.y, basePosition.y + bounds.y),
                           Random.Range(basePosition.z - bounds.z, basePosition.z + bounds.z));

    }
}
