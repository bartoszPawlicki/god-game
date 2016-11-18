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
        _godSkillIndicator = transform.FindChild("ThunderIndicator").gameObject;
        _indicatorLight = _godSkillIndicator.GetComponent<Light>();

        _thunder = transform.FindChild("Thunder").gameObject;
        
        _thunderController = _thunder.GetComponent<ThunderController>();
        _thunderController.OnThunderStruck += _thunderController_OnThunderStruck;
        _thunderController.OnThunderExpired += _thunderController_OnThunderExpired;

        _wind = transform.FindChild("Wind").gameObject;

        _windController = _wind.GetComponent<WindController>();
        _windController.OnWindGustExpired += _windController_OnWindGustExpired;

        _globalWind = transform.FindChild("GlobalWind").gameObject;

        _globalWindController = _globalWind.GetComponent<GlobalWindController>();
        _globalWindController.OnGlobalWindExpired += _globalWindController_OnGlobalWindExpired;
        
        _godSpeed = GodInitialSpeed;

        _skillChosen = Skill.None;
        _startingIndicatorColor = _indicatorLight.color;
    }

    

    void Update ()
    {
        Debug.Log(Input.GetAxis("Confirm_Target"));
        if (Input.GetAxis("Fire_Thunder") == 1)
        {
            _skillChosen = Skill.Thunder;
            _indicatorLight.color = _startingIndicatorColor;
        }

        if (Input.GetAxis("Fire_Wind") == 1)
        {
            _skillChosen = Skill.WindGust;
            _indicatorLight.color = new Color(1F, 0.15F, 0.6F, 1F);
            
        }

        if (Input.GetAxis("Fire_Global_Wind") == 1)
        {
            _skillChosen = Skill.GlobalWind;
            _indicatorLight.color = new Color(0F, 0.4F, 1F, 1F);

        }


        if (Input.GetAxis("Confirm_Target") < -0.5 && _skillChosen != Skill.None)
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

            if (_skillChosen == Skill.GlobalWind && !_globalWindController.isActiveAndEnabled)
            {
                _globalWindController.Strike();
            }
        }

        indicatorRaycastFunc();
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
        _godSkillIndicator.SetActive(true);
        _skillChosen = Skill.None;
    }

    private void _thunderController_OnThunderStruck(object sender, System.EventArgs e)
    {
        _indicatorLight.range /= _lightRange;
        _godSpeed = ThunderSpeed;
        _godSkillIndicator.SetActive(false);
    }

    private void _windController_OnWindGustExpired(object sender, System.EventArgs e)
    {
        _skillChosen = Skill.None;
    }

    private void _globalWindController_OnGlobalWindExpired(object sender, System.EventArgs e)
    {
        _skillChosen = Skill.None;
    }

    private void indicatorRaycastFunc()
    {
        Vector3 _rayOrigin = gameObject.transform.position;
        if (Physics.Raycast(_rayOrigin, Vector3.down, out _indicatorRaycastHit, 20))
        {
            if (!_indicatorRaycastHit.collider.gameObject.CompareTag("Player"))
            {
                if (_godSkillIndicator.transform.position.y != _indicatorRaycastHit.point.y + 0.5F)
                {
                    _godSkillIndicator.transform.Translate(0, _indicatorRaycastHit.point.y + 0.5F - _godSkillIndicator.transform.position.y, 0);  
                }
                
                
            }
        }
    }

    private float _lightRange = 1.5f;
    private float _godSpeed;

    private Light _indicatorLight;
    private Color _startingIndicatorColor;
    private GameObject _thunder;
    private GameObject _wind;
    private GameObject _globalWind;
    private GameObject _godSkillIndicator;
    private ThunderController _thunderController;
    private WindController _windController;
    private GlobalWindController _globalWindController;
    private Skill _skillChosen;

    private RaycastHit _indicatorRaycastHit;
}
