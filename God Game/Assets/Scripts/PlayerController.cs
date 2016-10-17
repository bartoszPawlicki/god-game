using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    private Rigidbody _rigidbody;
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, _rigidbody.velocity.y, moveVertical);
        _rigidbody.velocity = movement;
    }
}
