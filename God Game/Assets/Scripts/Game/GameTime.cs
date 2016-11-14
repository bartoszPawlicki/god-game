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

  //  public GameObject totem;
    public double TotalGameTimeSeconds;
    public double TimePerRound = 120f;
    public bool IsGameStarted = false;
    private bool gameOverflag = false;
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

        if (TimeLeft.CompareTo(TimeSpan.Zero) <= 0 )
        {
            if (gameOverflag == false)
            {
                var gameController = GameObject.FindGameObjectsWithTag("GameController");
                foreach (var gc in gameController)
                {
                    gc.GetComponentInChildren<RoundManager>().newRound = true;
                    TimeLeft += TimeSpan.FromSeconds(TimePerRound);
                    TimeLeftSeconds += TimePerRound;
                    if (gc.GetComponentInChildren<RoundManager>().roundNumber > 5)
                    {
                        gameOverflag = true;
                        gc.GetComponentInChildren<RoundManager>().message = "Game Over";
                        GameOver();
                    }
                }
            }
            var totems = GameObject.FindGameObjectsWithTag("Totem");
            foreach (var totem in totems)
            {
                if (totem.GetComponentInChildren<TotemActivator>().totemCapturedFlagTimeImpact == true)
                {
                    TimeLeft += TimeSpan.FromSeconds(30);
                    TimeLeftSeconds += 30.0f;
                    totem.GetComponentInChildren<TotemActivator>().totemCapturedFlagTimeImpact = false;
                }
            }
        }
    }
    void GameOver()
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
        if (OnTimeElapsed != null)
            OnTimeElapsed.Invoke(this, null);
    }    
    
    private TimeSpan _totalGameTime;
    private TimeSpan _timeleft;
}
