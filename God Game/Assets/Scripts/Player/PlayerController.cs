using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;
using System.Timers;
using Assets.Scripts;
using System.Diagnostics;

public class PlayerController : MonoBehaviour
{
    public float ThrowCooldownValue
    {
        get
        {
            return _throw.RemainingCooldown;
        }
    }
    public float RopeCooldownValue
    {
        get
        {
            return _rope.RemainingCooldown;
        }
    }
    /// <summary>
    /// Player name has to end with player number
    /// </summary>
    public int ThrowCooldown;
    public int RopeCooldown;
    public float StartingSpeed;
    public event DealDamageEventHandler OnInflictDamage;
    //All players had to be on scene or had to be add to players in some way
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rope = GetComponentInChildren<RopeController>();
        if(RopeCooldown != 0)
        { 
            _rope.InitializeCooldown(RopeCooldown);
        }
        _throw = GetComponent<ThrowCompanionBehaviour>();
        if(ThrowCooldown != 0)
        {
            _throw.InitializeCooldown(ThrowCooldown);
        }
        
        _speed = StartingSpeed;
        _playerNumber = (int)char.GetNumericValue(transform.gameObject.name[transform.gameObject.name.Length - 1]);
        _slowTimer = new Timer() { AutoReset = false };
        _slowTimer.Elapsed += Timer_Elapsed;
        _damage = 1;
        
        //Niech to już zniknie xD bo patrzeć nie moge
        //totem1
        _totem1 = transform.parent.FindChild("TotemOfTheEagle").gameObject;
        _totemActivator1 = _totem1.GetComponent<TotemActivator>();
        _totemActivator1.OnTotemCapured += PlayerControler_OnTotemCaptured;
        //totem2
        _totem2 = transform.parent.FindChild("TotemOfTheBear").gameObject;
        _totemActivator2 = _totem2.GetComponent<TotemActivator>();
        _totemActivator2.OnTotemCapured += PlayerControler_OnTotemCaptured;
        //totem3
        _totem3 = transform.parent.FindChild("TotemOfThePhoenix").gameObject;
        _totemActivator3 = _totem3.GetComponent<TotemActivator>();
        _totemActivator3.OnTotemCapured += PlayerControler_OnTotemCaptured;

         InvokeRepeating("InflictDamageOnGod", 3.0f,1.0f);
    }
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="slowPower">0.3f = 30%</param>
    /// <param name="slowDuration">In miliseconds</param>
    public void ApplySlow(float slowPower, float slowDuration)
    {
        _speed *= (1 - slowPower);
        _slowTimer.Interval = slowDuration;
        _slowTimer.Start();
    }
    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        _speed = StartingSpeed;
    }
    
    void Update()
    {
        if (Input.GetAxis("FireRope_" + _playerNumber) != _fireRopeAxisValue)
        {
            _fireRopeAxisValue = Input.GetAxis("FireRope_" + _playerNumber);
            if(_fireRopeAxisValue == 1)
                if (!_collidingWithAnotherPlayer)
                    _rope.FireRope();
        }
    }

	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_" + _playerNumber);
        float moveVertical = Input.GetAxis("Vertical_" + _playerNumber);

        if (moveHorizontal != 0 | moveVertical != 0)
        {
            Vector3 movement = transform.position + new Vector3(moveHorizontal, 0, moveVertical) * _speed;
            _rigidbody.MovePosition(movement);
            if (!_rope.IsMoving && !_rope.IsReturning)
            {
                transform.eulerAngles = new Vector3(0, (float)(Math.Atan2(moveHorizontal,moveVertical) * 180 / Math.PI), 0);
            }
                
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _collidingWithAnotherPlayer = true;
            if (_rope.IsPullingPlayer)
                _rope.EndPulling();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            _collidingWithAnotherPlayer = false;
    }
    private void PlayerControler_OnTotemCaptured(object sender, EventArgs e)
    {
        _damage += 2;
        UnityEngine.Debug.Log("damage++, now equls: " + _damage);
    }
    private void InflictDamageOnGod()
    {
        if (gameObject.transform.position.y > 30)
        {
            if (OnInflictDamage != null)
                OnInflictDamage.Invoke(this, _damage);
        }
    }

    private float _fireRopeAxisValue = -1;
    private bool _collidingWithAnotherPlayer;
    private Timer _slowTimer;
    private int _playerNumber;
    private float _speed;
    private Rigidbody _rigidbody;
    private RopeController _rope;
    private ThrowCompanionBehaviour _throw;
    private int _damage;

    // wymyslic czy nie da sie lepiej bez robienia niepotrzebnego syfu
    private TotemActivator _totemActivator1;
    private GameObject _totem1;
    private TotemActivator _totemActivator2;
    private GameObject _totem2;
    private TotemActivator _totemActivator3;
    private GameObject _totem3;
}
