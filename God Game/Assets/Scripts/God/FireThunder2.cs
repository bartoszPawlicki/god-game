using UnityEngine;
using System.Collections;

public class FireThunder2 : MonoBehaviour
{

    public GameObject thunderPrefab;

    private bool axisInUse;
    private bool thunderCreated;
    private float indicatorTimer;
    // Use this for initialization
    void Start()
    {
        axisInUse = false;
        thunderCreated = false;
        indicatorTimer = 1F;
    }

    void CreateThunder()
    {
        Instantiate(thunderPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 20, gameObject.transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!thunderCreated)
        {
            if (Input.GetAxisRaw("Confirm_Target") == 1)
            {
                if (!axisInUse)
                {

                    CreateThunder();
                    thunderCreated = true;
                    axisInUse = true;
                }

            }
        }
        
        if (Input.GetAxisRaw("Confirm_Target") == 1)
        {
            axisInUse = false;
        }
        if (thunderCreated)
        {
            indicatorTimer -= Time.deltaTime;
            if (indicatorTimer < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

