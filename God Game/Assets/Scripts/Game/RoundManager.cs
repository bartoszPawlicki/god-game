using UnityEngine;
using System.Collections;
using System;

public class RoundManager : MonoBehaviour
{
    public event EventHandler OnNewRoundStarted;
    private GameObject[] _players;
    private GameObject _god;
    public bool newRound { get; set; }
    public bool messageflag = false;
    public short roundNumber { get; set; }
    public string message { get; set; } 
    private float messageDisplayTime = 0f;

    void Start ()
    {
        newRound = true;
        messageflag = true;
        roundNumber = 1;
        setAllPlayersOnStartingPosition();
    }
	void Update ()
    {
	    if(newRound == true)
        {
            setAllPlayersOnStartingPosition();
            message = "Round " + roundNumber.ToString();
            roundNumber++;
            messageDisplayTime = 4f;
            messageflag = true;
            newRound = false;

            if (OnNewRoundStarted != null)
                OnNewRoundStarted.Invoke(this, null);
        }
        messageDisplayTime -= Time.deltaTime;
        if (messageDisplayTime <= 0)
        {
            messageflag = false;
        }
    }
    void OnGUI()
    {
        GUI.skin.label.fontSize = 70;
        if (messageflag == true)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 3, 500f, 500f), message);
        }
    }
    void setAllPlayersOnStartingPosition()
    {
         _players = GameObject.FindGameObjectsWithTag("Player");
         _god = GameObject.FindGameObjectWithTag("God");
        short i = 0;
        foreach (var player in _players)
        {
            player.transform.position = new Vector3(-5 + i, 1, 0);
            i += 10;
        }
            _god.transform.position = new Vector3(0, 3, 0);

    }
}
