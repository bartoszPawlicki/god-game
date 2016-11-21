using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts;
using System.Linq;

public class GameOver : MonoBehaviour
{
    public Vector3 FinalIslandPosition { get; set; }
    public GameEndEventHandler OnGameEnd;
	// Use this for initialization
	void Start ()
    {
        FinalIslandPosition = new Vector3(-20, 41, -100);
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            _playerColliding.Add(item, false);
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTime>().OnTimeElapsed += GameOverController_OnTimeElapsed;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<RoundManager>().OnNewRoundStarted += GameOver_OnNewRoundStarted;
    }

    private void GameOver_OnNewRoundStarted(object sender, EventArgs e)
    {
        GetComponent<Collider>().enabled = true;
    }

    private void GameOverController_OnTimeElapsed(object sender, EventArgs e)
    {
        Debug.Log("God Winns");
        enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        //it have to be in the end of function becouse of return in foreach

        teleport();
        //had to be add in 
        //playersWin();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerColliding[collision.gameObject] = true;
            collision.gameObject.SetActive(false);
        }
    }
    private void teleport()
    {

        bool teleport = true;
        foreach (var item in _playerColliding.Values)
        {
            if (!item)
                teleport = false;
        }

        if (teleport)
        {
            foreach (var item in _playerColliding.Keys.ToList())
            {
                item.SetActive(true);
                item.transform.position = FinalIslandPosition;
                
                _playerColliding[item] = false;
            }
            GameObject.FindGameObjectWithTag("God").transform.position = FinalIslandPosition + new Vector3(0, 1.5f, 0);
            GetComponent<Collider>().enabled = false;
        }

    }
    private void playersWin()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTime>().enabled = false;
        if (OnGameEnd != null)
            OnGameEnd.Invoke(this, Winner.Players);
        Debug.Log("Players Winns");
        enabled = false;

    }
    private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();
}
