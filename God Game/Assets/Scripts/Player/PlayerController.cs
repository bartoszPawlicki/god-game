using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;
using System.Timers;

public class PlayerController : MonoBehaviour
{ 
    /// <summary>
    /// Player name has to end with player number
    /// </summary>
    public float StartingSpeed;

    //All players had to be on scene or had to be add to players in some way
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rope = GetComponentInChildren<RopeController>();
        _speed = StartingSpeed;
        _playerNumber = (int)char.GetNumericValue(transform.gameObject.name[transform.gameObject.name.Length - 1]);
        _slowTimer = new Timer() { AutoReset = false };
        _slowTimer.Elapsed += Timer_Elapsed;
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
        if (Input.GetAxis("FireRope_" + _playerNumber) == 1)
            if(!_collidingWithAnotherPlayer)
                _rope.FireRope();
    }

	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_" + _playerNumber);
        float moveVertical = Input.GetAxis("Vertical_" + _playerNumber);

        if (moveHorizontal != 0 | moveVertical != 0)
        {
            Vector3 movement = transform.position + new Vector3(moveHorizontal, _rigidbody.velocity.y, moveVertical) * _speed;
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

        foreach (var contact in collision.contacts)
        {
            if(contact.thisCollider.gameObject == _rope.gameObject)
            {
                _rope.Collision(collision);
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            _collidingWithAnotherPlayer = false;
    }

    private bool _collidingWithAnotherPlayer;
    private Timer _slowTimer;
    private int _playerNumber;
    private float _speed;
    private Rigidbody _rigidbody;
    private RopeController _rope;
}
