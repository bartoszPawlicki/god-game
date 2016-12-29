using UnityEngine;
using System;
using System.Collections;

public class GameController : MonoBehaviour
{

    public Vector3 StartPosition;
    public GameObject[] Spawns;

    // Use this for initialization
    private void Start ()
    {
        GameContener.Initialize();
        _roundManager = gameObject.GetComponent<RoundManager>();
        _roundManager.OnLastRoundEnded += _roundManager_OnLastRoundEnded;
        _roundManager.OnNewRoundStarted += _roundManager_OnNewRoundStarted;

        _timeManager = gameObject.GetComponent<TimeManager>();
        _timeManager.OnTimeElapsed += _timeManager_OnTimeElapsed;

        _respawnManager = gameObject.GetComponent<RespawnManager>();
        _respawnManager.OnLastPlayerFall += _respawnManager_OnLastPlayerFall;

        _portalCollider = GameObject.Find("PortalCollider").GetComponent<PortalColliderController>();
        _portalCollider.OnPlayersPassed += GameController_OnPlayersPassed;

        _cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        _cameraController.OnCameraStopMoving += _cameraController_OnCameraStopMoving;
        _cameraController.StartPosition = StartPosition + transform.position;
    }

    private void _cameraController_OnCameraStopMoving(object sender, System.EventArgs e)
    {
        _timeManager.enabled = true;
        _portalCollider.Collider.enabled = true;
        _respawnManager.enabled = true;
        GameContener.UnfreezePlayers();
    }

    private void GameController_OnPlayersPassed(object sender, System.EventArgs e)
    {
        _portalCollider.Collider.enabled = false;
        
        GameContener.MovePlayersToPosition(getRandomSpawn());
    }

    private void _roundManager_OnLastRoundEnded(object sender, System.EventArgs e)
    {
        GameContener.FreezePlayers();
        //TODO: Game End
    }

    private void _roundManager_OnNewRoundStarted(object sender, short roundNumber)
    {
        //New round starts when camera stop moving, check _cameraController_OnCameraStopMoving method
        _timeManager.enabled = false;
        GameContener.MovePlayersToPosition(StartPosition);
        GameContener.FreezePlayers();
        _timeManager.TimeLeft = _timeManager.TotalGameTime;
        _cameraController.IsInitialMoving = true;
    }

    private void _respawnManager_OnLastPlayerFall(object sender, System.EventArgs e)
    {
        _roundManager.RoundEnd();
    }

    private void _timeManager_OnTimeElapsed(object sender, System.EventArgs e)
    {
        _timeManager.enabled = false;
        _roundManager.RoundEnd();
    }
    
    private void Update ()
    {
	
	}
    
    private Vector3 getRandomSpawn()
    {
        int number = random.Next(0, Spawns.Length);
        return Spawns[number].transform.localPosition;
    }

    private System.Random random = new System.Random();

    private RoundManager _roundManager;
    private TimeManager _timeManager;
    private RespawnManager _respawnManager;
    private PortalColliderController _portalCollider;
    private CameraController _cameraController;
}
