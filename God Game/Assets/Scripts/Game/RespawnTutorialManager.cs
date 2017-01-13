using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnTutorialManager : MonoBehaviour
{
    private GameObject[] _players;

    public event PlayerFallEventHandler OnPlayerFall;
    // Use this for initialization
    void Start ()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        foreach (var player in _players)
        {
            // -15 is the best, since player is in fog but when is falling to slow -5 is better
            if (player.transform.position.y < -5 && player.GetComponent<PlayerController>().isActiveAndEnabled)
            {
                if (OnPlayerFall != null)
                    OnPlayerFall.Invoke(this, player);
            }
        }
    }

    public void StartRespawn(GameObject _playerInRange)
    {
        if (OnPlayerFall != null)
            OnPlayerFall.Invoke(this, _playerInRange);
    }
}
