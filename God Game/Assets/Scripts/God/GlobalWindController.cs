using UnityEngine;
using System.Collections;
using System;

public class GlobalWindController : MonoBehaviour
{
    public float GlobalWindStrength;
    public float TotalLifeTime;
    public float AimHorizontal { get; set; }
    public float AimVertical { get; set; }
    public bool IsDirectionChosen
    {
        get { return _isDirectionChosen; }
        private set
        {
            if (value != _isDirectionChosen)
            {
                _isDirectionChosen = value;
                if (value)
                    if (OnDirectionChosen != null)
                        OnDirectionChosen.Invoke(this, null);
            }
        }
    }

    public event EventHandler OnDirectionChosen;
    public event EventHandler OnGlobalWindExpired;

    public GameObject windDirection;
    private WindEffectController _windEffectController;
    
    void Start()
    {
        _windEffectController = windDirection.GetComponent<WindEffectController>();
        _players = GameObject.FindGameObjectsWithTag("Player");
        enabled = false;
        gameObject.SetActive(false);
        OnDirectionChosen += GlobalWindController_OnDirectionChosen;
        OnGlobalWindExpired += GlobalWindController_OnGlobalWindExpired;

        _windEffectController.SetEnabled(false);
        _windEffectController.SetIndicatorEnabled(false);
    }

    private void GlobalWindController_OnGlobalWindExpired(object sender, EventArgs e)
    {
        _windEffectController.SetEnabled(false);
        _windEffectController.SetIndicatorEnabled(false);
    }

    private void GlobalWindController_OnDirectionChosen(object sender, EventArgs e)
    {
        _lifeTime = TotalLifeTime;
    }
    void FixedUpdate()
    {
        if (_isDirectionChosen)
        {
            _lifeTime -= Time.deltaTime;
            foreach (var player in _players)
            {
                player.GetComponent<Rigidbody>().AddForce((new Vector3(AimHorizontal, 0, AimVertical).normalized * GlobalWindStrength));
            }
        }

        if (_lifeTime < 0)
        {
            enabled = false;
            gameObject.SetActive(false);
            if (OnGlobalWindExpired != null)
                OnGlobalWindExpired.Invoke(this, null);
        }
    }

    public void Strike()
    {
        enabled = true;
        gameObject.SetActive(true);
        IsDirectionChosen = true;
        _lifeTime = TotalLifeTime;
        _windEffectController.SetEnabled(true);
        _windEffectController.SetIndicatorEnabled(true);
    }

    private float _lifeTime;
    private float _moveHorizontal;
    private float _moveVertical;
    private GameObject[] _players;
    private bool _isDirectionChosen;
}
