using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGodController : MonoBehaviour {

    public float GodInitialSpeed;
    

    void Start ()
    {
        _godSpeed = GodInitialSpeed;
    }

    void FixedUpdate()
    {
        //god movement
        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");
        gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * _godSpeed * Time.deltaTime);
    }

    private float _godSpeed;
}
