using UnityEngine;
using System.Collections;

public class MushroomController : MonoBehaviour {

    public float SlowPower;
    public float SlowDuration;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController slow = collider.GetComponent<PlayerController>();
            slow.ApplySlow(SlowPower, SlowDuration);
            gameObject.SetActive(false);

        }
    }
}
