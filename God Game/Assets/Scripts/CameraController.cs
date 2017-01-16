using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject TutorialCamera;
    public GameObject Player1;
    public GameObject Player2;
    public float Distance;
    public event EventHandler OnCameraStopMoving;
    private bool _isInitialMoving;
    public bool IsInitialMoving
    {
        get
        {
            return _isInitialMoving;
        }
        set
        {
            if(value != _isInitialMoving)
            {
                _isInitialMoving = value;
                if (value == false && OnCameraStopMoving != null)
                    OnCameraStopMoving.Invoke(this, null);
            }
        }
    }

    public Vector3 StartPosition { get; set; }

    Vector3 _worldPosition;
    Vector3 _offset;
	// Use this for initialization
	void Start ()
    {
        _heavensGatePostion = GameObject.FindGameObjectWithTag("Gate").transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float distance = (float)(Distance / Math.Sqrt(2));
        distance += (float)Math.Sqrt(Vector3.Distance(Player1.transform.position, Player2.transform.position));
        _offset = new Vector3(0, distance, -distance * 1.5f);
        if (Player1.GetComponent<PlayerController>().isActiveAndEnabled && Player2.GetComponent<PlayerController>().isActiveAndEnabled)
            _worldPosition = Vector3.Lerp(Player1.transform.position, Player2.transform.position, 0.5f);
        else if (Player1.GetComponent<PlayerController>().isActiveAndEnabled)
            _worldPosition = Player1.transform.position;
        else if (Player2.GetComponent<PlayerController>().isActiveAndEnabled)
            _worldPosition = Player2.transform.position;
        else
            _worldPosition = StartPosition;

        if (IsInitialMoving)
        {
            _worldPosition = Vector3.Lerp(_worldPosition, _heavensGatePostion, 1 - _t);
            _t += 0.002f;

            _offset += new Vector3(0, 10 - _t * 10, 0);

            transform.position = _worldPosition + _offset;
            transform.LookAt(Vector3.Lerp(_worldPosition, _heavensGatePostion, 1 - _t));

            if (_t >= 1)
            {
                IsInitialMoving = false;
                _t = 0;
            }
        }
        else
        {
            //tutaj
            //transform.position = CameraPosition(_zeroPoint, _worldPosition, 50, 30);
            transform.position = _worldPosition + _offset;
            transform.LookAt(_worldPosition);
        }
    }

    public Vector3 CameraPosition(Vector3 WorldPos, Vector3 PlayersMiddlePos, double CameraDistanceFromMiddlePoint, double CameraHeight)
    {
        double alfa = Math.Atan(PlayersMiddlePos.x / PlayersMiddlePos.z) + Mathf.Deg2Rad * 180;
        double camX, camZ;

        camX = 0 * Math.Cos(alfa) - CameraDistanceFromMiddlePoint * Math.Sin(alfa);
        camZ = 0 * Math.Sin(alfa) - CameraDistanceFromMiddlePoint * Math.Cos(alfa);

        return new Vector3((float)(WorldPos.x + camX), (float)CameraHeight, (float)(WorldPos.z + camZ));

    }

    public void SwitchCamera(bool isTutorial)
    {
        if (isTutorial)
            TutorialCamera.SetActive(true);
        else
            TutorialCamera.SetActive(false);
    }

    private float _t = 0;
    
    private Vector3 _heavensGatePostion;
    private Vector3 _zeroPoint = new Vector3(45, 0, 74);
}
