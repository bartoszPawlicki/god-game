using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionScript : MonoBehaviour {

    public float damage;
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
            Debug.Log("dostał");
        }
    }
}
