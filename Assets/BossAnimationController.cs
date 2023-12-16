using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{

    List<NodeAnimatorMeta> enemyNodeAnimators = new List<NodeAnimatorMeta>();

    private const int INCLUDE = -1;

    private class NodeAnimatorMeta {

        public EnemySpawnAnimator Animator { get; set; }
        public BossNodeMetaController NodeInfo { get; set; }

        public NodeAnimatorMeta(EnemySpawnAnimator enemySpawnAnimator, BossNodeMetaController bossNodeMetaController) {
            this.Animator = enemySpawnAnimator;
            this.NodeInfo = bossNodeMetaController;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(BossNodeMetaController nodeInfo in GetComponentsInChildren<BossNodeMetaController>()) {
            enemyNodeAnimators.Add(new NodeAnimatorMeta(nodeInfo.gameObject.GetComponent<EnemySpawnAnimator>(), nodeInfo));
        }
    }

    public void Spawn(int x = INCLUDE, int y = INCLUDE, int z = INCLUDE) {
        foreach(NodeAnimatorMeta nodeAnimatorMeta in enemyNodeAnimators) {

            bool checkX = x == INCLUDE || nodeAnimatorMeta.NodeInfo.GetCoords().x == x;
            bool checkY = y == INCLUDE || nodeAnimatorMeta.NodeInfo.GetCoords().y == y;
            bool checkZ = z == INCLUDE || nodeAnimatorMeta.NodeInfo.GetCoords().z == z;

            if(checkX && checkY && checkZ) {
                nodeAnimatorMeta.Animator.PlaySpawnAnimation();
            }
        }
    }

    public void Despawn(int x = INCLUDE, int y = INCLUDE, int z = INCLUDE) {
        foreach(NodeAnimatorMeta nodeAnimatorMeta in enemyNodeAnimators) {

            bool checkX = x == INCLUDE || nodeAnimatorMeta.NodeInfo.GetCoords().x == x;
            bool checkY = y == INCLUDE || nodeAnimatorMeta.NodeInfo.GetCoords().y == y;
            bool checkZ = z == INCLUDE || nodeAnimatorMeta.NodeInfo.GetCoords().z == z;

            if(checkX && checkY && checkZ) {
                nodeAnimatorMeta.Animator.PlayDespawnAnimation();
            }
        }
    }
}
