using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts;

public class GameOver : MonoBehaviour
{

    public GameEndEventHandler OnGameEnd;
	// Use this for initialization
	void Start ()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            _playerColliding.Add(item, false);
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTime>().OnTimeElapsed += GameOverController_OnTimeElapsed;
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
        foreach (var item in _playerColliding)
        {
            if (!item.Value)
                return;   
        }
        //TODO: Not always work properly
        //rely unity uses C# 4 ... lol a cant use "?" before invoke ... strange
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTime>().enabled = false;
        if(OnGameEnd != null)
            OnGameEnd.Invoke(this, Winner.Players);
        Debug.Log("Players Winns");
        enabled = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerColliding[collision.gameObject] = true;
            collision.gameObject.SetActive(false);
        }
    }
    private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();
}
