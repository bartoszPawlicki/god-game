﻿using UnityEngine;
using System.Collections;

public class GodController : MonoBehaviour
{

    public float Speed;
    public GameObject ThunderIndicatorPrefab;
    private float ThunderCooldown = -1F;
    void Start()
    {
        
    }
    void UseThunderSkill()
    {
        Instantiate(ThunderIndicatorPrefab, new Vector3(gameObject.transform.position.x, 0.01F, gameObject.transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");
        
        //Vector3 movement = new Vector3(moveHorizontal, _rigidbody.velocity.y, moveVertical);
        gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * Speed * Time.deltaTime, Space.Self);

        ThunderCooldown -= Time.deltaTime;
        if (Input.GetAxis("Fire_Thunder") == 1)
        {
            if (ThunderCooldown < 0)
            {
                UseThunderSkill();
                ThunderCooldown = 5F;
            }
        } 
    }
}
