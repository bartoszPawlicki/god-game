using UnityEngine;
using System.Collections;

public class ThunderIndicatorController2 : MonoBehaviour
{
    private GodController godController;
    public float Speed;

    // Use this for initialization
    void Start ()
    {
        godController = GameObject.FindGameObjectWithTag("God").GetComponent<GodController>();
        godController.ThunderIndicatorLifetimeTimer = godController.ThunderIndicatorLifeTime;
    }

    // Update is called once per frame
    void Update ()
    {
        godController.ThunderIndicatorLifetimeTimer -= Time.deltaTime;
        if (godController.ThunderIndicatorLifetimeTimer < 0)
        {
            godController.ThunderIndicatorLifetimeTimer = 5F;
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal_God");
        //float moveVertical = Input.GetAxis("Vertical_God");
        //gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * Speed * Time.deltaTime);
    }
}

