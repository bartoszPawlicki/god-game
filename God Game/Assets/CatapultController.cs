using UnityEngine;
using System.Collections;

public class CatapultController : MonoBehaviour
{
    public float SlowPower;
    public float SlowDuration;
    public float CatapultStrength;
    public Vector3 CatapultDirection;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController slow = collider.GetComponent<PlayerController>();
            slow.ApplySlow(SlowPower, SlowDuration);

            collider.gameObject.GetComponent<Rigidbody>().AddForce(CatapultDirection.normalized * CatapultStrength);
        }
    }
}
