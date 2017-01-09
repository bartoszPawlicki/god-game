using UnityEngine;
using System.Collections;
using System;

public class WaterGeyserController : MonoBehaviour
{
    public float WaterGeyserDamage;
    public float WaterGeyserKnockUpStrength;
    public float TotalLifeTime;
    public float WaterGeyserChargingTime;
    public float GushingSpeed;
    public float SlowPower;
    public float SlowDuration;

    public bool IsCharged
    {
        get { return _isCharged; }
        private set
        {
            if (value != _isCharged)
            {
                _isCharged = value;
                if (value)
                    if (OnWaterGeyserCharged != null)
                        OnWaterGeyserCharged.Invoke(this, null);
            }
        }
    }




    public event EventHandler OnWaterGeyserCharged;
    public event EventHandler OnWaterGeyserExpired;



    public void Strike()
    {
        _chargingTime = WaterGeyserChargingTime;
        IsCharged = false;
        enabled = true;
        gameObject.SetActive(true);
        _lifeTime = TotalLifeTime;
        waterGeyserRaycastFunc();
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<Renderer>().enabled = false;
        _waterStream.SetActive(false);
        _waterBubbles.SetActive(true);
    }
    void Start ()
    {
        OnWaterGeyserCharged += WaterGeyserController_OnWaterGeyserCharged;
        OnWaterGeyserExpired += WaterGeyserController_OnWaterGeyserExpired;
        _parent = gameObject.transform.parent;
        _waterBubbles = transform.FindChild("Bubbles").gameObject;
        _waterStream = transform.FindChild("Water").gameObject;
        _isCharged = false;
        enabled = false;
        gameObject.SetActive(false);
        _waterSoundSource = gameObject.GetComponent<AudioSource>();
        
    }

    

    private void WaterGeyserController_OnWaterGeyserCharged(object sender, EventArgs e)
    {
        _waterBubbles.SetActive(false);
        gameObject.GetComponent<Renderer>().enabled = true;
        _waterStream.SetActive(true);
        _waterSoundSource.Play();
    }

    private void WaterGeyserController_OnWaterGeyserExpired(object sender, EventArgs e)
    {
        gameObject.transform.SetParent(_parent);
        gameObject.transform.position = new Vector3(gameObject.transform.parent.transform.position.x, -20, gameObject.transform.parent.transform.position.z);
    }

    private void waterGeyserRaycastFunc()
    {
        Vector3 _rayOrigin = gameObject.transform.position + new Vector3 (0, 60, 0);
        if (Physics.Raycast(_rayOrigin, Vector3.down, out _waterGeyserRaycastHit, 100, _groundLayerMask))
        {
           
                gameObject.transform.position = new Vector3(gameObject.transform.parent.transform.position.x, _waterGeyserRaycastHit.point.y - gameObject.transform.localScale.y / 2 - 0.5f, gameObject.transform.parent.transform.position.z);
                _waterBubbles.transform.position = new Vector3(gameObject.transform.parent.transform.position.x, _waterGeyserRaycastHit.point.y - _waterBubbles.transform.localScale.y / 2 - 0.25F, gameObject.transform.parent.transform.position.z);

            
        }
    }

    void Update()
    {
        transform.localScale = transform.localScale;
    }

    void FixedUpdate ()
    {
        

        if (_chargingTime < 0)
        {
            IsCharged = true;
        }

        _chargingTime -= Time.deltaTime;

        if (_lifeTime < 0)
        {
            enabled = false;
            gameObject.SetActive(false);
            if (OnWaterGeyserExpired != null)
                OnWaterGeyserExpired.Invoke(this, null);
        }
        
        _lifeTime -= Time.deltaTime;

        if (_isCharged)
        {
            if (transform.position.y < _waterGeyserRaycastHit.point.y)
                transform.Translate(Vector3.up * GushingSpeed * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (_isCharged)
            {
                collider.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, WaterGeyserKnockUpStrength, 0));

                PlayerController player = collider.GetComponent<PlayerController>();
                player.ApplySlow(SlowPower, SlowDuration);

                player.HP -= WaterGeyserDamage;
            }
        }
        else if(collider.gameObject.tag == "TrainingBall")
        {
            if (_isCharged)
            {
                collider.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, WaterGeyserKnockUpStrength, 0));
            }
        }
    }

    private float _lifeTime;
    private float _chargingTime;
    private bool _isCharged;
    private Transform _parent;
    private RaycastHit _waterGeyserRaycastHit;
    private GameObject _waterBubbles;
    private GameObject _waterStream;
    private int _groundLayerMask = 1 << 8;
    public AudioClip _waterSoundClip;
    private AudioSource _waterSoundSource;
    
}
