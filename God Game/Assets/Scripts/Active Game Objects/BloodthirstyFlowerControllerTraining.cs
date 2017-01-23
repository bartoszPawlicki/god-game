using UnityEngine;
using System.Collections;

public class BloodthirstyFlowerControllerTraining : MonoBehaviour
{
    public AudioSource FlowerEatingAudioSource;
    public float Range;
    public float Speed;
    /// <summary>
    /// In range 0 - 1
    /// </summary>
    public float HP
    {
        get { return _hp; }
        set
        {
            if(value != _hp)
            {
                _hp = value;
                Range *= _hp;
                foreach(Material m in _material)
                {
                    m.color = new Color(_hp, _hp, _hp , 1);
                }
                if (_hp <= 0)
                {
                    _hp = 0;
                    enabled = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
    void Start ()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < 3; i++)
        {
            _material[i] = transform.GetChild(i).GetComponent<Renderer>().material;
        }

       _respawnManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<RespawnTutorialManager>();

        _originPosition = transform.position;

        _stemExpanding = transform.FindChild("stem_expanding");
        _stemHead = transform.FindChild("head");
        
        _originStemScale = _stemExpanding.localScale;
        
        HP = 1;
    }
	
	void Update ()
    {
        if(_isMoving)
        {
            if (_stemExpanding.localScale.z <= Range || _direction == -1)
            {
                _stemExpanding.localScale += new Vector3(0, 0, 0.1f) * _direction * Speed;
                _stemHead.localPosition = new Vector3(0, 0, (_stemExpanding.localScale.z - 1) / 2);
                float lenght = _stemExpanding.localScale.z / 2;
                float distanceBetweenPoints = Vector3.Distance(_originPosition, _playerInRange.transform.position);
                //transform.position = Vector3.Lerp(_originPosition, _playerInRange.transform.position, lenght / distanceBetweenPoints);
            }
            else
                _direction = -1;
            

            if (_stemExpanding.localScale.z <= _originStemScale.z)
            {
                _direction = 1;
                _stemExpanding.localScale = _originStemScale;
                _stemHead.localPosition = Vector3.zero;
                transform.localEulerAngles = new Vector3(0, 0, 0);
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
        if(isActiveAndEnabled)
        {
            FlowerEatingAudioSource.Play();
            _respawnManager.StartRespawn(_playerInRange);
            _isPlayerInRange = false;
        }
    }

    public void OnCollision(Collision collision)
    {
        _direction = -1;
        if (collision.collider.tag == "Player")
            EatPlayer();

        //TODO: 0.3f powinno być ustawiane z zewnątrz
        if (collision.collider.tag == "Bullet")
            HP -= 0.3f;
    }

    public void OnCollider(Collider other)
    {
        _direction = -1;
        if (other.attachedRigidbody.tag == "Player")
            EatPlayer();
    }

    private Color _originColor;
    private Material[] _material = new Material[3];
    private float _hp;
    private bool _isMoving;
    private int _direction;
    private bool _isPlayerInRange;
    private GameObject _playerInRange;
    private Vector3 _originPosition;
    private Vector3 _originStemScale;
    private GameObject[] _players;
    private RespawnTutorialManager _respawnManager;
    private Transform _stemExpanding;
    private Transform _stemHead;
}
