using UnityEngine;
using System.Collections;
using System;

public class ButtonController : MonoBehaviour
{
    public float Speed;
    public float PushValue
    {
        get
        {
            return ((1000.0f/3.0f)*transform.localScale.y - (400.0f/3.0f)) * -1;
        }
    }

    public bool IsPressed { get; private set; }       
    // Use this for initialization
    void Start ()
    {
        _originYScale = transform.localScale.y;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (IsPressed && transform.localScale.y > _minYScale)
            transform.localScale -= new Vector3(0, 1, 0) * Speed;
        else if(!IsPressed && transform.localScale.y < _originYScale)
            transform.localScale += new Vector3(0, 1, 0) * Speed;
    }

    void Presse()
    {
        IsPressed = true;
    }

    void Release()
    {
        IsPressed = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Presse();
    }

    void OnCollisionExit(Collision collision)
    {
        Release();
    }
    
    private float _originYScale;
    private float _minYScale = 0.1f;
}
