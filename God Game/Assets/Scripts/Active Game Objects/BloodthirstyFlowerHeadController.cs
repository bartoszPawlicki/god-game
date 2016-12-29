using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodthirstyFlowerHeadController : MonoBehaviour {
    private BloodthirstyFlowerController _parentController;

    // Use this for initialization
    void Start()
    {
        _parentController = transform.parent.GetComponent<BloodthirstyFlowerController>();
    }
    void OnCollisionEnter(Collision collision)
    {
        _parentController.OnCollision(collision);
    }

    void OnTriggerEnter(Collider other)
    {
        _parentController.OnCollider(other);
    }
}
