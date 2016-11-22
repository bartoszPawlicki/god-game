using UnityEngine;
using System.Collections;
using System;
using System.Timers;
using System.Diagnostics;

public class RopeController : MonoBehaviour
{
    public float Lenght;
    public float ThrowSpeed;
    public float ThrowStrength;

    public float RemainingCooldown
    {
        get
        {
            return (float)((_cooldownStopwatch.ElapsedMilliseconds + 10) / _cooldownTimer.Interval * 100.0f);
        }
    }
    public int Cooldown
    {
        set
        {
            _cooldownTimer.Interval = value;
        }
    }
    public bool IsReturning { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsPullingPlayer { get; set; }

    public void FireRope()
    {
        if (!IsMoving && !IsReturning && RemainingCooldown >= 100)
        {
            IsMoving = true;
            enabled = true;
            startCooldown();
        }
        else if (IsMoving || IsReturning)
        {
            IsMoving = false;
            IsReturning = false;
            EndPulling();
        }
    }
    public void EndPulling()
    {
        IsPullingPlayer = false;
        _player.GetComponent<ConstantForce>().force = Vector3.zero;
        transform.localScale = new Vector3(0.05f, 0, 0.05f);
    }
    public void InitializeCooldown()
    {
        _cooldownTimer.Elapsed += _cooldownTimer_Elapsed;
        startCooldown();
    }

    private void _cooldownTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        _cooldownStopwatch.Stop();
    }

    void Start ()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player != transform.parent.gameObject)
                _player = player;
        }

        enabled = false;
    }
	
	void FixedUpdate ()
    {
        if (IsPullingPlayer)
        {
            float forceHorizontal = transform.parent.position.x - _player.transform.position.x;
            float forceUp = transform.parent.position.y - _player.transform.position.y;
            float forceVertical = transform.parent.position.z - _player.transform.position.z;
            Vector3 movement = new Vector3(forceHorizontal, forceUp, forceVertical).normalized;
            _player.GetComponent<ConstantForce>().force = movement * ThrowStrength;

            Vector3 firstPoint = transform.parent.position;
            Vector3 secondPoint = _player.transform.position;

            transform.parent.LookAt(_player.transform);
            transform.localEulerAngles = new Vector3(0, -90, -90);
            transform.localScale = new Vector3(0.05f, Vector3.Distance(firstPoint, secondPoint) / 2, 0.05f);
            transform.localPosition = new Vector3(0, 0, transform.localScale.y);
        }
        else
        {
            if ((transform.localScale.y <= Lenght && IsMoving) || (transform.localScale.y >= 0 && IsReturning))
            {
                moveScaleAndRotate();
            }
            else if (IsMoving)
                revertDirection(null);
            else if (IsReturning)
            {
                IsReturning = false;
                IsMoving = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        revertDirection(other.gameObject);
    }
    private void moveScaleAndRotate()
    {
        int direction = 0;

        if (IsMoving)
            direction = 1;
        else if (IsReturning)
            direction = -1;

        transform.parent.LookAt(_player.transform);
        transform.localEulerAngles = new Vector3(0, -90, -90);
        transform.localScale += new Vector3(0, direction, 0) * ThrowSpeed;
        transform.localPosition = new Vector3(0,0, transform.localScale.y);

    }

    private void revertDirection(GameObject gameObject)
    {
        if (gameObject == _player)
        {
            IsPullingPlayer = true;
        }
        else
            IsReturning = true;

        IsMoving = false;
    }
    private void startCooldown()
    {
        _cooldownStopwatch.Reset();
        _cooldownStopwatch.Start();
        _cooldownTimer.Start();
    }

    private GameObject _player;

    private Timer _cooldownTimer = new Timer() { AutoReset = false };
    private Stopwatch _cooldownStopwatch = new Stopwatch();
}
