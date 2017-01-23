using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using System;
using System.Linq;

public class RespawnManager : MonoBehaviour
{
    /// <summary>
    /// millis
    /// </summary>
    public float RespawnTime;
    public event EventHandler OnLastPlayerDied;
    public float HPLost;

    public void InitRespawnManager()
    {
        
    }

    public void StartRespawn(GameObject gameObject)
    {
        foreach (var item in _playerTuples)
        {
            if(item.GameObject == gameObject)
            {
                item.GameObject.GetComponent<SprintScript>().EndSprint();
                StartRespawn(item);
                break;
            }
        }
    }

    public void StartRespawn(Tuple tuple)
    {
        tuple.GameObject.SetActive(false);
        tuple.Timer.Start();
    }
    void Start ()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        //_playersInGame = players.Length;

        foreach (var item in players)
        { 
            Timer timer = new Timer(RespawnTime) { AutoReset = false};
            timer.Elapsed += Timer_Elapsed;
            _playerTuples.Add(new Tuple() { GameObject = item, Timer = timer, PlayerController = item.GetComponent<PlayerController>() });
        }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {

            foreach (var item in _playerTuples)
            {
                if (item.Timer == (Timer)sender)
                {
                    _objectToSetActive = item;
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
        
        if (_playerTuples.Where(x => x.GameObject.activeInHierarchy).ToList().Count < 1)
        {
            if (OnLastPlayerDied != null)
                OnLastPlayerDied.Invoke(this, null);
        }

        foreach (var item in _playerTuples)
        {
            if(item.GameObject.GetComponent<PlayerController>().isActiveAndEnabled)
            {
                _livingPlayerPosition = item.GameObject.transform.position;
                break;
            }
        }

        if (_objectToSetActive != null && _playerTuples.Where(x => x.PlayerController.isActiveAndEnabled).ToList().Count > 0)
        {
            _objectToSetActive.GameObject.transform.position = _livingPlayerPosition + new Vector3(5, 2, 0);
            _objectToSetActive.GameObject.SetActive(true);
            _objectToSetActive.PlayerController.HP *= (1 - HPLost);
            _objectToSetActive = null;
        }
	}
    
    private List<Tuple> _playerTuples = new List<Tuple>();
    private Vector3 _livingPlayerPosition;
    private Tuple _objectToSetActive;
    public class Tuple
    {
        public PlayerController PlayerController { get; set; }
        public GameObject GameObject { get; set; }
        public Timer Timer { get; set; }
    }
}