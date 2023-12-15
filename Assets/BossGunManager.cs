using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossGunManager : MonoBehaviour
{
    [SerializeField]
    private BossSideGunManager leftSideGunManager;

    [SerializeField]
    private BossSideGunManager rightSideGunManager;

    [SerializeField]
    private BossSideGunManager topSideGunManager;

    [SerializeField]
    private BossSideGunManager bottomSideGunManager;

    private Dictionary<BossMeta.Side, BossSideGunManager> gunMap = new Dictionary<BossMeta.Side, BossSideGunManager>();

    // Start is called before the first frame update
    void Start()
    {
        gunMap[BossMeta.Side.LEFT] = leftSideGunManager;
        gunMap[BossMeta.Side.RIGHT] = rightSideGunManager;
        gunMap[BossMeta.Side.TOP] = topSideGunManager;
        gunMap[BossMeta.Side.BOTTOM] = bottomSideGunManager;
    }

    public void Shoot(BossMeta.Side gunSide, Vector3? direction = null, BossSideGunManager.GunSide[] gunParts = null) {
        Shoot(direction, new BossMeta.Side[] { gunSide }, gunParts);
        
    }

    public void Shoot(Vector3? direction = null, BossMeta.Side[] gunSides = null, BossSideGunManager.GunSide[] gunParts = null) {

        if(gunSides.Length == 0) {
            gunSides = new BossMeta.Side[]{ BossMeta.Side.LEFT, BossMeta.Side.RIGHT, BossMeta.Side.TOP, BossMeta.Side.BOTTOM }; 
        }

        foreach(BossMeta.Side gunSide in gunSides) {
            gunMap[gunSide].Shoot(direction, gunParts);
        }
        
    }
}
