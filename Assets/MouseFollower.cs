using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : Follower
{
    protected override Vector3 GetTargetPosition()
    {
        return GameUtil.GetMouseScreenPosition();
    }
}
