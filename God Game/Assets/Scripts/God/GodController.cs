using UnityEngine;
using System.Collections;
using Assets.Scripts;

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

        _wind = transform.FindChild("Wind").gameObject;

        _windController = _wind.GetComponent<WindController>();
        _windController.OnWindGustExpired += _windController_OnWindGustExpired;
        
        _godSpeed = GodInitialSpeed;

        _skillChosen = Skill.None;
    }

    

    void Update ()
    {
        if (Input.GetAxis("Fire_Thunder") == 1)
        {
            _skillChosen = Skill.Thunder;
        }

        if (Input.GetAxis("Fire_Wind") == 1)
        {
            _skillChosen = Skill.WindGust;
        }


        if (Input.GetAxis("Confirm_Target") == 1 && _skillChosen != Skill.None)
        {
            if (_skillChosen == Skill.Thunder && !_thunderController.isActiveAndEnabled)
            {
                _thunderController.Strike();
                _godSpeed = 0;
                _indicatorLight.range *= _lightRange;
            }

            if (_skillChosen == Skill.WindGust && !_windController.isActiveAndEnabled)
            {
                _windController.Strike();
            }
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
        _skillChosen = Skill.None;
    }

    private void _thunderController_OnThunderStruck(object sender, System.EventArgs e)
    {
        _indicatorLight.range /= _lightRange;
        _godSpeed = ThunderSpeed;
        _thunderIndicator.SetActive(false);
    }

    private void _windController_OnWindGustExpired(object sender, System.EventArgs e)
    {
        _skillChosen = Skill.None;
    }

    private float _lightRange = 1.5f;
    private float _godSpeed;

    private Light _indicatorLight;
    private GameObject _thunder;
    private GameObject _wind;
    private GameObject _thunderIndicator;
    private ThunderController _thunderController;
    private WindController _windController;
    private Skill _skillChosen;
}
