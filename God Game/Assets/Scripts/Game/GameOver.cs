using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts;
using System.Linq;

public class GameOver : MonoBehaviour
{
    
    public GameEndEventHandler OnGameEnd;
    public Vector3 FinalIslandPosition { get; set; }
    public Dictionary<GameObject, bool> PlayerColliding { get; private set; }
    // Use this for initialization
    void Start ()
    {
        FinalIslandPosition = new Vector3(-20, 41, -100);
        var players = GameObject.FindGameObjectsWithTag("Player");
        PlayerColliding = new Dictionary<GameObject, bool>();
        foreach (var item in players)
        {
            PlayerColliding.Add(item, false);
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

        teleport();
        //has to be add in 
        //playersWin();
    }

    private void teleport()
    {

        bool teleport = true;
        foreach (var item in PlayerColliding.Values)
        {
            if (!item)
                teleport = false;
        }

        if (teleport)
        {
            foreach (var item in PlayerColliding.Keys.ToList())
            {
                item.SetActive(true);
                item.transform.position = FinalIslandPosition;
                
                PlayerColliding[item] = false;
            }
            GameObject.FindGameObjectWithTag("God").transform.position = FinalIslandPosition + new Vector3(0, 1.5f, 0);
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
}
