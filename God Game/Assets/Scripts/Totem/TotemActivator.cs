using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TotemActivator : MonoBehaviour
{
    private GameObject[] _players;
    public float captureSpeed = 0.2f;
    private float totemRange = 4.0f;
    private float messageDisplayTime = 0f;
    public bool totemCapturedFlag { get; set; }
    private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();
    private bool _totemOfEagleCaptured = false;


    public event EventHandler OnTotemCapured;
    void Start()
    {
        totemCapturedFlag = false;
        _players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in _players)
        {
            if (player != gameObject)
                _playerColliding.Add(player, false);
        }
    }
    void FixedUpdate()
    {
        float captureTotem1 = Input.GetAxis("CaptureTotem_1");
        float captureTotem2 = Input.GetAxis("CaptureTotem_2");

        if (captureTotem1 == 1 && captureTotem2 == 1)
        {
            foreach (GameObject player in _players)
            {
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) < totemRange && gameObject.transform.position.y - player.transform.position.y < 0.5f && _totemOfEagleCaptured == false)
                {
                    if(transform.rotation.eulerAngles.z < 359)
                    {
                        transform.Rotate(Vector3.forward * captureSpeed);                
                    }
                    else
                    {
                        _totemOfEagleCaptured = true;
                        totemCapturedFlag = true;
                        messageDisplayTime = 4f;
                        if (OnTotemCapured != null)
                            OnTotemCapured.Invoke(this, null);
                    }
                }
            }
        }
        messageDisplayTime -= Time.deltaTime;
        if(messageDisplayTime <= 0)
        {
            totemCapturedFlag = false;
        }
    }
    void OnGUI()
    {
        GUI.skin.label.fontSize = 70;
        if (totemCapturedFlag == true)
        {
            GUI.Label(new Rect(Screen.width/2, Screen.height /3, 500f, 500f), "Totem Captured");
        }
        
    }

}

