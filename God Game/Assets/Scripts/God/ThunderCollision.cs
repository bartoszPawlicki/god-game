using UnityEngine;
using System.Collections;

public class ThunderCollision : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") Debug.Log("Thunder struck player");
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Thunder colliding with player");
            PlayerController slow = collider.GetComponent<PlayerController>();
            //slow.ApplySlow(SlowPower, SlowDuration);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player") Debug.Log("Thunder is no longer colliding with player");
    }
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
