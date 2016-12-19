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
    public int SlinkShotCooldown;

    public int Direction { get; private set; }
    public float SlinkShootValue
    {
        get
        {
            return _slinkShotCD.Loading;
        }
    }
    void Start ()
    {
        bulletPrefab = Resources.Load("bullet") as GameObject;
        _slinkShotCD = new CooldownProvider(SlinkShotCooldown);
        _playerNumber = (int)char.GetNumericValue(transform.gameObject.name[transform.gameObject.name.Length - 1]);
    }
	void Update ()
    {
        if (Input.GetAxis("Bullet_" + _playerNumber) == 1 && SlinkShootValue == 100)
        {
            _slinkShotCD.Use();
            GameObject newBullet = Instantiate(bulletPrefab) as GameObject;
            newBullet.transform.position = transform.position + gameObject.transform.forward;
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            rb.velocity = gameObject.transform.forward * shotSpeed;
            _slinkShotCD.Start();
        }
	}

    private GameObject bulletPrefab;
    private CooldownProvider _slinkShotCD;
    private int _playerNumber;

}
