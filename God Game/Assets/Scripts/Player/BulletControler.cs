using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class BulletControler : MonoBehaviour
{
    enum SA : int { freezingBullet, fireBullet, pushPowerBullet}
    public float shotRange;
    public float shotSpeed;
    public float shotStrength;
    public float damage;
    public int SlinkShotCooldown;
    public int BulletVanishingTime;
    public GameObject totem;
    //public ArrayList bulletList;
    public Dictionary<GameObject,bool> bulletDic;
    public int Direction { get; private set; }
    public float SlinkShootValue
    {
        get
        {
            return _slinkShotCD.Loading;
        }
    }
    public float BulletValue
    {
        get
        {
            return _bulletCollector.Loading;
        }
    }
    void Start ()
    {
        bulletPrefab = Resources.Load("bullet") as GameObject;
        _slinkShotCD = new CooldownProvider(SlinkShotCooldown);
        _bulletCollector = new CooldownProvider(BulletVanishingTime);
        _playerNumber = (int)char.GetNumericValue(transform.gameObject.name[transform.gameObject.name.Length - 1]);
        bulletDic = new Dictionary<GameObject, bool>();
        for (int i = 0; i < _amountOfBullets; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab) as GameObject;
            newBullet.SetActive(false);
            bulletDic.Add(newBullet,true);
        }
        _totemActivator = totem.GetComponent<TotemActivator>();
        _totemActivator.OnEnableSpecialAbility += TotemActivator_OnEnableSpecialAbility;
    }
	void Update ()
    {
        if (Input.GetAxis("Bullet_" + _playerNumber) < -0.5 && SlinkShootValue == 100)
        {
            foreach (var item in bulletDic)
            {
                if(item.Value)
                {
                    _slinkShotCD.Use();
                    item.Key.SetActive(true); //activate bullet as gameObject
                    item.Key.transform.position = transform.position + gameObject.transform.forward; //set bullet starting point to player position
                    Rigidbody rb = item.Key.GetComponent<Rigidbody>();
                    rb.velocity = gameObject.transform.forward * shotSpeed;
                    _slinkShotCD.Start();
                    _bulletCollector.Use();
                    _bulletCollector.Start();
                    bulletDic[item.Key] = false; //this bullet in now on cooldown
                    Debug.Log("szczeliłem kulką");
                    break;
                }
            }
        }
        if(BulletValue == 100)
        {
            foreach (var item in bulletDic)
            {
                if(item.Value == false)
                {
                    bulletDic[item.Key] = true;
                    item.Key.SetActive(false);
                    Debug.Log("kulka unicestwiona xD");
                    break;
                }
               
            }
        }
	}
    public void TotemActivator_OnEnableSpecialAbility(object sender, int specialAbility)
    {
        if(specialAbility == (int)SA.freezingBullet)
        {
            _freezingBullet = true;
            _fireBullet = false;
            _pushPowerBullet = false;
            Debug.Log("sa = freezingBullet");
        }
        if (specialAbility == (int)SA.fireBullet)
        {
            _freezingBullet = false;
            _fireBullet = true;
            _pushPowerBullet = false;
            Debug.Log("sa = fireBullet");
        }
        if (specialAbility == (int)SA.pushPowerBullet)
        {
            _freezingBullet = false;
            _fireBullet = false;
            _pushPowerBullet = true;
            Debug.Log("sa = pushPowerBullet");
        }
    }

    private GameObject bulletPrefab;
    private CooldownProvider _slinkShotCD;
    private CooldownProvider _bulletCollector;
    private TotemActivator _totemActivator;
    private bool _freezingBullet;
    private bool _fireBullet;
    private bool _pushPowerBullet;
    private int _playerNumber;
    private int _amountOfBullets = 5;
    private bool _isBulletAvailable = true;

}
