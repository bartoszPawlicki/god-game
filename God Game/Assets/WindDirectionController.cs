using UnityEngine;
using System.Collections;

public class WindDirectionController : MonoBehaviour
{
    public float _windDirectionX { get; set; }
    public float _windDirectionZ { get; set; }
	// Use this for initialization
	void Start ()
    {
        _globalWindController = GlobalWind.GetComponent<GlobalWindController>();
        transform.position = new Vector3(0f, 0f, 100f);
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_globalWindController.isActiveAndEnabled)
        {
            _windDirectionX = _globalWindController.AimHorizontal;
            _windDirectionZ = _globalWindController.AimVertical;
        }
    }

    public GameObject GlobalWind;
    private GlobalWindController _globalWindController;
}
