using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWalls : MonoBehaviour
{
    public enum WallType {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }

    [field:SerializeField]
    public GameObject LeftWalls { get; set; }

    [field:SerializeField]
    public GameObject RightWalls { get; set; }

    [field:SerializeField]
    public GameObject BottomWalls { get; set; }

    [field:SerializeField]
    public GameObject TopWalls { get; set; }
}
