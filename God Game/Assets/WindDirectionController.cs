using UnityEngine;
using System.Collections;

public class WindDirectionController : MonoBehaviour
{
    public float _windDirectionX { get; set; }
    public float _windDirectionZ { get; set; }
	// Use this for initialization
	void Start ()
    {
        _globalWindController = GetComponentInParent<GlobalWindController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        _windDirectionX = _globalWindController.AimHorizontal;
        _windDirectionZ = _globalWindController.AimVertical;

        Debug.Log("_windDirectionX: " + _windDirectionX);
        Debug.Log("_windDirectionZ: " + _windDirectionZ);
    }
    
    private GlobalWindController _globalWindController;
}
