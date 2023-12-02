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

    private int numberOfEnemiesRemaining;

    private bool initialized = false;

    private void KillEnemy() {

        numberOfEnemiesRemaining--;
        if(numberOfEnemiesRemaining <= 0) {
            TriggerEvent(RoomEvent.ROOM_COMPLETED);
        }

    }

    private void Init() {
        
        if(initialized) {
            return;
        }

        numberOfEnemiesRemaining = Random.Range(numberOfEnemiesToSpawn.x, numberOfEnemiesToSpawn.y);

        for(int i = 0; i < numberOfEnemiesRemaining; i++) {
            GameObject enemy = enemySpawner.Spawn();
            enemy.GetComponent<HealthController>().AddOnEventTriggeredEvent(KillEnemy);
        }

        initialized = true;

        TriggerEvent(RoomEvent.ROOM_ENTERED);
    }

    private void OnTriggerEnter(Collider other) {
        Init();
    }
}
