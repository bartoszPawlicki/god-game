using UnityEngine;
using System.Collections;

public class GodController : MonoBehaviour
{

    public float Speed;
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_2");
        float moveVertical = Input.GetAxis("Vertical_2");
        
        Vector3 movement = new Vector3(moveHorizontal, _rigidbody.velocity.y, moveVertical);
        _rigidbody.MovePosition(transform.position + movement * Speed);
    }
}
