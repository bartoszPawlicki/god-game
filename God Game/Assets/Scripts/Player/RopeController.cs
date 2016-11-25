using UnityEngine;
using Assets.Scripts.Utils;
using System;

public class RopeController : MonoBehaviour
{
    public float Lenght;
    public float ThrowSpeed;
    public float ThrowStrength;

    public event EventHandler OnRopeReturned;
    public int Direction { get; private set; }

    public void FireRope()
    {
        enabled = true;
        Direction = 1;
    }
    public void EndPulling()
    {
        Direction = -1;
        if(_isPullingPlayer)
        {
            _playerConstantForce.force -= _constantForce;
            _constantForce = Vector3.zero;
            _isPullingPlayer = false;
        }
    }
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player != transform.parent.gameObject)
                _player = player;
        }
        _parent = transform.parent.gameObject;

        _playerConstantForce = _player.GetComponent<ConstantForce>();
        
        _originScale = transform.localScale;

        enabled = false;
    }

    void FixedUpdate()
    {
        if (!_isPullingPlayer)
        {
            if (transform.localScale.y <= Lenght || Direction == -1)
                transform.localScale += new Vector3(0, 0.1f, 0) * Direction * ThrowSpeed;
            else
                Direction = -1;

            DrawRopeBetween(_parent.transform.position, _player.transform.position);

            //It has to be after all scale actions
            if (transform.localScale.y <= 0)
            {
                transform.localScale = _originScale;
                if (OnRopeReturned != null)
                    OnRopeReturned.Invoke(this, null);
                enabled = false;
            }
        }
        else
        {
            float forceHorizontal = _parent.transform.position.x - _player.transform.position.x;
            float forceUp = _parent.transform.position.y - _player.transform.position.y;
            float forceVertical = _parent.transform.position.z - _player.transform.position.z;
            Vector3 movement = new Vector3(forceHorizontal, forceUp, forceVertical).normalized;
            _playerConstantForce.force -= _constantForce;
            _constantForce = movement * ThrowStrength;
            _playerConstantForce.force += _constantForce;

            transform.localScale = new Vector3(_originScale.x, Vector3.Distance(_parent.transform.position, _player.transform.position) / 2, _originScale.z);
            DrawRopeBetween(_parent.transform.position, _player.transform.position);
        }
        
    }

    void DrawRopeBetween(Vector3 startPoint, Vector3 endPoint)
    {
        float lenght = transform.localScale.y;
        float distanceBetweenPoints = Vector3.Distance(startPoint, endPoint);
        transform.position = Vector3.Lerp(startPoint, endPoint, lenght / distanceBetweenPoints);
        transform.LookAt(endPoint);
        transform.Rotate(new Vector3(90, 0, 0));
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _isPullingPlayer = true;
        }
        else if (!_isPullingPlayer)
            EndPulling();
    }

    private bool _isPullingPlayer;
    private Vector3 _constantForce = Vector3.zero;
    private Vector3 _originScale;
    private GameObject _parent;
    private GameObject _player;
    private ConstantForce _playerConstantForce;
}
