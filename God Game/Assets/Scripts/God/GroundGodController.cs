using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GroundGodController : MonoBehaviour {

    public float GodInitialSpeed;
    public float ThunderCooldown;

    public float ThunderCooldownValue
    {
        get
        {
            return _thunderCooldown.Loading;
        }
    }
    void Start ()
    {
        _godSpeed = GodInitialSpeed;
        _rigidbody = GetComponent<Rigidbody>();
        _thunder = transform.FindChild("Thunder").gameObject;
        _thunder.SetActive(false);
        _thunderCooldown = new CooldownProvider(ThunderCooldown);

        _thunderTimer = new Timer() { Interval = ThunderCooldown / 2, AutoReset = false};
        _thunderTimer.Elapsed += _thunderTimer_Elapsed;

        VibraionTimer = 0f;

        Timer timer = new Timer() { AutoReset = true, Interval = 20 };
        timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        PadVibrationOnDamage();
    }

    private void _thunderTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        _setThunderActiveFalse = true;
        _thunderCooldown.Start();
    }

    void Update()
    {
        

        if (Input.GetButtonDown("Fire_Thunder") && ThunderCooldownValue == 100)
        {
            _thunderCooldown.Use();
            _thunder.SetActive(true);
            _thunderTimer.Start();
        }

        if(_setThunderActiveFalse)
        {
            _setThunderActiveFalse = false;
            _thunder.SetActive(false);
        }

    }

    void FixedUpdate()
    {
        //god movement
        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");

        if (moveHorizontal != 0 || moveVertical != 0)
        {

            Vector3 movement = transform.position + new Vector3(moveHorizontal, 0, moveVertical) * _godSpeed;
            _rigidbody.MovePosition(movement);

            //gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * _godSpeed * Time.deltaTime);

            transform.eulerAngles = new Vector3(0, (float)(Math.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg), 0);
        }
    }

    public void ApplySpecialAbility(int sa)
    {
        _sa = sa;
        if (_sa == 3)
        {
            StartCoroutine(StartSlow());
        }
    }
    public IEnumerator StartSlow()
    {
        //TODO slow powinien dzialas 5 s od czasu dostania ostatnia kulka
        _godSpeed = GodInitialSpeed / 2;
        Debug.Log("dostaje slowa");
        yield return new WaitForSeconds(5);
        _godSpeed = GodInitialSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            _rigidbody.drag = 10;

        if (collision.collider.gameObject.tag == "Bullet")
        {
            ApplySpecialAbility(_sa);
        }
    }

    void PadVibrationOnDamage()
    {
        if (VibraionTimer >= 0)
        {
            IsVibrationInUse = true;
            VibraionTimer -= Time.deltaTime;
        }

        else IsVibrationInUse = false;


    }

    public bool IsVibrationInUse
    {
        get { return _isVibrationInUse; }
        private set
        {
            if (value != _isVibrationInUse)
            {
                _isVibrationInUse = value;

                //if (value) GamePad.SetVibration((PlayerIndex)2, 1, 1);
                //else GamePad.SetVibration((PlayerIndex)2, 0, 0);
            }
        }
    }
    
    private bool _setThunderActiveFalse;

    private float _godSpeed;
    private Rigidbody _rigidbody;

    private Timer _thunderTimer; 
    private GameObject _thunder;
    private CooldownProvider _thunderCooldown;

    private int _sa;

    public float VibraionTimer { get; set; } 
    private bool _isVibrationInUse = false;
}
