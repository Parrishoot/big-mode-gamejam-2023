using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBehaviorManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyBehavior> enemyBehaviorOptions;

    EnemyBehavior currentBehavior = null;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviorOptions = enemyBehaviorOptions.OrderByDescending(x => x.GetPriority()).ToList(); 
        ChooseBehavior();   
    }

    void Update() {
        if(currentBehavior != null && !currentBehavior.enabled) {
            ChooseBehavior();
        }
    }

    private void ChooseBehavior() {

        List<EnemyBehavior> weightedBehaviorList = new List<EnemyBehavior>();

        foreach(EnemyBehavior behavior in enemyBehaviorOptions) {
            
            // If we've met the condition where this behavior is required, set that 
            // behavior active
            if(behavior.BehaviorRequired()) {
                currentBehavior = behavior;
                behavior.enabled = true;
                return;
            }

            // Otherwise, pick one based on the priority weights
            for(int i = 0; i < behavior.GetPriority(); i++) {
                weightedBehaviorList.Add(behavior);
            }
        }

        currentBehavior = GameUtil.GetRandomValueFromList(weightedBehaviorList);
        currentBehavior.enabled = true;
    }
}
