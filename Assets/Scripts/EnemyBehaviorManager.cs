using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBehaviorManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyBehavior> enemyBehaviorOptions;

    [SerializeField]
    private EnemyBehavior waitBehavior;

    private EnemyBehavior previousBehavior;

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

                if(currentBehavior != waitBehavior) {
                    previousBehavior = currentBehavior; 
                }

                currentBehavior = behavior;
                behavior.enabled = true;
                return;
            }

            // Otherwise, pick one based on the priority weights
            for(int i = 0; i < behavior.GetPriority(); i++) {
                weightedBehaviorList.Add(behavior);
            }
        }

        EnemyBehavior nextSelection;

        do {
            nextSelection = GameUtil.GetRandomValueFromList(weightedBehaviorList);
        }
        while(previousBehavior != null && !previousBehavior.CanRepeat() && previousBehavior == nextSelection);

        if(currentBehavior != waitBehavior) {
            previousBehavior = currentBehavior; 
        }

        currentBehavior = nextSelection;
        currentBehavior.enabled = true;
    }
}
