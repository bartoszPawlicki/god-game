using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGodController : MonoBehaviour {

    public float GodInitialSpeed;
    

    void Start ()
    {
        _godSpeed = GodInitialSpeed;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //god movement
        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");

        Vector3 movement = transform.position + new Vector3(moveHorizontal, 0, moveVertical) * _godSpeed * Time.deltaTime;
        _rigidbody.MovePosition(movement);

        //gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * _godSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, (float)(Math.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg), 0);
    }

    private float _godSpeed;
    private Rigidbody _rigidbody;
}
