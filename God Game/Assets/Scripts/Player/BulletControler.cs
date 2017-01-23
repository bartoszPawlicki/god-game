using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class BulletControler : MonoBehaviour
{
    enum SA : int { normalBullet, fireBullet, pushPowerBullet, freezingBullet }
    public float shotSpeed;
    public float shotStrength;
    public float damage;
    public int SlinkShotCooldown;
    public float ReloadCoodown;
    public int BulletVanishingTime;
    public int amountOfBullets;
    public GameObject totem;
    public AmmoDisplay ammoDisplay;

    //public ArrayList bulletList;
   // public Dictionary<GameObject,bool> bulletDic;
    public List<GameObject> bulletsList;
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
    public float ReloadValue
    {
        get
        {
            return _reloadCD.Loading;
        }
    }


    public int ActualBulletAmount
    {
        get { return _actualBulletAmount; }
        private set
        {
            if (value != _actualBulletAmount)
            {
                _actualBulletAmount = value;
            }
        }
    }

    public AudioSource SlingshotSoundSource;
    void Start ()
    {
        bulletPrefab = Resources.Load("bullet") as GameObject;
        _slinkShotCD = new CooldownProvider(SlinkShotCooldown);
        _reloadCD = new CooldownProvider(ReloadCoodown);
        _bulletCollector = new CooldownProvider(BulletVanishingTime);
        _playerNumber = (int)char.GetNumericValue(transform.gameObject.name[transform.gameObject.name.Length - 1]);
        bulletsList = new List<GameObject>();
        for (int i = 0; i < amountOfBullets; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab) as GameObject;
            newBullet.SetActive(false);
            bulletsList.Add(newBullet);
        }
        ammoDisplay.ammo = amountOfBullets;
        _actualBulletAmount = amountOfBullets;
        _totemActivator = totem.GetComponent<TotemActivator>();
        _totemActivator.OnEnableSpecialAbility += TotemActivator_OnEnableSpecialAbility;
        
    }
	void Update ()
    {
        if (ReloadValue == 100)
        {
            ammoDisplay.ammo = _actualBulletAmount;
            if (Input.GetAxis("Bullet_" + _playerNumber) < -0.5 && SlinkShootValue == 100)
            {
                foreach (var item in bulletsList)
                {
                    if (_actualBulletAmount > 0)
                    {
                        _slinkShotCD.Use();
                        SlingshotSoundSource.Play();
                        _slinkShotCD.Start();
                        _bulletCollector.Use();
                        _bulletCollector.Start();
                        item.SetActive(true); //activate bullet as gameObject
                        item.transform.position = transform.position + gameObject.transform.forward; //set bullet starting point to player position
                        Rigidbody rb = item.GetComponent<Rigidbody>();
                        rb.velocity = gameObject.transform.forward * shotSpeed;
                        _actualBulletAmount--;
                        ammoDisplay.ammo = _actualBulletAmount;
                        item.transform.Translate(new Vector3(0, 0.8f, 0));
                        item.GetComponent<BulletCollisionScript>().damage = damage;
                        item.GetComponent<BulletCollisionScript>().sa = _specialAbility;
                        if (_specialAbility == 2)
                            rb.mass += 10;
                        else
                            rb.mass = 1;
                        Debug.Log("");
                        StartCoroutine(DestroyBullet(item));
                        break;
                    }
                }
            }
        }
        if(_actualBulletAmount == 0)
        {
            _reloadCD.Use();
            _reloadCD.Start();
            ActualBulletAmount = amountOfBullets;
        }

    }
    public IEnumerator DestroyBullet(GameObject item)
    {
        yield return new WaitForSeconds(16.0f);
        item.SetActive(false);
    }
    //public IEnumerator reloadAmmo()
    //{
    //    yield return new WaitForSeconds(8.0f);

    //}
    public void TotemActivator_OnEnableSpecialAbility(object sender, int specialAbility)
    {
        if(specialAbility == (int)SA.freezingBullet)
        {
            _freezingBullet = true;
            _fireBullet = false;
            _pushPowerBullet = false;
            _specialAbility = (int)SA.freezingBullet;
            Debug.Log("sa = freezingBullet");
        }
        if (specialAbility == (int)SA.fireBullet)
        {
            _freezingBullet = false;
            _fireBullet = true;
            _pushPowerBullet = false;
            _specialAbility = (int)SA.fireBullet;
            Debug.Log("sa = fireBullet");
        }
        if (specialAbility == (int)SA.pushPowerBullet)
        {
            _freezingBullet = false;
            _fireBullet = false;
            _pushPowerBullet = true;
            _specialAbility = (int)SA.pushPowerBullet;
            Debug.Log("sa = pushPowerBullet");
        }
        if (specialAbility == (int)SA.normalBullet)
        {
            _freezingBullet = false;
            _fireBullet = false;
            _pushPowerBullet = false;
            _specialAbility = (int)SA.normalBullet;
            Debug.Log("sa = normalBullet");
        }
    }

    private GameObject bulletPrefab;
    private CooldownProvider _slinkShotCD;
    private CooldownProvider _reloadCD;
    private CooldownProvider _bulletCollector;
    private TotemActivator _totemActivator;
    private bool _freezingBullet;
    private bool _fireBullet;
    private bool _pushPowerBullet;
    private int _playerNumber;
    private int _specialAbility;
    private int _actualBulletAmount;
    private bool _isBulletAvailable = true;

    

}
