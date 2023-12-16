using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : ParameterizedEventTrigger<RoomManager.RoomEvent>
{

    public enum RoomEvent {
        ROOM_ENTERED,
        ROOM_COMPLETED
    }

    [SerializeField]
    private Spawner enemySpawner;

    [SerializeField]
    private Vector2Int numberOfEnemiesToSpawn = new Vector2Int(2, 4);

    [SerializeField]
    private BoxCollider roomCollider;

    [SerializeField]
    private List<HealthController> startingEnemies;

    [SerializeField]
    private bool spawnRandomizedEnemies = true;

    [SerializeField]
    private RoomResizer roomResizer;

    private int numberOfEnemiesRemaining;

    private bool initialized = false;

    private void KillEnemy(HealthController.EventType healthControllerEventType) {

        if(healthControllerEventType != HealthController.EventType.DEATH) {
            return;
        }

        numberOfEnemiesRemaining--;
        if(numberOfEnemiesRemaining <= 0) {
            TriggerEvent(RoomEvent.ROOM_COMPLETED);
        }

    }

    private void Start() {
        roomCollider.enabled = true;

        numberOfEnemiesRemaining = 0;

        foreach(HealthController healthController in startingEnemies) {
            healthController.AddOnEventTriggeredEvent(KillEnemy);
        }
    }

    private void Init() {

        FloorManager.Instance.SetActiveRoom(this);

        if(initialized) {
            return;
        }

        if(spawnRandomizedEnemies) {

            int roomSize = roomResizer.GetSize().x * roomResizer.GetSize().y;

            numberOfEnemiesRemaining += Random.Range(Mathf.Max(1, numberOfEnemiesToSpawn.x * roomSize / 2), Mathf.Min(8, numberOfEnemiesToSpawn.y * roomSize / 2));

            if(numberOfEnemiesToSpawn.x == numberOfEnemiesToSpawn.y) {
                numberOfEnemiesRemaining = numberOfEnemiesToSpawn.x;
            }

            for(int i = 0; i < numberOfEnemiesRemaining; i++) {
                GameObject enemy = enemySpawner.Spawn();
                enemy.GetComponent<HealthController>().AddOnEventTriggeredEvent(KillEnemy);
            }
        }

        initialized = true;

        TriggerEvent(RoomEvent.ROOM_ENTERED);
    }

    private void OnTriggerEnter(Collider other) {
        Init();
    }

    public bool Discovered() {
        return initialized;
    }
}
