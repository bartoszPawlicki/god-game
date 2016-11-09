using UnityEngine;
using System.Collections;
using System;

public class ThunderController : MonoBehaviour
{
    public float SlowPower;
    public float SlowDuration;
    public float TotalLifeTime;

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
        transform.Translate(new Vector3(0, _initialHeight, 0));
        _lifeTime = TotalLifeTime;
        IsFalling = true;
    }

    void Start ()
    {
        enabled = false;
        gameObject.SetActive(false);
        _initialScale = transform.localScale.x;
    }
	
	void Update ()
    {
    }

    void FixedUpdate()
    {
        if (transform.position.y > transform.localScale.y / 2)
            transform.Translate(new Vector3(0, -1, 0) * _fallingSpeed * Time.deltaTime);
        else
            IsFalling = false;

        if (_lifeTime >= 0)
            transform.localScale = new Vector3(_initialScale * _lifeTime / TotalLifeTime, _initialScaleY, _initialScale * _lifeTime / TotalLifeTime);
        else
        {
            transform.localScale = new Vector3(0, _initialScale, 0);
            enabled = false;
            gameObject.SetActive(false);
            if (OnThunderExpired != null)
                OnThunderExpired.Invoke(this, null);
        }
            
        _lifeTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController slow = collider.GetComponent<PlayerController>();
            slow.ApplySlow(SlowPower, SlowDuration);
        }
    }

    void OnTriggerExit(Collider collider)
    {
    }

    private bool _isFalling;
    private float _initialScaleY = 20;
    private float _initialScale;
    private float _lifeTime;
    private float _fallingSpeed = 50;
    private float _initialHeight = 20;
}
