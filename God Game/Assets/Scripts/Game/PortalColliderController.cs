using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class PortalColliderController : MonoBehaviour
{
    public event EventHandler OnPlayersPassed;
    public Collider Collider { get; private set; }
    void Start ()
    {
        foreach (var player in GameContener.Players)
        {
            _playerColliding.Add(player, false);
        }

        Collider = GetComponent<Collider>();
    }

    void Update ()
    {
        bool passed = true;
        foreach (var item in _playerColliding.Values)
        {
            if (!item)
                passed = false;
        }

        if(passed)
        {
            foreach (var item in _playerColliding.Keys.ToList())
            {
                _playerColliding[item] = false;
            }
            
            if (OnPlayersPassed != null)
                OnPlayersPassed.Invoke(this, null);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerColliding[collision.gameObject] = true;
            collision.gameObject.SetActive(false);
        }
    }

    private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();
}
