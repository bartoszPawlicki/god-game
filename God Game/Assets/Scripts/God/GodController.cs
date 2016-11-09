using UnityEngine;
using System.Collections;

public class GodController : MonoBehaviour
{
    public float GodInitialSpeed;
    public float ThunderSpeed;
    
    // Use this for initialization
    void Start ()
    {
        _thunderIndicator = transform.FindChild("ThunderIndicator").gameObject;
        _indicatorLight = _thunderIndicator.GetComponent<Light>();

        _thunder = transform.FindChild("Thunder").gameObject;
        
        _thunderController = _thunder.GetComponent<ThunderController>();
        _thunderController.OnThunderStruck += _thunderController_OnThunderStruck;
        _thunderController.OnThunderExpired += _thunderController_OnThunderExpired;
        
        _godSpeed = GodInitialSpeed;
    }
    
    void Update ()
    {
        if (Input.GetAxis("Fire_Thunder") == 1 && !_thunderController.isActiveAndEnabled)
        {
            _thunderController.Strike();
            _godSpeed = 0;
            _indicatorLight.range *= _lightRange;
        }
    }

    void FixedUpdate()
    {
        //god movement
        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");
        gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * _godSpeed * Time.deltaTime);
    }
    private void _thunderController_OnThunderExpired(object sender, System.EventArgs e)
    {
        _godSpeed = GodInitialSpeed;
        _thunderIndicator.SetActive(true);
    }

    private void _thunderController_OnThunderStruck(object sender, System.EventArgs e)
    {
        _indicatorLight.range /= _lightRange;
        _godSpeed = ThunderSpeed;
        _thunderIndicator.SetActive(false);
    }

    private float _lightRange = 1.5f;
    private float _godSpeed;

    private Light _indicatorLight;
    private GameObject _thunder;
    private GameObject _thunderIndicator;
    private ThunderController _thunderController;
}
