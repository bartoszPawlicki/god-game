using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmolationController : MonoBehaviour {

    public float ImmolationDamage;

	void Start ()
    {
		
	}
	

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            player.HP -= ImmolationDamage;

        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            //player.ApplyRisingSlow(SlowPower, SlowDuration);

            player.HP -= ImmolationDamage;
        }
    }


}
