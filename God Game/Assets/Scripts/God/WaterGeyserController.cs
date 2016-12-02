using UnityEngine;
using System.Collections;
using System;

public class WaterGeyserController : MonoBehaviour
{
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
    }
    void Start ()
    {
        OnWaterGeyserCharged += WaterGeyserController_OnWaterGeyserCharged;
        OnWaterGeyserExpired += WaterGeyserController_OnWaterGeyserExpired;
        _parent = gameObject.transform.parent;
        //_waterBubbles = transform.FindChild("Bubbles").gameObject;
        _isCharged = false;
        enabled = false;
        gameObject.SetActive(false);
    }

    

    private void WaterGeyserController_OnWaterGeyserCharged(object sender, EventArgs e)
    {
        //_waterBubbles.SetActive(false);
    }

    private void WaterGeyserController_OnWaterGeyserExpired(object sender, EventArgs e)
    {
        gameObject.transform.SetParent(_parent);
        gameObject.transform.position = new Vector3(gameObject.transform.parent.transform.position.x, -20, gameObject.transform.parent.transform.position.z);
    }

    private void waterGeyserRaycastFunc()
    {
        Vector3 _rayOrigin = gameObject.transform.position + new Vector3 (0, 30, 0);
        if (Physics.Raycast(_rayOrigin, Vector3.down, out _waterGeyserRaycastHit, 50))
        {
            if (!_waterGeyserRaycastHit.collider.gameObject.CompareTag("Player"))
            {
                gameObject.transform.position = new Vector3 (gameObject.transform.parent.transform.position.x, _waterGeyserRaycastHit.point.y - gameObject.transform.localScale.y/2 -0.5f, gameObject.transform.parent.transform.position.z);
                //_waterBubbles.transform.position = new Vector3(gameObject.transform.parent.transform.position.x, _waterGeyserRaycastHit.point.y - _waterBubbles.transform.localScale.y / 2 - 0.5f, gameObject.transform.parent.transform.position.z);

            }
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
            if (transform.position.y < 1)
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

                PlayerController slow = collider.GetComponent<PlayerController>();
                slow.ApplySlow(SlowPower, SlowDuration);
            }

        }
    }

    private float _lifeTime;
    private float _chargingTime;
    private bool _isCharged;
    private Transform _parent;
    private RaycastHit _waterGeyserRaycastHit;
    //private GameObject _waterBubbles;

}
