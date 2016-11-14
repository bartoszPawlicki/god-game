using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;

public class PlayerController : MonoBehaviour
{ 
    public float StartingSpeed;
    public float SlowDuration;

    //All players had to be on scene or had to be add to players in some way
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rope = GetComponentInChildren<RopeController>();
        _speed = StartingSpeed;
        _playerNumber = (int)char.GetNumericValue(transform.gameObject.name[transform.gameObject.name.Length - 1]);
	}
	
    public void ApplySlow(float SlowPower, float SlowDuration)
    {
        this.SlowDuration = SlowDuration;
        _speed = StartingSpeed * (1 - SlowPower);
    }

    void Update()
    {
        SlowDuration -= Time.deltaTime;
        if (SlowDuration < 0) _speed = StartingSpeed;
    }
	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_" + _playerNumber);
        float moveVertical = Input.GetAxis("Vertical_" + _playerNumber);

        if (moveHorizontal != 0 | moveVertical != 0)
        {
            Vector3 movement = transform.position + new Vector3(moveHorizontal, _rigidbody.velocity.y, moveVertical) * _speed;
            _rigidbody.MovePosition(movement);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            if(contact.thisCollider.gameObject == _rope.gameObject)
            {
                _rope.Collision(collision);
                break;
            }
        }
    }

    private int _playerNumber;
    private float _speed;
    private Rigidbody _rigidbody;
    private RopeController _rope;
}
