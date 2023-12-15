using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossSideGunManager : MonoBehaviour
{
    [SerializeField]
    private GunController leftGunController;

    [SerializeField]
    private GunController centerGunController;

    [SerializeField]
    private GunController rightGunController;

    public enum GunSide {
        LEFT,
        RIGHT,
        CENTER
    }

    Dictionary<GunSide, GunController> gunMap = new Dictionary<GunSide, GunController>();

    void Start() {
        gunMap[GunSide.LEFT] = leftGunController;
        gunMap[GunSide.RIGHT] = rightGunController;
        gunMap[GunSide.CENTER] = centerGunController;
    }

    public void Shoot(Vector3? direction = null, GunSide[] gunSides = null) {

        Vector3 shootDirection =  direction == null ? transform.right : (Vector3) direction;

        if(gunSides == null) {
            gunSides = new GunSide[]{ GunSide.LEFT, GunSide.RIGHT, GunSide.CENTER }; 
        }

        foreach(GunSide gunSide in gunSides) {
            gunMap[gunSide].Fire(shootDirection);
        }
    }
}
