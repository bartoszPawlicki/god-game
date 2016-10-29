using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private GameObject[] _players;
    private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();
        
    public float JumpingForce;
    public int PlayerNumber;
    public float Diameter;
    public float ThrowStrength;

    private float Speed;
    public float StartingSpeed;
    public float SlowDuration = 0F;

    //All players had to be on scene or had to be add to players in some way
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in _players)
        {
            if(item != gameObject)
                _playerColliding.Add(item, false);
        }
        Speed = StartingSpeed;
	}
	
    public void ApplySlow(float SlowPower, float SlowDuration)
    {
        this.SlowDuration = SlowDuration;
        Speed = StartingSpeed * (1 - SlowPower);
    }

    void Update()
    {
        SlowDuration -= Time.deltaTime;
        if (SlowDuration < 0) Speed = StartingSpeed; 
    }
	void FixedUpdate()
    {
        move();
        throwCompanion();
    }

    private void throwCompanion()
    {
        float throwCompanion1 = Input.GetAxis("Throw_1");
        float throwCompanion2 = Input.GetAxis("Throw_2");

        if (throwCompanion1 == 1 && throwCompanion2 == 1)
        {
            foreach (GameObject player in _players)
            {
                if (player != gameObject && _playerColliding[player])
                {
                    float forceHorizontal = player.transform.position.x - transform.position.x;
                    float forceVertical = player.transform.position.z - transform.position.z;
                    Vector3 movement = new Vector3(forceHorizontal, 0, forceVertical).normalized;
                    player.GetComponent<Rigidbody>().AddForce(movement * ThrowStrength);
                }
            }
        }
    }

    private void move()
    {

        float moveHorizontal = Input.GetAxis("Horizontal_" + PlayerNumber);
        float moveVertical = Input.GetAxis("Vertical_" + PlayerNumber);

        if (moveHorizontal != 0 | moveVertical != 0)
        {
            Vector3 movement = transform.position + new Vector3(moveHorizontal, _rigidbody.velocity.y, moveVertical) * Speed;
            _rigidbody.MovePosition(movement);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        _playerColliding[collision.gameObject] = true;
    }

    void OnCollisionExit(Collision collision)
    {
        _playerColliding[collision.gameObject] = false;
    }
}
