using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour
{
    public float Lenght;
    public float ThrowSpeed;
    public float ThrowStrength;

    public bool IsReturning { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsPullingPlayer { get; set; }

    public void Collision(Collision collision)
    {
        revertDirection(collision.gameObject);
    }

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
        Debug.Log("End");
        IsPullingPlayer = false;
        _player.GetComponent<ConstantForce>().force = Vector3.zero;
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
            //_player.GetComponent<Rigidbody>().AddForce(movement * ThrowStrength);
            Debug.Log(movement * ThrowStrength);
            _player.GetComponent<ConstantForce>().force = movement * ThrowStrength;
        }

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
            IsPullingPlayer = true;
        IsMoving = false;
        IsReturning = true;
    }


    private GameObject _player;
}
