using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts;

public class RoundManager : MonoBehaviour
{
    public event NewRoundEventHandler OnNewRoundStarted;
    public event EventHandler OnLastRoundEnded;

    public short NumberOfRounds;
    
    public short RoundNumber { get; private set; }

    public void RoundEnd()
    {
        if (RoundNumber < NumberOfRounds)
            _newRound = true;
        else
            if (OnLastRoundEnded != null)
            OnLastRoundEnded.Invoke(this, null);
    }

    void Start ()
    {
        RoundNumber = 0;
        _newRound = true;
    }

	void Update ()
    {
	    if(_newRound == true)
        {
            _newRound = false;
            RoundNumber++;

            if (OnNewRoundStarted != null)
                OnNewRoundStarted.Invoke(this, RoundNumber);
        }
    }

    private bool _newRound = false;
}
