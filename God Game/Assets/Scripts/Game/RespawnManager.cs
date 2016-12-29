using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using System;

public class RespawnManager : MonoBehaviour
{
    /// <summary>
    /// millis
    /// </summary>
    public float RespawnTime;
    public event EventHandler OnLastPlayerFall;

    public void StartRespawn(GameObject gameObject)
    {
        foreach (var item in _playerTuples)
        {
            if(item.GameObject == gameObject)
            {
                StartRespawn(item);
                break;
            }
        }
    }

    public void StartRespawn(Tuple tuple)
    {
        tuple.GameObject.SetActive(false);
        tuple.Timer.Start();
        _playersInGame--;
    }
    void Start ()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        _playersInGame = players.Length;

        foreach (var item in players)
        { 
            Timer timer = new Timer(RespawnTime) { AutoReset = false};
            timer.Elapsed += Timer_Elapsed;
            _playerTuples.Add(new Tuple() { GameObject = item, Timer = timer });
        }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if(_playersInGame < _playerTuples.Count)
        {
            foreach (var item in _playerTuples)
            {
                if (item.Timer == (Timer)sender)
                {
                    _objectToSetActive = item.GameObject;
                    _playersInGame++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        foreach (var item in _playerTuples)
        {
            // -15 is the best, since player is in fog but when is falling to slow -5 is better
            if (item.GameObject.transform.position.y < -5 && item.GameObject.GetComponent<PlayerController>().isActiveAndEnabled)
            {
                StartRespawn(item);
            }
        }

        if(_playersInGame <= 0)
        {
            if (OnLastPlayerFall != null)
                OnLastPlayerFall.Invoke(this, null);
            enabled = false;
            _playersInGame = 2;
        }

        foreach (var item in _playerTuples)
        {
            if(item.GameObject.GetComponent<PlayerController>().isActiveAndEnabled)
            {
                _livingPlayerPosition = item.GameObject.transform.position;
                break;
            }
        }

        if(_objectToSetActive != null)
        {
            _objectToSetActive.transform.position = _livingPlayerPosition + new Vector3(5, 2, 0);
            _objectToSetActive.SetActive(true);
            _objectToSetActive = null;
        }
	}

    private int _playersInGame;
    private List<Tuple> _playerTuples = new List<Tuple>();
    private Vector3 _livingPlayerPosition;
    private GameObject _objectToSetActive;
    public class Tuple
    {
        public GameObject GameObject { get; set; }
        public Timer Timer { get; set; }
    }
}