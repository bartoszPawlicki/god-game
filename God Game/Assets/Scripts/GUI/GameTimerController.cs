using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GameTimerController : MonoBehaviour
{
    private GameTime _gameTime;
	// Use this for initialization
	void Start ()
    {
        _gameTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTime>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        var text = gameObject.GetComponent<Text>();
        text.text = string.Format("{0:00}:{1:00}:{2:00}", _gameTime.TimeLeft.Minutes, _gameTime.TimeLeft.Seconds, _gameTime.TimeLeft.Milliseconds / 10);
	}
}
