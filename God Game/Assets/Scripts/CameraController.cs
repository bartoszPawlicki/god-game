using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    Vector3 worldPosition;
    Vector3 offset;
	// Use this for initialization
	void Start ()
    {
        offset = new Vector3(offsetX, offsetY, offsetZ);
    }
	
	// Update is called once per frame
	void Update ()
    {
        worldPosition = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);
        transform.position = worldPosition + offset;
        transform.LookAt(worldPosition);
    }
}
