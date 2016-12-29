using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;
using System.Timers;
using Assets.Scripts;
using Assets.Scripts.Utils;

public class PlayerController : MonoBehaviour
{
    public float ThrowCooldownValue
    {
        get
        {
            return _throw.Loading;
        }
    }
    public float RopeCooldownValue
    {
        get
        {
            return _ropeCooldown.Loading;
        }
    }

    public float StartingHp;
    private float _hp;
    public float HP
    {
        get { return _hp; }
        set
        {
            if (value != _hp)
            {
                _hp = value;
                if (_hp <= 0)
                {
                    _hp = 0;
                    //TODO: behaviour on player death
                }
            }
        }
    }

    public int DAMAGE
    {
        get { return _damage; }
        set { _damage = DAMAGE;}
    }


    /// <summary>
    /// Player name has to end with player number
    /// </summary>
    public float GravityStrenght;
    public int RopeCooldown;
    public float StartingSpeed;
    public event DealDamageEventHandler OnInflictDamage;
    //All players had to be on scene or had to be add to players in some way
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rope = GetComponentInChildren<RopeController>();
        _rope.OnRopeReturned += _rope_OnRopeReturned;
        _ropeCooldown = new CooldownProvider(RopeCooldown);

        _throw = GetComponent<ThrowCompanionBehaviour>();

        _speed = StartingSpeed;
        _playerNumber = (int)char.GetNumericValue(transform.gameObject.name[transform.gameObject.name.Length - 1]);
        _slowTimer = new Timer() { AutoReset = false };
        _slowTimer.Elapsed += Timer_Elapsed;
        _risingSlowTicks = 0;

        _damage = 1;
        _hp = StartingHp;
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

    public void ApplyRisingSlow(float slowPower, float slowDuration)
    {
        _risingSlowTicks++;
        if (slowPower * _risingSlowTicks >= 1) _speed = 0;
        else _speed = StartingSpeed * (1 - (slowPower * _risingSlowTicks));
        _slowTimer.Interval = slowDuration;
        _slowTimer.Start();
    }
    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        _speed = StartingSpeed;
        _risingSlowTicks = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("FireRope_" + _playerNumber))
        {
            if (!_collidingWithAnotherPlayer)
            {
                if (!_rope.isActiveAndEnabled && RopeCooldownValue == 100)
                {
                    _ropeCooldown.Use();
                    _rope.FireRope();
                }
                else
                {
                    _rope.EndPulling();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (_rigidbody.velocity.y < -1)
            _rigidbody.AddForce(new Vector3(0, -GravityStrenght, 0));

        float moveHorizontal = Input.GetAxis("Horizontal_" + _playerNumber);
        float moveVertical = Input.GetAxis("Vertical_" + _playerNumber);

        float aimHorizontal = Input.GetAxis("Horizontal_" + _playerNumber + "_rotate");
        float aimVertical = Input.GetAxis("Vertical_" + _playerNumber + "_rotate");



        if (moveHorizontal != 0 || moveVertical != 0 || aimHorizontal != 0 || aimVertical != 0)
        {
            Vector3 movement = transform.position + new Vector3(moveHorizontal, 0, moveVertical) * _speed;
            _rigidbody.MovePosition(movement);
            if (!_rope.isActiveAndEnabled)
            {
                transform.eulerAngles = new Vector3(0, (float)(Math.Atan2(aimHorizontal, aimVertical) * 180 / Math.PI), 0);

            }

        }

        if (_rope.isActiveAndEnabled)
            transform.eulerAngles = new Vector3(0, _rope.transform.eulerAngles.y, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _collidingWithAnotherPlayer = true;
            _rope.EndPulling();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            _collidingWithAnotherPlayer = false;
    }
    private void _rope_OnRopeReturned(object sender, EventArgs e)
    {
        _ropeCooldown.Start();
    }
    private void PlayerControler_OnTotemCaptured(object sender, EventArgs e)
    {
        _damage += 2;
        UnityEngine.Debug.Log("damage++, now equls: " + _damage);
    }
    public void IncreseDamage(int damage)
    {
        _damage += damage;
    }

    private bool _collidingWithAnotherPlayer;
    private Timer _slowTimer;
    private int _playerNumber;
    private float _speed;
    private int _risingSlowTicks;
    private Rigidbody _rigidbody;
    private RopeController _rope;
    private ThrowCompanionBehaviour _throw;
    private int _damage;
    private CooldownProvider _ropeCooldown;
}
