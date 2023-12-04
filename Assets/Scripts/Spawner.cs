using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Spawner : MonoBehaviour
{

    [SerializeField]
    
    private Vector3 bounds = Vector3.zero;

    [SerializeField]
    private GameObject spawnObject;

    public GameObject Spawn(bool parented=false, bool randomWithinBounds=true) {
        Vector3 randomSpawnLocation = randomWithinBounds ? GetRandomSpawnLocationWithinBounds() : transform.position;
        return Instantiate(spawnObject, randomSpawnLocation, Quaternion.identity, parented ? transform : null);
    }

    public Vector3 GetRandomSpawnLocationWithinBounds() {

        Vector3 basePosition = transform.position;

        return new Vector3(Random.Range(basePosition.x - (bounds.x / 2), basePosition.x + (bounds.x / 2)),
                           Random.Range(basePosition.y - (bounds.y / 2), basePosition.y + (bounds.y / 2)),
                           Random.Range(basePosition.z - (bounds.z / 2), basePosition.z + (bounds.z / 2)));

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, bounds + Vector3.one * .00001f);  
    }

    public void SetBounds(Vector3 newBounds) {
        bounds = newBounds;
    }
}
