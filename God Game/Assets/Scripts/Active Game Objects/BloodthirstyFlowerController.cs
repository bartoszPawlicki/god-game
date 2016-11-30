using UnityEngine;
using System.Collections;

public class BloodthirstyFlowerController : MonoBehaviour
{
    public float Range;
    public float Speed;
    void Start ()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");

        _respawnManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<RespawnManager>();

        _originPosition = transform.position;
        _originScale = transform.localScale;
    }
	
	void Update ()
    {
        if(_isMoving)
        {
            if (transform.localScale.z <= Range || _direction == -1)
            {
                transform.localScale += new Vector3(0, 0, 0.1f) * _direction * Speed;

                float lenght = transform.localScale.z / 2;
                float distanceBetweenPoints = Vector3.Distance(_originPosition, _playerInRange.transform.position);
                transform.position = Vector3.Lerp(_originPosition, _playerInRange.transform.position, lenght / distanceBetweenPoints);
            }
            else
                _direction = -1;
            

            if (transform.localScale.z <= _originScale.z)
            {
                _direction = 1;
                transform.localScale = _originScale;
                transform.position = _originPosition;
                _isMoving = false;
            }
        }

        if(_isPlayerInRange)
        {
            if (!_isMoving)
            {
                _isMoving = true;
            }

            transform.LookAt(_playerInRange.transform.position);

            if (Vector3.Distance(_playerInRange.transform.position, _originPosition) > Range)
            {
                _isPlayerInRange = false;
                _direction = -1;
            }
        }
        else
        {
            float distance;
            float minDistance = float.MaxValue;

            foreach (var item in _players)
            {
                distance = Vector3.Distance(item.transform.position, _originPosition);
                if (distance < Range && distance < minDistance && item.GetComponent<PlayerController>().isActiveAndEnabled)
                {
                    minDistance = distance;
                    _playerInRange = item;
                    _isPlayerInRange = true;
                    _direction = 1;
                    _isMoving = true;
                }
            }
        }
    }
    
    void EatPlayer()
    {
        _respawnManager.StartRespawn(_playerInRange);
        _isPlayerInRange = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        _direction = -1;
        if (collision.collider.tag == "Player")
            EatPlayer();
    }

    void OnTriggerEnter(Collider other)
    {
        _direction = -1;
        if (other.attachedRigidbody.tag == "Player")
            EatPlayer();
    }

    private bool _isMoving;
    private int _direction;
    private bool _isPlayerInRange;
    private GameObject _playerInRange;
    private Vector3 _originPosition;
    private Vector3 _originScale;
    private GameObject[] _players;
    private RespawnManager _respawnManager;
}
