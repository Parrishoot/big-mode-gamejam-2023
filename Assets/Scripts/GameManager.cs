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

    // Start is called before the first frame update
    void Start()
    {
        introTextController.gameObject.SetActive(true);
        floorManager.BuildFloor();
        introTextController.PlayIntro();
        playerObject.SetActive(true);
    }
}
