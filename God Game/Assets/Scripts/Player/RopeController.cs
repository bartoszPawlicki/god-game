using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour
{
    public float Lenght;
    public float ThrowSpeed;

    public bool IsReturning { get; private set; }
    public bool IsMoving { get; private set; }

    void Start ()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player != transform.parent.gameObject)
                _player = player;
        }
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
            IsReturning = false;
            enabled = false;
        }
	}



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _player.gameObject)
            revertDirection();
    }

    void OnCollisionExit(Collision collision)
    {

    }

    void FireRope()
    {
        IsMoving = true;
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
        transform.localPosition += (_player.transform.position - transform.parent.position).normalized * ThrowSpeed * direction;
    }
    private void revertDirection()
    {
        IsMoving = false;
        IsReturning = true;
    }
    private GameObject _player;
}
