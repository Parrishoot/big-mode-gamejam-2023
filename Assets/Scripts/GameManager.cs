using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FloorManager floorManager;

    [SerializeField]
    private IntroTextController introTextController;

    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private bool playIntro = true;

    // Start is called before the first frame update
    void Start()
    {
        if(playIntro){
            introTextController.gameObject.SetActive(true);
            floorManager.BuildFloor();
            introTextController.PlayIntro();
        }
        else {
            floorManager.BuildFloor();
            playerObject.GetComponent<PlayerInputController>().enabled = true;
        }

    }
}
