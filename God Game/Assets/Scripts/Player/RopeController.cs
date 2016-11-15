using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour
{
    public float Lenght;
    public float ThrowSpeed;

    public bool IsReturning { get; private set; }
    public bool IsMoving { get; private set; }

    public void Collision(Collision collision)
    {
        revertDirection();
    }

    public void FireRope()
    {
        if(!IsMoving && !IsReturning)
        {
            IsMoving = true;
            enabled = true;
        }
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
	
	void Update ()
    {
        if ((transform.localScale.y <= Lenght && IsMoving) || (transform.localScale.y >= 0 && IsReturning))
        {
            moveScaleAndRotate();
        }
        else if (IsMoving)
            revertDirection();
        else if (IsReturning)
        {
            transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            transform.localRotation = new Quaternion(0, 0, -90, 0);
            transform.localPosition = Vector3.zero;
            IsReturning = false;
            IsMoving = false;
            enabled = false;
        }
	}

    
    private void moveScaleAndRotate()
    {
        int direction = 0;

        if (IsMoving)
            direction = 1;
        else if (IsReturning)
            direction = -1;

        transform.localScale += new Vector3(0, ThrowSpeed, 0) * direction;
        transform.LookAt(_player.transform);
        transform.Rotate(new Vector3(0, 90, -90));

        transform.localPosition = Vector3.zero;      
        transform.localPosition += (_player.transform.position - transform.parent.position).normalized * transform.localScale.y;
    }
    private void revertDirection()
    {
        IsMoving = false;
        IsReturning = true;
    }
    private GameObject _player;
}
