using UnityEngine;
using System.Collections;

public class RotatingBridgeWithButtonController : MonoBehaviour
{
    public float Angle;
    // Use this for initialization
    void Start()
    {
        _buttonController = transform.parent.FindChild("Button").GetComponent<ButtonController>();
        _lenght = transform.localScale.z / 2;
    }

    /// <summary>
    /// I have not idea how the fuck it is going to work 
    /// </summary>
    void Update()
    {
        Debug.Log(Angle - _buttonController.PushValue * (Angle / 100));
        transform.localEulerAngles = new Vector3(0, - _buttonController.PushValue * (Angle / 100), 0);
        float alfa = (360 - transform.localEulerAngles.y) * Mathf.PI / 180;
        float x = Mathf.Sin(alfa) * _lenght;
        float z = (1 - Mathf.Cos(alfa)) * _lenght;
        transform.localPosition = new Vector3(x, 0, z);
    }

    private float _lenght;
    private ButtonController _buttonController;
}

