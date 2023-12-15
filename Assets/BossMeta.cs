using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeta
{
    public enum Side {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }

    public enum Row {
        TOP,
        MIDDLE,
        BOTTOM
    }

    public static Side[] ALL_SIDES = new Side[] {
        Side.LEFT,
        Side.RIGHT,
        Side.TOP,
        Side.BOTTOM
    };
}
