using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FloorManager floorManager;

    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(true);
        floorManager.BuildFloor();
        loadingScreen.SetActive(false);
        playerObject.SetActive(true);
    }
}
