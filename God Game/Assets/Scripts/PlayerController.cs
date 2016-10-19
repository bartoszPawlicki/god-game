using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private GameObject[] _players;
    private Dictionary<GameObject, float> _playersMasses = new Dictionary<GameObject, float>();
        
    public float JumpingForce;
    public int PlayerNumber;
    public float Speed;
    public float Diameter;
    public float ThrowStrength;

    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	void FixedUpdate()
    {
        move();
        throwCompanion();
    }

    bool IsUsed;

    private void throwCompanion()
    {
        float throwCompanion = Input.GetAxis("Throw_" + PlayerNumber);

        if (throwCompanion == 1)
        {
            foreach (GameObject player in _players)
            {
                if (player != gameObject && Vector3.Distance(gameObject.transform.position, player.transform.position) < 1)
                {
                    float forceHorizontal = player.transform.position.x - transform.position.x;
                    float forceVertical = player.transform.position.z - transform.position.z;

                    Vector3 movement = new Vector3(forceHorizontal, 0, forceVertical).normalized;

                    Debug.Log(movement * ThrowStrength);

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
    //private void move()
    //{

    //    foreach (var item in _playersMasses)
    //    {
    //        item.Key.GetComponent<Rigidbody>().mass = item.Value;
    //    }

    //    _playersMasses.Clear();

    //    float moveHorizontal = Input.GetAxis("Horizontal_" + PlayerNumber);
    //    float moveVertical = Input.GetAxis("Vertical_" + PlayerNumber);

    //    if(moveHorizontal != 0 | moveVertical != 0)
    //    {

    //        Vector3 movement = transform.position + new Vector3(moveHorizontal, _rigidbody.velocity.y, moveVertical) * Speed;

    //        foreach (GameObject player in _players)
    //        {
    //            if (player != gameObject)
    //            {
    //                if (Vector3.Distance(movement, player.transform.position) < 1)
    //                {
    //                    _rigidbody.mass = 100;
    //                    _playersMasses.Add(player, player.GetComponent<Rigidbody>().mass);
    //                    player.GetComponent<Rigidbody>().mass = float.MaxValue;
    //                }
    //            }
    //        }

    //        _rigidbody.MovePosition(movement);

    //    }
    //}
}
