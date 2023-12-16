using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Spawner : MonoBehaviour
{

    [SerializeField]
    private Vector3 bounds = Vector3.zero;

    [SerializeField]
    private List<GameObject> spawnObjects;

    private LayerMask wallMask;

    void Awake() {
        wallMask = LayerMask.GetMask("Wall");
    }

    public GameObject Spawn(bool parented=false, bool randomWithinBounds=true) {
        Vector3 randomSpawnLocation = randomWithinBounds ? GetRandomSpawnLocationWithinBounds() : transform.position;
        return Instantiate(GameUtil.GetRandomValueFromList(spawnObjects), randomSpawnLocation, Quaternion.identity, parented ? transform : null);
    }

    public Vector3 GetRandomSpawnLocationWithinBounds() {

        Vector3 basePosition = transform.position;

        Vector3 spawnLocation = Vector3.zero;

        bool canSpawn = false;

        while(!canSpawn) {

            canSpawn = true;

            spawnLocation = new Vector3(Random.Range(basePosition.x - (bounds.x / 2), basePosition.x + (bounds.x / 2)),
                                        Random.Range(basePosition.y - (bounds.y / 2), basePosition.y + (bounds.y / 2)),
                                        Random.Range(basePosition.z - (bounds.z / 2), basePosition.z + (bounds.z / 2)));

            if(bounds != Vector3.zero) {
                canSpawn = CheckWallAtSpawnLocation(spawnLocation);
            }
        }

        return spawnLocation;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, bounds + Vector3.one * .00001f);  
    }

    public void SetBounds(Vector3 newBounds) {
        bounds = newBounds;
    }

    public Vector2 GetBounds() {
        return bounds;
    }

    private bool CheckWallAtSpawnLocation(Vector3 spawnLocation) {
        return Physics.OverlapSphere(spawnLocation, 5f, wallMask, QueryTriggerInteraction.Collide).Length == 0;
    }
}
