using UnityEngine;
using System.Collections;

public class FireThunder2 : MonoBehaviour
{

    public GameObject thunderPrefab;

    private GodController godController;

    private bool axisInUse;
    private bool thunderCreated;
    private float TimeBeforeThunderFallsTimer;
    private float TimeBeforeThunderFalls;

    // Use this for initialization
    void Start()
    {
        godController = GameObject.FindGameObjectWithTag("God").GetComponent<GodController>();
        axisInUse = false;
        thunderCreated = false;
        TimeBeforeThunderFallsTimer = 1F;
        TimeBeforeThunderFalls = 1F;
    }

    void CreateThunder()
    {
        Instantiate(thunderPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 20, gameObject.transform.position.z), Quaternion.identity);

        TimeBeforeThunderFallsTimer = TimeBeforeThunderFalls;
        thunderCreated = true;
        axisInUse = true;
        
        Debug.Log("Zmniejszam Speed");
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
                }

            }
        }
        
        if (Input.GetAxisRaw("Confirm_Target") == 1)
        {
            axisInUse = false;
        }

        if (thunderCreated)
        {
            godController.GodSpeed = 0F;
            TimeBeforeThunderFallsTimer -= Time.deltaTime;
            if (TimeBeforeThunderFallsTimer < 0)
            {
                godController.GodSpeed = godController.ThunderSpeed;
                Debug.Log("Zwiększam Speed");
                gameObject.SetActive(false);
                thunderCreated = false;
                TimeBeforeThunderFallsTimer = TimeBeforeThunderFalls;
            }      
        }
    }
}

