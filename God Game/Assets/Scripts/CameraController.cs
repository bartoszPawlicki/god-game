using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public float Distance;
    
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
        _offset = new Vector3(0, distance, -distance * 1.5f);
        if (Player1.GetComponent<PlayerController>().isActiveAndEnabled && Player2.GetComponent<PlayerController>().isActiveAndEnabled)
            _worldPosition = Vector3.Lerp(Player1.transform.position, Player2.transform.position, 0.5f);
        else if (Player1.GetComponent<PlayerController>().isActiveAndEnabled)
            _worldPosition = Player1.transform.position;
        else if (Player2.GetComponent<PlayerController>().isActiveAndEnabled)
            _worldPosition = Player2.transform.position;
        else
            _worldPosition = Vector3.zero;
        transform.position = _worldPosition + _offset;
        transform.LookAt(_worldPosition);
    }
}
