using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public float Distance;

    private float _playersDistance;
    Vector3 _worldPosition;
    Vector3 _offset;
	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        float distance = (float)(Distance / Math.Sqrt(2));
        distance += (float)Math.Sqrt(Vector3.Distance(Player1.transform.position, Player2.transform.position));
        _offset = new Vector3(0, distance, -distance);
        _worldPosition = Vector3.Lerp(Player1.transform.position, Player2.transform.position, 0.5f);
        transform.position = _worldPosition + _offset;
        transform.LookAt(_worldPosition);
    }
}
