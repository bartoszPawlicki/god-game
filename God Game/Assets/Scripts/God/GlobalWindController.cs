using UnityEngine;
using System.Collections;
using System;

public class GlobalWindController : MonoBehaviour
{
    public float GlobalWindStrength;
    public float TotalLifeTime;

    public event EventHandler OnGlobalWindExpired;
    // Use this for initialization
    void Start ()
    {
        enabled = false;
        gameObject.SetActive(false);
        _players = GameObject.FindGameObjectsWithTag("Player");
        _lifeTime = TotalLifeTime;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (_lifeTime < 0)
        {
            enabled = false;
            gameObject.SetActive(false);
            if (OnGlobalWindExpired != null)
                OnGlobalWindExpired.Invoke(this, null);
        }

        foreach (var player in _players)
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(GlobalWindStrength, 0, 0));
        }

        _lifeTime -= Time.deltaTime;
    }

    public void Strike()
    {
        enabled = true;
        gameObject.SetActive(true);
        _lifeTime = TotalLifeTime;
    }

    private float _lifeTime;
    private GameObject[] _players;
}
