using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodSwitchManager : MonoBehaviour {
    
    public GameObject God;
    public GameObject GroundGod;

    public float GroundGodDuration;

    public bool IsGroundGodExpired
    {
        get { return _isGroundGodExpired; }
        private set
        {
            if (value != _isGroundGodExpired)
            {
                _isGroundGodExpired = value;
                if (value)
                    if (OnGroundGodExpired != null)
                        OnGroundGodExpired.Invoke(this, null);
            }
        }
    }

    public event EventHandler OnGroundGodExpired;

    void Start ()
    {
        IsGroundGodExpired = false;
        OnGroundGodExpired += GodSwitchManager_OnGroundGodExpired;

        _templeDestroyed = false;

        _godController = God.GetComponent<GodController>();
        _groundGodController = GroundGod.GetComponent<GroundGodController>();

        _temples = GameObject.FindGameObjectsWithTag("Temple");

        foreach (GameObject temple in _temples)
        {
            TempleController cont = temple.GetComponent<TempleController>();
            cont.OnTempleDestroyed += TempleDestroyed;
        }

        GroundGod.SetActive(false);
        _groundGodController.enabled = false;
        

    }

    private void GodSwitchManager_OnGroundGodExpired(object sender, EventArgs e)
    {
        _godController.enabled = true;
        God.SetActive(true);
        God.transform.position = new Vector3(GroundGod.transform.position.x, 5f, GroundGod.transform.position.z);
        _godController.silenceGodSkills(false);

        _groundGodController.enabled = false;
        GroundGod.SetActive(false); 
    }

    public void TempleDestroyed(object sender, Transform transform)
    {
        _godController.enabled = false;
        God.SetActive(false);

        _groundGodController.enabled = true;
        GroundGod.SetActive(true);
        GroundGod.transform.position = new Vector3(transform.position.x, 10, transform.position.z);

        _groundGodTimer = GroundGodDuration;
        _templeDestroyed = true;
    }

    void Update()
    {
        if (!IsGroundGodExpired && _templeDestroyed)
        {
            _groundGodTimer -= Time.deltaTime;
            if (_groundGodTimer <= 0)
            {
                IsGroundGodExpired = true;
                _templeDestroyed = false;
            }
        }
        
    }

    private GodController _godController;
    private GroundGodController _groundGodController;
    private float _groundGodTimer;

    private GameObject[] _temples;
    private bool _isGroundGodExpired;
    private bool _templeDestroyed;
    
}
