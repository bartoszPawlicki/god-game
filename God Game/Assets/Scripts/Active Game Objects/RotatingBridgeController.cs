using UnityEngine;
using System.Collections;

public class RotatingBridgeController : MonoBehaviour
{
    public float RotatingSpeed;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, 0.1f * RotatingSpeed, 0));
	}
}
