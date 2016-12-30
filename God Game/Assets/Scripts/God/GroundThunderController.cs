using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundThunderController : MonoBehaviour {

    public float ThunderDamage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            //player.ApplyRisingSlow(SlowPower, SlowDuration);

            player.HP -= ThunderDamage;

        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            //player.ApplyRisingSlow(SlowPower, SlowDuration);

            player.HP -= ThunderDamage;
        }
    }
}
