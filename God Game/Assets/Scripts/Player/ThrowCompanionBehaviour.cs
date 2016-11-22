using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System;

public class ThrowCompanionBehaviour : MonoBehaviour
{
    /// <summary>
    /// Renaining skill cooldown in percentages
    /// </summary>
    public float RemainingCooldown
    {
        get
        {
            return (float)(_cooldownStopwatch.ElapsedMilliseconds / _cooldownTimer.Interval * 100);
        }
    }
    public int Cooldown
    {
        set
        {
            _cooldownTimer.Interval = value;
        }
    }
    public float ThrowStrength;

    public void InitializeCooldown()
    {
        _cooldownTimer.Elapsed += _cooldownTimer_Elapsed;
        startCooldown();
    }
    void Start()
    {
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in _players)
        {
            if (item != gameObject)
                _playerColliding.Add(item, false);
        }
    }

    private void _cooldownTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        _cooldownStopwatch.Stop();
    }

    void Update ()
    {
        float throwCompanion1 = Input.GetAxis("Throw_1");
        float throwCompanion2 = Input.GetAxis("Throw_2");

        if (throwCompanion1 == 1 && throwCompanion2 == 1 && RemainingCooldown == 100)
        {
            foreach (var item in _playerColliding)
            {
                if (item.Value)
                {
                    float forceHorizontal = item.Key.transform.position.x - transform.position.x;
                    float forceVertical = item.Key.transform.position.z - transform.position.z;
                    Vector3 movement = new Vector3(forceHorizontal, 0, forceVertical).normalized;
                    item.Key.GetComponent<Rigidbody>().AddForce(movement * ThrowStrength);
                    startCooldown();
                }
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (_playerColliding.ContainsKey(collision.gameObject))
            _playerColliding[collision.gameObject] = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (_playerColliding.ContainsKey(collision.gameObject))
            _playerColliding[collision.gameObject] = false;
    }
    private void startCooldown()
    {
        _cooldownTimer.Start();
        _cooldownStopwatch.Reset();
        _cooldownStopwatch.Start();
    }

    private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();

    private Timer _cooldownTimer = new Timer() { AutoReset = false };
    private Stopwatch _cooldownStopwatch = new Stopwatch();
}
