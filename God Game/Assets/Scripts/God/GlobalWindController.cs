﻿using UnityEngine;
using System.Collections;
using System;

public class GlobalWindController : MonoBehaviour
{
    public float GlobalWindStrength;
    public float TotalLifeTime;
    public float AimHorizontal { get; set; }
    public float AimVertical { get; set; }
    public bool IsDirectionChosen
    {
        get { return _isDirectionChosen; }
        private set
        {
            if (value != _isDirectionChosen)
            {
                _isDirectionChosen = value;
                if (value)
                    if (OnDirectionChosen != null)
                        OnDirectionChosen.Invoke(this, null);
            }
        }
    }

    public event EventHandler OnDirectionChosen;
    public event EventHandler OnGlobalWindExpired;

    // Use this for initialization
    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        enabled = false;
        gameObject.SetActive(false);
        OnDirectionChosen += GlobalWindController_OnDirectionChosen;
    }

    private void GlobalWindController_OnDirectionChosen(object sender, EventArgs e)
    {
        _lifeTime = TotalLifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!_isDirectionChosen)
        //{
        //    if (Input.GetAxisRaw("Horizontal_God_Aim") != 0)
        //    {
        //        _moveHorizontal = Input.GetAxis("Horizontal_God_Aim");
        //    }

        //    if (Input.GetAxisRaw("Vertical_God_Aim") != 0)
        //    {
        //        _moveVertical = Input.GetAxis("Vertical_God_Aim");
        //    }
        //}

        //if (_moveHorizontal != 0 || _moveVertical != 0)
        //{
        //    if (Input.GetAxis("Confirm_Target") < -0.5)
        //    {
        //        IsDirectionChosen = true;
        //    }
        //}
    }

    void FixedUpdate()
    {
        if (_isDirectionChosen)
        {
            _lifeTime -= Time.deltaTime;
            foreach (var player in _players)
            {
                player.GetComponent<Rigidbody>().AddForce((new Vector3(AimHorizontal, 0, AimVertical).normalized * GlobalWindStrength));
            }
        }

        if (_lifeTime < 0)
        {
            enabled = false;
            gameObject.SetActive(false);
            if (OnGlobalWindExpired != null)
                OnGlobalWindExpired.Invoke(this, null);
        }
    }

    public void Strike()
    {
        enabled = true;
        gameObject.SetActive(true);
        IsDirectionChosen = true;

        _lifeTime = TotalLifeTime;
    }

    private float _lifeTime;
    private float _moveHorizontal;
    private float _moveVertical;
    private GameObject[] _players;
    private bool _isDirectionChosen;
}
