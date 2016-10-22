using UnityEngine;
using System.Collections;

public class ThunderControllerScript : MonoBehaviour
{
    float ThunderLifeTime;
    float initialXScale;
    float initialZScale;

    private float SlowDuration = 2F;
    private float SlowPower = 0.3F;
    private Rigidbody _rigidbody;
    
    public float Speed;
    // Use this for initialization
    void Start ()
    {
        ThunderLifeTime = 4F;
        initialXScale = gameObject.transform.localScale.x;
        initialZScale = gameObject.transform.localScale.z;
        _rigidbody = GetComponent<Rigidbody>();
    }
	
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
            slow.ApplySlow(SlowPower, SlowDuration);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player") Debug.Log("Thunder is no longer colliding with player");
    }
	// Update is called once per frame
	void Update ()
    {
        ThunderLifeTime -= Time.deltaTime;
        if (ThunderLifeTime < 0) gameObject.SetActive(false);

        //thunder's falling
        if (transform.position.y > transform.localScale.y / 2 + 1) gameObject.transform.Translate(new Vector3(0, -15F * Time.deltaTime, 0), Space.Self);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //thunder's diminishing over time
        gameObject.transform.localScale = new Vector3(initialXScale * ThunderLifeTime / 3F, 5, initialZScale * ThunderLifeTime / 3F);

        Vector3 movement = transform.position + new Vector3(moveHorizontal, 0, moveVertical) * Speed * Time.deltaTime;
        _rigidbody.MovePosition(movement);
        
    }
}
