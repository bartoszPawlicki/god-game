﻿using UnityEngine;
using System;
using System.Collections;

public class GameController : MonoBehaviour
{

    public Vector3 StartPosition;
    public GameObject[] Spawns;
    public bool TutorialEnable;

    // Use this for initialization
    private void Start ()
    {
        GameContener.Initialize();
        _roundManager = gameObject.GetComponent<RoundManager>();
        _roundManager.OnLastRoundEnded += _roundManager_OnLastRoundEnded;
        _roundManager.OnNewRoundStarted += _roundManager_OnNewRoundStarted;

        _respawnManager = gameObject.GetComponent<RespawnManager>();
        _respawnManager.OnLastPlayerFall += _respawnManager_OnLastPlayerFall;

        _respawnTutorialManager = gameObject.GetComponent<RespawnTutorialManager>();
        _respawnTutorialManager.OnPlayerFall += _respawnTutorialManager_OnPlayerFall;

        _portalCollider = GameObject.Find("PortalCollider").GetComponent<PortalColliderController>();
        _portalCollider.OnPlayersPassed += GameController_OnPlayersPassed;

        _cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        _cameraController.OnCameraStopMoving += _cameraController_OnCameraStopMoving;
        _cameraController.StartPosition = StartPosition + transform.position;

        if (TutorialEnable)
            initTutorial();
        else
            initGame();

    }

    private void initGame()
    {
        GameContener.UnfreezePlayers();
        _cameraController.IsInitialMoving = false;
        _respawnManager.enabled = true;
        _roundManager.enabled = true;
        _respawnTutorialManager.enabled = false;
    }

    private void initTutorial()
    {
        _cameraController.IsInitialMoving = true;
        _respawnManager.enabled = false;
        _respawnTutorialManager.enabled = true;
        _roundManager.enabled = false;
        GameContener.FreezePlayers();
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

    private void GameController_OnPlayersPassed(object sender, System.EventArgs e)
    {
        initGame();
    }

    private void _roundManager_OnLastRoundEnded(object sender, System.EventArgs e)
    {
        GameContener.FreezePlayers();
        //TODO: Game End
    }

    private void _roundManager_OnNewRoundStarted(object sender, short roundNumber)
    {
        GameContener.MovePlayersToPosition(getRandomSpawn());
    }

    private void _respawnManager_OnLastPlayerFall(object sender, System.EventArgs e)
    {
        _roundManager.RoundEnd();
    }
        
    private void Update ()
    {
	
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

    private RoundManager _roundManager;
    private RespawnManager _respawnManager;
    private PortalColliderController _portalCollider;
    private CameraController _cameraController;
    private RespawnTutorialManager _respawnTutorialManager;
}
