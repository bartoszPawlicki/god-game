using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowCompanionBehaviour : MonoBehaviour
{
    public float ThrowStrength;

    void Start ()
    {
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in _players)
        {
            if (item != gameObject)
                _playerColliding.Add(item, false);
        }
    }
	
	void Update ()
    {
        float throwCompanion1 = Input.GetAxis("Throw_1");
        float throwCompanion2 = Input.GetAxis("Throw_2");

        Debug.Log(throwCompanion1);
        Debug.Log(throwCompanion2);

        if (throwCompanion1 == 1 && throwCompanion2 == 1)
        {
            foreach (var item in _playerColliding)
            {
                if (item.Value)
                {
                    float forceHorizontal = item.Key.transform.position.x - transform.position.x;
                    float forceVertical = item.Key.transform.position.z - transform.position.z;
                    Vector3 movement = new Vector3(forceHorizontal, 0, forceVertical).normalized;
                    item.Key.GetComponent<Rigidbody>().AddForce(movement * ThrowStrength);
                }
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (_playerColliding.ContainsKey(collision.gameObject))
            _playerColliding[collision.gameObject] = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (_playerColliding.ContainsKey(collision.gameObject))
            _playerColliding[collision.gameObject] = false;
    }

    private Dictionary<GameObject, bool> _playerColliding = new Dictionary<GameObject, bool>();
}
