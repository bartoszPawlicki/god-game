using UnityEngine;
using System.Collections;

public class FireThunder : MonoBehaviour {

    public GameObject thunderPrefab;

    private bool axisInUse;
    // Use this for initialization
    void Start ()
    {
        axisInUse = false;
	}

    void CreateThunder()
    {
        Instantiate(thunderPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 20, gameObject.transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetAxisRaw("Confirm_Target") == 1)
        {
            if (!axisInUse)
            {
                CreateThunder();
                gameObject.SetActive(false);
                axisInUse = true;
            }
            
        }
        if (Input.GetAxisRaw("Confirm_Target") == 1)
        {
            axisInUse = false;
        }
    }
}
