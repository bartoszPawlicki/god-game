using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using Assets.Scripts;

public class GameController : MonoBehaviour
{

    public Vector3 StartPosition;
    public GameObject[] Spawns;
    public bool TutorialEnable;

    public GameEndEventHandler OnGameEnd;

    // Use this for initialization
    private void Start ()
    {
        GameContener.Initialize();
        //_roundManager = gameObject.GetComponent<RoundManager>();
        //_roundManager.OnLastRoundEnded += _roundManager_OnLastRoundEnded;
        //_roundManager.OnNewRoundStarted += _roundManager_OnNewRoundStarted;

        _respawnManager = gameObject.GetComponent<RespawnManager>();
        _respawnManager.OnLastPlayerDied += _respawnManager_OnLastPlayerDied;

        _respawnTutorialManager = gameObject.GetComponent<RespawnTutorialManager>();
        _respawnTutorialManager.OnPlayerFall += _respawnTutorialManager_OnPlayerFall;

        _portalCollider = GameObject.Find("PortalCollider").GetComponent<PortalColliderController>();
        _portalCollider.OnPlayersPassed += _portalCollider_OnPlayersPassed; ;

        _cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        _cameraController.OnCameraStopMoving += _cameraController_OnCameraStopMoving;
        _cameraController.StartPosition = StartPosition + transform.position;

        _endCamera = GameObject.Find("End Camera");
        _endCamera.SetActive(false);

        if (TutorialEnable)
            initTutorial();
        else
            initGame();

    }

    private void _portalCollider_OnPlayersPassed(object sender, EventArgs e)
    {
        initGame();
    }

    private void initGame()
    {
        GameContener.MovePlayersToPosition(getRandomSpawn());
        GameContener.UnfreezePlayers();
        _cameraController.IsInitialMoving = false;
        _cameraController.SwitchCamera(false);
        _respawnManager.enabled = true;
        //_roundManager.enabled = true;
        _respawnTutorialManager.enabled = false;
        _respawnManager.InitRespawnManager();
    }

    private void initTutorial()
    {
        _cameraController.IsInitialMoving = false;
        _cameraController.SwitchCamera(true);
        _respawnManager.enabled = false;
        _respawnTutorialManager.enabled = true;
        //_roundManager.enabled = false;
        GameContener.MovePlayersToPosition(StartPosition);
        //here you may disable HP
    }

    private void _respawnTutorialManager_OnPlayerFall(object sender, GameObject player)
    {
        player.transform.localPosition = StartPosition;
    }

    private void _cameraController_OnCameraStopMoving(object sender, System.EventArgs e)
    {
        //_portalCollider.Collider.enabled = true;
        GameContener.UnfreezePlayers();
    }

    //private void _roundManager_OnLastRoundEnded(object sender, System.EventArgs e)
    //{
    //    GameContener.FreezePlayers();
    //    //TODO: Game End
    //}

    //private void _roundManager_OnNewRoundStarted(object sender, short roundNumber)
    //{
    //    GameContener.MovePlayersToPosition(getRandomSpawn());
    //}

    private void _respawnManager_OnLastPlayerDied(object sender, System.EventArgs e)
    {
        if (OnGameEnd != null)
            OnGameEnd.Invoke(this, Winner.God);
        GameEnd();
    }
        
    private void Update ()
    {
        if (GameContener.Players.Where(x => x.GetComponent<PlayerController>().HP > 0).ToList().Count <= 0)
        {
            if (OnGameEnd != null)
                OnGameEnd.Invoke(this, Winner.Players);
            GameEnd();
        }

        if (GameContener.GodPride.godPride <= 0)
        {
            if (OnGameEnd != null)
                OnGameEnd.Invoke(this, Winner.God);
            GameEnd();
        }
    }
    
    private void GameEnd()
    {
        GameContener.FreezePlayers();
        _endCamera.SetActive(true);
        _cameraController.gameObject.SetActive(false);

        enabled = false;

        //TODO: Game End Logic
    }
    //First reandom.Next is always 0 i don't know why
    private Vector3 getRandomSpawn()
    {
        random.Next(0, Spawns.Length);
        random.Next(0, Spawns.Length);
        random.Next(0, Spawns.Length);
        int number = random.Next(0, Spawns.Length);
        return Spawns[number].transform.localPosition;
    }

    private System.Random random = new System.Random();

    //private RoundManager _roundManager;
    private RespawnManager _respawnManager;
    private PortalColliderController _portalCollider;
    private CameraController _cameraController;
    private RespawnTutorialManager _respawnTutorialManager;
    private GameObject _endCamera;

}
