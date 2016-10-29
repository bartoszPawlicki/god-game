using UnityEngine;
using System.Collections;

public class ThunderIndicator : MonoBehaviour
{
    private float IndicatorLifeTime;
    public float Speed;

	// Use this for initialization
	void Start ()
    {
        IndicatorLifeTime = 5F;

    }
	
	// Update is called once per frame
	void Update ()
    {
        IndicatorLifeTime -= Time.deltaTime;
        if (IndicatorLifeTime < 0) gameObject.SetActive(false);
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");
        gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * Speed * Time.deltaTime, Space.Self);
    }
}
