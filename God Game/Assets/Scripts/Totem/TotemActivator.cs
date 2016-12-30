using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts;

public class TotemActivator : MonoBehaviour
{
    public float captureSpeed = 0.2f;
    public GameObject player1;
    public GameObject player2;
    
    public event EventHandler OnTotemCapured;
    public event EnableSpecialAbility OnEnableSpecialAbility;
    void Start()
    {
        _playerControler1 = player1.GetComponent<PlayerController>();
        _playerControler2 = player2.GetComponent<PlayerController>();
        transform.FindChild("BlobLightProjector").SetParent(null);
    }
    void FixedUpdate()
    {
        float captureTotem1 = Input.GetAxis("CaptureTotem_1");
        float captureTotem2 = Input.GetAxis("CaptureTotem_2");

        if (captureTotem1 == 1 && captureTotem2 == 1)
        {
            if (Vector3.Distance(_playerControler1.transform.position, gameObject.transform.position) < totemRange
                && gameObject.transform.position.y - _playerControler1.transform.position.y < 0.5f && _totemOfEagleCaptured == false
                && Vector3.Distance(_playerControler2.transform.position, gameObject.transform.position) < totemRange &&
                gameObject.transform.position.y - _playerControler2.transform.position.y < 0.5f && _totemOfEagleCaptured == false)
            {
                captureTotem(captureSpeed);
            }
        }
    }

    void captureTotem(float captureSpeed)
    {
        if (transform.rotation.eulerAngles.z < 359)
        {
            transform.Rotate(Vector3.forward * captureSpeed);
        }
        else if (_totemOfEagleCaptured == false)
        {
            System.Random rand = new System.Random();
            _playerControler1.IncreseDamage(2);
            _playerControler2.IncreseDamage(2);
            Debug.Log("player damage = " + _playerControler2.DAMAGE);
            if (OnTotemCapured != null)
                OnTotemCapured.Invoke(this, null);
            if (OnEnableSpecialAbility != null)
                OnEnableSpecialAbility.Invoke(this,rand.Next(0,2));
            _totemOfEagleCaptured = true;
        }
    }
    private float totemRange = 4.0f;
    private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();
    private PlayerController _playerControler1;
    private PlayerController _playerControler2;
    private bool _totemOfEagleCaptured = false;
}

