using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Timers;

public class GodController : MonoBehaviour
{
    
    public float GodInitialSpeed;
    public float ThunderSpeed;

    public float ThunderCooldown;
    public float WaterGeyserCooldown;
    public float GlobalWindCooldown;

    // Use this for initialization
    void Start ()
    {
        _godSkillIndicator = transform.FindChild("ThunderIndicator").gameObject;
        _indicatorLight = _godSkillIndicator.GetComponent<Light>();
        _indicatorLightParticles = _godSkillIndicator.GetComponent<ParticleSystem>();

        _thunder = transform.FindChild("Thunder").gameObject;
        
        _thunderController = _thunder.GetComponent<ThunderController>();
        _thunderController.OnThunderStruck += _thunderController_OnThunderStruck;
        _thunderController.OnThunderExpired += _thunderController_OnThunderExpired;

        _wind = transform.FindChild("WaterGeyser").gameObject;

        _waterGeyserController = _wind.GetComponent<WaterGeyserController>();
        _waterGeyserController.OnWaterGeyserExpired += _windController_OnWindGustExpired;

        _globalWind = transform.FindChild("GlobalWind").gameObject;

        _globalWindController = _globalWind.GetComponent<GlobalWindController>();
        _globalWindController.OnGlobalWindExpired += _globalWindController_OnGlobalWindExpired;
        
        _godSpeed = GodInitialSpeed;

        _startingIndicatorColor = _indicatorLight.color;
        SkillChosen = Skill.None;

        _thunderCooldownTimer = new Timer { AutoReset = false };
        _thunderCooldownTimer.Elapsed += _thunderCooldownTimer_Elapsed;

        _waterGeyserCooldownTimer = new Timer { AutoReset = false };
        _waterGeyserCooldownTimer.Elapsed += _waterGeyserCooldownTimer_Elapsed;

        _globalWindCooldownTimer = new Timer { AutoReset = false };
        _globalWindCooldownTimer.Elapsed += _globalWindCooldownTimer_Elapsed;
        
        _canFireThunder = true;
        _canFireWaterGeyser = true;
        _canFireGlobalWind = true;
        
    }

    private void _thunderCooldownTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        _canFireThunder = true;
    }

    private void _waterGeyserCooldownTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        _canFireWaterGeyser = true;
    }
    

    private void _globalWindCooldownTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        _canFireGlobalWind = true;
    }

    public Skill SkillChosen
    {
        get { return _skillChosen; }
        private set
        {
            if (value != _skillChosen)
            {
                _skillChosen = value;
                switch (value)
                {
                    case Skill.None:
                        _indicatorLight.color = new Color(1F, 1F, 1F, 1F);
                        _indicatorLightParticles.startColor = new Color(1F, 1F, 1F, 1F);
                        break;
                    case Skill.Thunder:

                        _indicatorLight.color = new Color(1F, 0.15F, 0.6F, 1F);
                        _indicatorLightParticles.startColor = new Color(1F, 0.15F, 0.6F, 1F);
                        break;

                    case Skill.WaterGeyser:

                        _indicatorLight.color = new Color(0F, 0.4F, 1F, 1F);
                        _indicatorLightParticles.startColor = new Color(0F, 0.4F, 1F, 1F);
                        break;

                    case Skill.GlobalWind:

                        _indicatorLight.color = _startingIndicatorColor;
                        _indicatorLightParticles.startColor = _startingIndicatorColor;
                        break;
                }
            }
        }
    }

    void Update ()
    {
        if (Input.GetAxis("Fire_Thunder") == 1)
        {
            SkillChosen = Skill.Thunder;
            
        }

        if (Input.GetAxis("Fire_Wind") == 1)
        {
            SkillChosen = Skill.WaterGeyser;
            
            
        }

        if (Input.GetAxis("Fire_Global_Wind") == 1)
        {
            SkillChosen = Skill.GlobalWind;

        }

        if(SkillChosen == Skill.GlobalWind)
        {
            if (!_globalWindController.isActiveAndEnabled)
            {
                if (Input.GetAxisRaw("Horizontal_God_Aim") != 0)
                {
                    _globalWindController.AimHorizontal = Input.GetAxis("Horizontal_God_Aim");
                }

                if (Input.GetAxisRaw("Vertical_God_Aim") != 0)
                {
                    _globalWindController.AimVertical = Input.GetAxis("Vertical_God_Aim");
                }
            }
                
        }

        if (Input.GetAxis("Confirm_Target") == 1 || (Input.GetAxis("Confirm_Target") < -0.5) && SkillChosen != Skill.None)
        {
            if (SkillChosen == Skill.Thunder && !_thunderController.isActiveAndEnabled && _canFireThunder)
            {
                _canFireThunder = false;
                _thunderController.Strike();
                _godSpeed = 0;
                _indicatorLight.range *= _lightRange;
                
            }

            if (SkillChosen == Skill.WaterGeyser && !_waterGeyserController.isActiveAndEnabled && _canFireWaterGeyser)
            {
                _canFireWaterGeyser = false;
                _waterGeyserController.Strike();
                _indicatorLight.range *= _lightRange;
            }

            if (SkillChosen == Skill.GlobalWind && !_globalWindController.isActiveAndEnabled && (_globalWindController.AimVertical != 0 || _globalWindController.AimHorizontal != 0) && _canFireGlobalWind)
            {
                _canFireGlobalWind = false;
                _globalWindController.Strike();
                _indicatorLight.range *= _lightRange;
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
        SkillChosen = Skill.None;
        _thunderCooldownTimer.Interval = ThunderCooldown;
        _thunderCooldownTimer.Start();
    }

    private void _thunderController_OnThunderStruck(object sender, System.EventArgs e)
    {
        _indicatorLight.range /= _lightRange;
        _godSpeed = ThunderSpeed;
    }

    private void _windController_OnWindGustExpired(object sender, System.EventArgs e)
    {
        SkillChosen = Skill.None;
        _indicatorLight.range /= _lightRange;
        _waterGeyserCooldownTimer.Interval = WaterGeyserCooldown;
        _waterGeyserCooldownTimer.Start();
    }

    private void _globalWindController_OnGlobalWindExpired(object sender, System.EventArgs e)
    {
        SkillChosen = Skill.None;
        _globalWindController.AimHorizontal = 0;
        _globalWindController.AimVertical = 0;
        _indicatorLight.range /= _lightRange;

        _globalWindCooldownTimer.Interval = GlobalWindCooldown;
        _globalWindCooldownTimer.Start();
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
    private ParticleSystem _indicatorLightParticles;
    private Color _startingIndicatorColor;
    private GameObject _thunder;
    private GameObject _wind;
    private GameObject _globalWind;
    private GameObject _godSkillIndicator;
    private ThunderController _thunderController;
    private WaterGeyserController _waterGeyserController;
    private GlobalWindController _globalWindController;
    private Skill _skillChosen;

    private RaycastHit _indicatorRaycastHit;

    private Timer _thunderCooldownTimer;
    private Timer _waterGeyserCooldownTimer;
    private Timer _globalWindCooldownTimer;

    private bool _canFireThunder;
    private bool _canFireWaterGeyser;
    private bool _canFireGlobalWind;

   
}
