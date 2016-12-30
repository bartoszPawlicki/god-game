using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionScript : MonoBehaviour {

    public float damage;
    //TODO pobrac dmg od gracza i trzyamc w kulce;
	// Use this for initialization
	void Start ()
    {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Temple")
        {
            TempleController dmg = collision.collider.GetComponent<TempleController>();
            dmg.ApplyDamage(damage);
        }
        if (collision.collider.gameObject.tag == "GroundGod")
        {
            GodPride dmg = collision.collider.GetComponent<GodPride>();
            dmg.ApplyDamage(damage);
            Debug.Log("chuj");
        }


    }
    
}
