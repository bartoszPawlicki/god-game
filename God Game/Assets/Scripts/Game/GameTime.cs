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
    public double bonusTimeOnCapture = 30d;
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
        //totem1
        _totem1 = transform.FindChild("TotemOfTheEagle").gameObject;
        _totemActivator1 = _totem1.GetComponent<TotemActivator>();
        _totemActivator1.OnTotemCapured += GameTime_OnTotemCapturd;
        //totem2
        _totem2 = transform.FindChild("TotemOfTheBear").gameObject;
        _totemActivator2 = _totem2.GetComponent<TotemActivator>();
        _totemActivator2.OnTotemCapured += GameTime_OnTotemCapturd;
        //totem3
        _totem3 = transform.FindChild("TotemOfThePhoenix").gameObject;
        _totemActivator3 = _totem3.GetComponent<TotemActivator>();
        _totemActivator3.OnTotemCapured += GameTime_OnTotemCapturd;

    //wiem ze brzydko to jest zrobione, ale w przyszlosci nie bede 3 totemy, tylko 3 rozne skrypty :D
        GetComponent<RespawnManager>().OnLastPlayerFall += GameTime_OnLastPlayerFall;  
    }

    private void GameTime_OnLastPlayerFall(object sender, EventArgs e)
    {
        gameObject.GetComponent<RoundManager>().newRound = true;
    }
    private void GameTime_OnTotemCapturd(object sender, EventArgs e)
    {
        TimeLeft += TimeSpan.FromSeconds(bonusTimeOnCapture);
        Debug.Log("totemCaptured");
        //add additional time in case of capturing any totem
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
                    TimeLeft += TimeSpan.FromSeconds(TotalGameTimeSeconds);
                    if (gc.GetComponentInChildren<RoundManager>().roundNumber > 5)
                    {
                        gameOverflag = true;
                        gc.GetComponentInChildren<RoundManager>().message = "Game Over";
                        GameOver();
                    }
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
    private TotemActivator _totemActivator1;
    private GameObject _totem1;
    private TotemActivator _totemActivator2;
    private GameObject _totem2;
    private TotemActivator _totemActivator3;
    private GameObject _totem3;
}
