using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;

public class PlayerController : MonoBehaviour
{ 
    public int PlayerNumber;
    public float StartingSpeed;

    //All players had to be on scene or had to be add to players in some way
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _speed = StartingSpeed;
	}
	
    public void ApplySlow(float slowPower, float slowDuration)
    {
        _slowDuration = slowDuration;
        _speed = StartingSpeed * (1 - slowPower);
    }

    void Update()
    {
        _slowDuration -= Time.deltaTime;
        if (_slowDuration < 0) _speed = StartingSpeed;
    }
	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_" + PlayerNumber);
        float moveVertical = Input.GetAxis("Vertical_" + PlayerNumber);

        if (moveHorizontal != 0 | moveVertical != 0)
        {
            Vector3 movement = transform.position + new Vector3(moveHorizontal, _rigidbody.velocity.y, moveVertical) * _speed;
            _rigidbody.MovePosition(movement);
        }
    }


    private float _slowDuration;
    private float _speed;
    private Rigidbody _rigidbody;
}
