using UnityEngine;
using System.Collections;

public class MushroomController : MonoBehaviour {

    public float SlowPower;
    public float SlowDuration;
    public float ShroomDamage;

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
            PlayerController player = collider.GetComponent<PlayerController>();
            player.ApplySlow(SlowPower, SlowDuration);
            player.HP -= ShroomDamage;
            gameObject.SetActive(false);

        }
    }
}
