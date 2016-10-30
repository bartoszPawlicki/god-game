using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// GameTime has to be enabled when reuse
/// </summary>
public class GameTime : MonoBehaviour
{
    /// <summary>
    /// Game Time in seconds
    /// </summary>
    public double TotalGameTimeSeconds;
    public bool IsGameStarted = false;
    public event EventHandler OnTimeElapsed;
    public TimeSpan TotalGameTime
    {
        get { return _totalGameTime; }
        set 
        {
            if (value != _totalGameTime)
            {

                _totalGameTime = value;
                TotalGameTimeSeconds = _totalGameTime.TotalSeconds;
            }
        }
    }

    public double TimeLeftSeconds;
    public TimeSpan TimeLeft
    {
        get { return _timeleft; }
        set
        {
            if (value != _timeleft)
            {
                _timeleft = value;
                TimeLeftSeconds = _timeleft.TotalSeconds;
            }
        }
    }
    void Start ()
    {
        TotalGameTime = TimeSpan.FromSeconds(TotalGameTimeSeconds);
        IsGameStarted = true;
        TimeLeft = TotalGameTime;
	}
	
	// Update is called once per frame
	void Update()
    {
        TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
        if(TimeLeft.CompareTo(TimeSpan.Zero) <= 0)
        {
            TimeLeft = TimeSpan.Zero;
            var players = GameObject.FindGameObjectsWithTag("Player");
            var god = GameObject.FindGameObjectWithTag("God");

            foreach (var item in players)
            {
                var rigidbodyPlayer = item.GetComponent<Rigidbody>();
                rigidbodyPlayer.constraints = RigidbodyConstraints.FreezePosition;
            }

            var rigidbodyGod = god.GetComponent<Rigidbody>();
            rigidbodyGod.constraints = RigidbodyConstraints.FreezePosition;

            enabled = false;
            if(OnTimeElapsed != null)
                OnTimeElapsed.Invoke(this, null);
        }
	}

    private TimeSpan _totalGameTime;
    private TimeSpan _timeleft;
}
