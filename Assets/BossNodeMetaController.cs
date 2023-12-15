using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNodeMetaController : MonoBehaviour
{
    private Vector3Int coords;

    void Awake() {

        coords = Vector3Int.zero;

        // Horribly hacky but the deadline is in 2 days
        if(gameObject.transform.parent.name == "BottomRow") {
            coords.y = 0;
        }
        else if(gameObject.transform.parent.name == "MiddleRow") {
            coords.y = 1;
        }
        else if(gameObject.transform.parent.name == "TopRow") {
            coords.y = 2;
        }

        string[] parts = gameObject.name.Split('_');

        int x = Int32.Parse(parts[0]);
        int z = Int32.Parse(parts[1]);

        coords.x = x;
        coords.z = z;
    }

    public bool InRow(BossMeta.Row row) {
        if(row == BossMeta.Row.BOTTOM) {
            return coords.y == 0;
        }
        else if(row == BossMeta.Row.MIDDLE) {
            return coords.y == 1;
        }
        else {
            return coords.y == 2;
        }
    }

    public bool OnSide(BossMeta.Side side) {
        if(side == BossMeta.Side.LEFT) {
            return coords.x == 0;
        }
        else if(side == BossMeta.Side.RIGHT) {
            return coords.x == 2;
        }
        else if(side == BossMeta.Side.TOP) {
            return coords.z == 2;
        }
        else {
            return coords.z == 0;
        }
    }

    public Vector3Int GetCoords() {
        return coords;
    }
}
