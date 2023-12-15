using System;
using System.Collections;
using UnityEngine;

public class BossGunBehavior : EnemyBehavior
{
    [SerializeField]
    public BossGunManager bossGunManager;

    [SerializeField]
    public float timeBetweenShots = .2f;
    
    [SerializeField]
    public float shootTimePerSide = 1f;

    private BossMeta.Side[] gunSides;

    private bool isShooting = false;

    private delegate void ShotAction(BossMeta.Side gunSide, Vector3? direction, int shotNumber, int totalShots);


    // Start is called before the first frame update
    protected virtual void Start()
    {
        gunSides = new BossMeta.Side[] {
            BossMeta.Side.BOTTOM,
            BossMeta.Side.RIGHT,
            BossMeta.Side.TOP,
            BossMeta.Side.LEFT,
        };
    }

    public IEnumerator ShootCheckerboard(BossMeta.Side[] gunSides, bool simultaneous = true, Vector3? direction = null, Action onComplete = null) {
        yield return simultaneous ? ShootSimultaneous(gunSides, ShootCheckerboardPattern, direction, onComplete) : ShootSequential(gunSides, ShootCheckerboardPattern, direction, onComplete);
    }

    public IEnumerator ShootFlutter(BossMeta.Side[] gunSides, bool simultaneous = true, Vector3? direction = null,  Action onComplete = null) {
        yield return simultaneous ? ShootSimultaneous(gunSides, ShootFlutterPattern, direction, onComplete) : ShootSequential(gunSides, ShootFlutterPattern, direction, onComplete);
    }

    public IEnumerator Shoot(BossMeta.Side[] gunSides, bool simultaneous = true, Vector3? direction = null,  Action onComplete = null) {
        yield return simultaneous ? ShootSimultaneous(gunSides, ShootRegular, direction, onComplete) : ShootSequential(gunSides, ShootRegular, direction, onComplete);
    }

    private void ShootFlutterPattern(BossMeta.Side gunSide, Vector3? direction, int shotNumber, int totalShots) {

        BossSideGunManager.GunSide[] gunSides = {
            BossSideGunManager.GunSide.LEFT,
            BossSideGunManager.GunSide.CENTER,
            BossSideGunManager.GunSide.RIGHT
        };

        bossGunManager.Shoot(gunSide, direction, gunParts: new BossSideGunManager.GunSide[] { gunSides[shotNumber % 3] });
    }

    private void ShootRegular(BossMeta.Side gunSide, Vector3? direction, int shotNumber, int totalShots) {

        bossGunManager.Shoot(gunSide, direction);
    }

    private void ShootCheckerboardPattern(BossMeta.Side gunSide, Vector3? direction, int shotNumber, int totalShots) {

        BossSideGunManager.GunSide[] gunParts = null;

        if(shotNumber % 2 == 0) {
            gunParts = new BossSideGunManager.GunSide[] {
                BossSideGunManager.GunSide.LEFT,
                BossSideGunManager.GunSide.RIGHT
            };
        }
        else {
            gunParts = new BossSideGunManager.GunSide[] {
                BossSideGunManager.GunSide.CENTER
            };
        }

        BossMeta.Side[] gunSides = new BossMeta.Side[] { gunSide };

        bossGunManager.Shoot(gunSide, direction, gunParts: gunParts);
    }

    private IEnumerator ShootSequential(BossMeta.Side[] gunSides, ShotAction shotAction, Vector3? direction = null, Action onComplete = null) {

        isShooting = true;

        int numberOfShots = (int) Mathf.Floor(shootTimePerSide / timeBetweenShots);

        foreach(BossMeta.Side gunSide in gunSides) {
            for(int i = 0; i < numberOfShots; i++) {
                shotAction.Invoke(gunSide, direction, i, numberOfShots);
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }

        onComplete?.Invoke();

        isShooting = false;

        yield return null;

    }

    private IEnumerator ShootSimultaneous(BossMeta.Side[] gunSides, ShotAction shotAction, Vector3? direction = null, Action onComplete = null) {

        isShooting = true;

        int numberOfShots = (int) Mathf.Floor(shootTimePerSide / timeBetweenShots);

        for(int i = 0; i < numberOfShots; i++) {
            
            foreach(BossMeta.Side gunSide in gunSides) {
                shotAction.Invoke(gunSide, direction, i, numberOfShots);
            }
        
            yield return new WaitForSeconds(timeBetweenShots);
        }

        onComplete?.Invoke();

        isShooting = false;

        yield return null;
    }

    public bool IsShooting() {
        return isShooting;
    }
}


