using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private FloorManager floorManager;

    [SerializeField]
    private IntroTextController introTextController;

    [SerializeField]
    private OutroController outroController;

    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private bool playIntro = true;

    [SerializeField]
    private Shaker cameraShaker;

    [SerializeField]
    private StartGameController restartGameController;

    // Start is called before the first frame update
    void Start()
    {
        AudioListenerController.Instance?.SetTarget(1, .25f);

        Cursor.lockState = CursorLockMode.Locked;

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

    public void GameBeaten() {
        cameraShaker.Shake(fadeOut: false).SetLoops(-1);
        playerObject.GetComponent<PlayerInputController>().enabled = false;
        outroController.PlayOutro();
    }

    public void SetRestartAvailable() {
        restartGameController.enabled = true;
    }
}
