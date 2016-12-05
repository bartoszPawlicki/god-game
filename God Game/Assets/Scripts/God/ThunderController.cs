using UnityEngine;
using System.Collections;
using System;

public class ThunderController : MonoBehaviour
{
    public float SlowPower;
    public float SlowDuration;
    public float TotalLifeTime;
    public float FallingSpeed;

    public bool IsFalling
    {
        get { return _isFalling; }
        private set
        {
            if(value != _isFalling)
            {
                _isFalling = value;
                if (!value)
                    if (OnThunderStruck != null)
                        OnThunderStruck.Invoke(this, null);
            }
        }
    }

    public event EventHandler OnThunderExpired;
    public event EventHandler OnThunderStruck;
    public void Strike()
    {
        enabled = true;
        gameObject.SetActive(true);
        thunderRaycastFunc();

        //_thunderCharge.SetActive(true);
        //_thunderCharge.transform.position = new Vector3(transform.position.x, _thunderRaycastHit.point.y + 1, transform.position.z);
        //_thunderCharge.transform.SetParent(null);

        transform.Translate(new Vector3(0, _initialHeight, 0));
        _lifeTime = TotalLifeTime;
        IsFalling = true;
        
        
    }

    void Start ()
    {
        //_thunderCharge = gameObject.transform.FindChild("ThunderCharge").gameObject;
        //_thunderCharge.SetActive(false);
        OnThunderStruck += ThunderController_OnThunderStruck;
        enabled = false;
        gameObject.SetActive(false);
        _initialScaleX = transform.localScale.x;
        _initialScaleY = transform.localScale.y;
    }

    private void ThunderController_OnThunderStruck(object sender, EventArgs e)
    {
        //_thunderCharge.transform.SetParent(gameObject.transform);
        //_thunderCharge.SetActive(false);
    }

    void FixedUpdate()
    {
        if (transform.position.y > _thunderRaycastHit.point.y + transform.localScale.y / 2 - 5)
            transform.Translate(Vector3.down * FallingSpeed * Time.deltaTime);
        else
            IsFalling = false;

        if (_lifeTime >= 0)
            transform.localScale = new Vector3(_initialScaleX * _lifeTime / TotalLifeTime, _initialScaleY, _initialScaleX * _lifeTime / TotalLifeTime);
        else
        {
            transform.localScale = new Vector3(0, _initialScaleX, 0);
            enabled = false;
            gameObject.SetActive(false);
            if (OnThunderExpired != null)
                OnThunderExpired.Invoke(this, null);
        }
            
        _lifeTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController slow = collider.GetComponent<PlayerController>();
            slow.ApplyRisingSlow(SlowPower, SlowDuration);
            
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController slow = collider.GetComponent<PlayerController>();
            slow.ApplyRisingSlow(SlowPower, SlowDuration);
        }
    }

    private void thunderRaycastFunc()
    {
        Vector3 _rayOrigin = gameObject.transform.position + new Vector3(0, 60, 0);
        Physics.Raycast(_rayOrigin, Vector3.down, out _thunderRaycastHit, 100, _groundLayerMask);        
    }

    private bool _isFalling;
    private float _initialScaleY;
    private float _initialScaleX;
    private float _lifeTime;
    //private GameObject _thunderCharge;

    private float _initialHeight = 5;
    private RaycastHit _thunderRaycastHit;
    private int _groundLayerMask = 1 << 8;
}
