using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Utils;

public class BulletControler : MonoBehaviour
{

    public float shotRange;
    public float shotSpeed;
    public float shotStrength;
    public int damage;

    public int Direction { get; private set; }
    void Start ()
    {
        bulletPrefab = Resources.Load("bullet") as GameObject;
        _playerNumber = (int)char.GetNumericValue(transform.gameObject.name[transform.gameObject.name.Length - 1]);
    }
	void Update ()
    {
      //  if(Input.GetMouseButtonDown(0))
        if (Input.GetAxis("Bullet_" + _playerNumber) == 1)
        {
            Debug.Log("shooot@");
            GameObject newBullet = Instantiate(bulletPrefab) as GameObject;
            newBullet.transform.position = transform.position + gameObject.transform.forward;
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            rb.velocity = gameObject.transform.forward * shotSpeed;
        }
	}

    private GameObject bulletPrefab;
    private int _playerNumber;

}
