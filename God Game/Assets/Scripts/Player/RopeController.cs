using UnityEngine;
using System.Collections;
using System;

public class RopeController : MonoBehaviour
{
    public float Lenght;
    public float ThrowSpeed;
    public float ThrowStrength;

    public bool IsReturning { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsPullingPlayer { get; set; }

    public void FireRope()
    {
        if(!IsMoving && !IsReturning)
        {
            IsMoving = true;
            enabled = true;
        }
    }
    public void EndPulling()
    {
        IsPullingPlayer = false;
        _player.GetComponent<ConstantForce>().force = Vector3.zero;
        transform.localScale = new Vector3(0.05f, 0, 0.05f);
    }
    void Start ()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player != transform.parent.gameObject)
                _player = player;
        }

        enabled = false;
    }
	
	void FixedUpdate ()
    {
        if (IsPullingPlayer)
        {
            float forceHorizontal = transform.parent.position.x - _player.transform.position.x;
            float forceVertical = transform.parent.position.z - _player.transform.position.z;
            Vector3 movement = new Vector3(forceHorizontal, 0, forceVertical).normalized;
            _player.GetComponent<ConstantForce>().force = movement * ThrowStrength;

            Vector3 firstPoint = transform.parent.position;
            Vector3 secondPoint = _player.transform.position;

            transform.parent.LookAt(_player.transform);
            transform.localEulerAngles = new Vector3(0, -90, -90);
            transform.localScale = new Vector3(0.05f, Vector3.Distance(firstPoint, secondPoint) / 2, 0.05f);
            transform.localPosition = new Vector3(0, 0, transform.localScale.y);
        }
        else
        {
            if ((transform.localScale.y <= Lenght && IsMoving) || (transform.localScale.y >= 0 && IsReturning))
            {
                moveScaleAndRotate();
            }
            else if (IsMoving)
                revertDirection(null);
            else if (IsReturning)
            {
                IsReturning = false;
                IsMoving = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        revertDirection(other.gameObject);
    }
    private void moveScaleAndRotate()
    {
        int direction = 0;

        if (IsMoving)
            direction = 1;
        else if (IsReturning)
            direction = -1;

        transform.parent.LookAt(_player.transform);
        transform.localEulerAngles = new Vector3(0, -90, -90);
        transform.localScale += new Vector3(0, direction, 0) * ThrowSpeed;
        transform.localPosition = new Vector3(0,0, transform.localScale.y);

    }

    private void revertDirection(GameObject gameObject)
    {
        if (gameObject == _player)
        {
            IsPullingPlayer = true;
        }
        else
            IsReturning = true;

        IsMoving = false;
    }

    private GameObject _player;
}
