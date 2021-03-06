﻿using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Timers;
using Assets.Scripts.Utils;
using System;

public class GodController : MonoBehaviour
{
    public float GodInitialSpeed;
    public float ThunderSpeed;
    public int ThunderCooldown;
    public int WaterGeyserCooldown;
    public int GlobalWindCooldown;

    public event EventHandler OnThunderSkillChosen;
    public event EventHandler OnWaterGeyserSkillChosen;
    public event EventHandler OnGlobalWindSKillChosen;
    
    void Start ()
    {
        _godSkillIndicator = transform.FindChild("GodSkillIndicator").gameObject;
        _indicatorLight = _godSkillIndicator.GetComponent<Light>();
        _indicatorLightParticles = _godSkillIndicator.GetComponent<ParticleSystem>();

        _thunder = transform.FindChild("Thunder").gameObject;
        _thunderController = _thunder.GetComponent<ThunderController>();
        _thunderController.OnThunderStruck += _thunderController_OnThunderStruck;
        _thunderController.OnThunderExpired += _thunderController_OnThunderExpired;

        _waterGeyser = transform.FindChild("WaterGeyser").gameObject;
        _waterGeyserController = _waterGeyser.GetComponent<WaterGeyserController>();
        _waterGeyserController.OnWaterGeyserExpired += _waterGeyserController_OnWaterGeyserExpired;

        _globalWind = transform.FindChild("GlobalWind").gameObject;
        _globalWindController = _globalWind.GetComponent<GlobalWindController>();
        _globalWindController.OnGlobalWindExpired += _globalWindController_OnGlobalWindExpired;
        
        _godSpeed = GodInitialSpeed;

        _startingIndicatorColor = _indicatorLight.color;
        SkillChosen = Skill.None;
       
        _thunderCooldown = new CooldownProvider(ThunderCooldown);
        _waterGeyserCooldown = new CooldownProvider(WaterGeyserCooldown);
        _globalWindCooldown = new CooldownProvider(GlobalWindCooldown);

        _areSkillsSilenced = false;
        _rigidbody = GetComponent<Rigidbody>();
    }
    public float ThunderCooldownValue
    {
        get
        {
            return _thunderCooldown.Loading;
        }
    }

    public float WaterGeyserCooldownValue
    {
        get
        {
            return _waterGeyserCooldown.Loading;
        }
    }

    public float GlobalWindCooldownValue
    {
        get
        {
            return _globalWindCooldown.Loading;
        }
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
                        if (OnThunderSkillChosen != null)
                            OnThunderSkillChosen.Invoke(this, null);
                        break;

                    case Skill.WaterGeyser:

                        _indicatorLight.color = new Color(0F, 0.4F, 1F, 1F);
                        _indicatorLightParticles.startColor = new Color(0F, 0.4F, 1F, 1F);
                        if (OnWaterGeyserSkillChosen != null)
                            OnWaterGeyserSkillChosen.Invoke(this, null);
                        break;

                    case Skill.GlobalWind:

                        _indicatorLight.color = _startingIndicatorColor;
                        _indicatorLightParticles.startColor = _startingIndicatorColor;
                        if (OnGlobalWindSKillChosen != null)
                            OnGlobalWindSKillChosen.Invoke(this, null);
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
                if (Input.GetAxisRaw("Horizontal_God") != 0)
                {
                    _globalWindController.AimHorizontal = Input.GetAxis("Horizontal_God");
                }

                if (Input.GetAxisRaw("Vertical_God") != 0)
                {
                    _globalWindController.AimVertical = Input.GetAxis("Vertical_God");
                }
            }
        }

        if ( (Input.GetAxis("Confirm_Target") == 1 || (Input.GetAxis("Confirm_Target") < -0.5) ) && SkillChosen != Skill.None && !_areSkillsSilenced)
        {
            if (SkillChosen == Skill.Thunder && !_thunderController.isActiveAndEnabled && ThunderCooldownValue == 100)
            {
                _thunderCooldown.Use();
                _thunderController.Strike();
                _godSpeed = 0;
                _indicatorLight.range *= _lightRange;
            }

            if (SkillChosen == Skill.WaterGeyser && !_waterGeyserController.isActiveAndEnabled && WaterGeyserCooldownValue == 100)
            {
                _waterGeyserCooldown.Use();
                _waterGeyserController.Strike();
                _indicatorLight.range *= _lightRange;
            }

            if (SkillChosen == Skill.GlobalWind && !_globalWindController.isActiveAndEnabled && (_globalWindController.AimVertical != 0 || _globalWindController.AimHorizontal != 0) && GlobalWindCooldownValue == 100)
            {
                _globalWindCooldown.Use();
                _globalWindController.Strike();
                _indicatorLight.range *= _lightRange;
            }
        }
        indicatorRaycastFunc();
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            
            Vector3 movement = transform.position + new Vector3(moveHorizontal, 0, moveVertical) * _godSpeed;
            _rigidbody.MovePosition(movement);

            //gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * _godSpeed * Time.deltaTime);

            transform.eulerAngles = new Vector3(0, (float)(Math.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg), 0);
        }



    }
    private void _thunderController_OnThunderExpired(object sender, System.EventArgs e)
    {
        _godSpeed = GodInitialSpeed;
        _thunderCooldown.Start();
        
    }

    private void _thunderController_OnThunderStruck(object sender, System.EventArgs e)
    {
        _indicatorLight.range /= _lightRange;
        _godSpeed = ThunderSpeed;
    }

    private void _waterGeyserController_OnWaterGeyserExpired(object sender, System.EventArgs e)
    {
        _indicatorLight.range /= _lightRange;
        _waterGeyserCooldown.Start();
    }

    private void _globalWindController_OnGlobalWindExpired(object sender, System.EventArgs e)
    {
        _globalWindController.AimHorizontal = 0;
        _globalWindController.AimVertical = 0;
        _indicatorLight.range /= _lightRange;
        _globalWindCooldown.Start();
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

    public void silenceGodSkills(bool silence)
    {
        _areSkillsSilenced = silence;
    }

    private float _lightRange = 1.5f;
    private float _godSpeed;

    private Rigidbody _rigidbody;
    private Light _indicatorLight;
    private ParticleSystem _indicatorLightParticles;
    private Color _startingIndicatorColor;
    private GameObject _thunder;
    private GameObject _waterGeyser;
    private GameObject _globalWind;
    private GameObject _godSkillIndicator;
    private ThunderController _thunderController;
    private WaterGeyserController _waterGeyserController;
    private GlobalWindController _globalWindController;
    private Skill _skillChosen;

    private RaycastHit _indicatorRaycastHit;

    private CooldownProvider _thunderCooldown;
    private CooldownProvider _waterGeyserCooldown;
    private CooldownProvider _globalWindCooldown;

    private bool _areSkillsSilenced;

}
