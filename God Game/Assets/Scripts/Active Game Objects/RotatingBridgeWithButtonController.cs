﻿using UnityEngine;
using System.Collections;

public class RotatingBridgeWithButtonController : MonoBehaviour
{
    public float Angle;
    // Use this for initialization
    void Start()
    {
        _buttonController = transform.parent.FindChild("Button").GetComponent<ButtonController>();
        try
        {
            _buttonController1 = transform.parent.FindChild("Button1").GetComponent<ButtonController>();
        }
        catch (System.Exception)
        {
        }
        _lenght = transform.localScale.z / 2;
    }

    /// <summary>
    /// I have not idea how the fuck it is going to work 
    /// </summary>
    void Update()
    {
        if(_buttonController1 != null && _buttonController.PushValue > _buttonController1.PushValue)
        {                
            if (_buttonController.PushValue > _buttonController1.PushValue)
                transform.localEulerAngles = new Vector3(0, -_buttonController.PushValue * (Angle / 100), 0);
            else
                transform.localEulerAngles = new Vector3(0, -_buttonController1.PushValue * (Angle / 100), 0);
        }
        else
            transform.localEulerAngles = new Vector3(0, -_buttonController.PushValue * (Angle / 100), 0);

        float alfa = (360 - transform.localEulerAngles.y) * Mathf.PI / 180;
        float x = Mathf.Sin(alfa) * _lenght;
        float z = (1 - Mathf.Cos(alfa)) * _lenght;
        transform.localPosition = new Vector3(x, transform.localPosition.y, z);
    }

    private float _lenght;
    private ButtonController _buttonController;
    private ButtonController _buttonController1;
}

