using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

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
    }

    private void GameController_OnPlayersPassed(object sender, System.EventArgs e)
    {
        _portalCollider.Collider.enabled = false;
        
        GameContener.MovePlayersToPosition(_finalIslandPosition);
    }

    private void _roundManager_OnLastRoundEnded(object sender, System.EventArgs e)
    {
        GameContener.FreezePlayers();
        //TODO: Game End
    }

    private void _roundManager_OnNewRoundStarted(object sender, short roundNumber)
    {
        _timeManager.enabled = true;
        _timeManager.TimeLeft = _timeManager.TotalGameTime;
        _portalCollider.Collider.enabled = true;
        GameContener.MovePlayersToPosition(_startPostion);
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

    private Vector3 _startPostion = new Vector3(-5, 1, 0);
    private Vector3 _finalIslandPosition = new Vector3(-25, 41, -100);

    private RoundManager _roundManager;
    private TimeManager _timeManager;
    private RespawnManager _respawnManager;
    private PortalColliderController _portalCollider;
}
