using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodthirstyFlowerHeadController : MonoBehaviour {
    private BloodthirstyFlowerController _parentController;
    private BloodthirstyFlowerControllerTraining _parentControllerTraining;
    // Use this for initialization
    void Start()
    {
        _parentController = transform.parent.GetComponent<BloodthirstyFlowerController>();
        _parentControllerTraining = transform.parent.GetComponent<BloodthirstyFlowerControllerTraining>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (_parentController != null)
            _parentController.OnCollision(collision);
        else
            _parentControllerTraining.OnCollision(collision);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_parentController != null)
            _parentController.OnCollider(other);
        else
            _parentControllerTraining.OnCollider(other);
    }
}
