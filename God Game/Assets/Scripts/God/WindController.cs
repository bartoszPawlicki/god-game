using UnityEngine;
using System.Collections;
using System;

public class WindController : MonoBehaviour
{
    public float WindStrength;
    public float TotalLifeTime;


    public event EventHandler OnWindGustExpired;
    public void Strike()
    {
        enabled = true;
        gameObject.SetActive(true);
        _lifeTime = TotalLifeTime;
    }
    void Start ()
    {
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        enabled = false;
        gameObject.SetActive(false);
    }
	
	void FixedUpdate ()
    {
        if (_lifeTime < 0)
        {
            enabled = false;
            gameObject.SetActive(false);
            if (OnWindGustExpired != null)
                OnWindGustExpired.Invoke(this, null);
        }
        
        _lifeTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            float forceHorizontal = collider.transform.position.x - gameObject.transform.position.x;
            float forceVertical = collider.transform.position.z - gameObject.transform.position.z;
            Vector3 movement = new Vector3(forceHorizontal, 0, forceVertical).normalized;
            collider.gameObject.GetComponent<Rigidbody>().AddForce(movement * WindStrength);    
        }
    }

    void OnTriggerStay(Collider collider)
    {
        
    }

    void OnTriggerExit(Collider collider)
    {

    }

    private float _lifeTime;

}
