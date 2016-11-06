using UnityEngine;
using System.Collections;

public class GodController2 : MonoBehaviour {

    public float GodSpeed { get; set; }
    public float GodStartingSpeed { get; set; }

    public GameObject ThunderIndicatorPrefab;
    public GameObject ThunderIndicator { get; set; }
    public float ThunderIndicatorLifetimeTimer { get; set; }
    public float ThunderIndicatorLifeTime { get; set; }

    public GameObject ThunderPrefab;
    public GameObject Thunder { get; set; }
    private Rigidbody _thunderRigidbody;
    public float initialThunderXScale { get; set; }
    public float initialThunderZScale { get; set; }
    public float ThunderCooldownTimer { get; set; }
    public float ThunderCooldown { get; set; }
    public float ThunderSpeed { get; set; }
    public float ThunderStartingSpeed { get; set; }
    public float ThunderLifeTime { get; set; }
    public float ThunderLifeTimeTimer { get; set; }
    public float ThunderTimeBeforeHit{ get; set; }
    public float ThunderTimeBeforeHitTimer { get; set; }
    
    private float SlowDuration = 2F;
    private float SlowPower = 0.3F;

    public bool ThunderCreated { get; set; }
    public bool ThunderChosen { get; set; }

    public bool ThunderFallen { get; set; }

    private bool axisInUse;


    // Use this for initialization
    void Start ()
    {
        GodStartingSpeed = 15F;
        GodSpeed = GodStartingSpeed;

        ThunderIndicator = (GameObject)Instantiate(ThunderIndicatorPrefab, new Vector3(gameObject.transform.position.x, 0.01F, gameObject.transform.position.z), Quaternion.identity);
        ThunderIndicator.transform.SetParent(gameObject.transform);
        ThunderIndicator.SetActive(false);

        ThunderIndicatorLifeTime = 5F;
        ThunderIndicatorLifetimeTimer = 1F;

        Thunder = (GameObject)Instantiate(ThunderPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 20, gameObject.transform.position.z), Quaternion.identity);
        Thunder.transform.SetParent(gameObject.transform);
        _thunderRigidbody = Thunder.GetComponent<Rigidbody>();

        ThunderCooldownTimer = -1F;
        ThunderCooldown = 5F;
        ThunderStartingSpeed = 8F;
        ThunderLifeTime = 4F;
        ThunderSpeed = ThunderStartingSpeed;
        ThunderTimeBeforeHit = 1F;
        initialThunderXScale = Thunder.transform.localScale.x;
        initialThunderZScale = Thunder.transform.localScale.z;
        Thunder.SetActive(false);

        ThunderCreated = true;
        ThunderChosen = false;
        axisInUse = false;
    }
    

    void ChooseThunderSKill()
    {
        ThunderCooldownTimer -= Time.deltaTime;
        ThunderIndicatorLifetimeTimer -= Time.deltaTime;

        if (Input.GetAxis("Fire_Thunder") == 1)
        {
            if(ThunderCooldownTimer < 0)
            {
                ThunderIndicator.SetActive(true);
                ThunderCooldownTimer = ThunderCooldown;
                ThunderIndicatorLifetimeTimer = ThunderIndicatorLifeTime;

                ThunderChosen = true;
            } 
        }

        if (ThunderIndicatorLifetimeTimer < 0)
        {
            ThunderIndicator.SetActive(false);
            ThunderChosen = false;
        }
    }

    void AcceptThunderSkill()
    {
        if(ThunderChosen)
        {
            if (Input.GetAxisRaw("Confirm_Target") == 1)
            {
                if (!axisInUse)
                {
                    CreateThunder();
                }

            }
            if (Input.GetAxisRaw("Confirm_Target") == 1)
            {
                axisInUse = false;
            }
        }
    }

    void CreateThunder()
    {
        Thunder.SetActive(true);
        axisInUse = true;
        ThunderIndicator.SetActive(false);
        ThunderChosen = false;
        ThunderCreated = true;
        ThunderLifeTimeTimer = ThunderLifeTime;
        ThunderTimeBeforeHitTimer = ThunderTimeBeforeHit;
        Thunder.transform.Translate(new Vector3(0, gameObject.transform.position.y + 20, 0));
        GodSpeed = 0;

    }

    void ThunderControl()
    {
        ThunderLifeTimeTimer -= Time.deltaTime;
        ThunderTimeBeforeHitTimer -= Time.deltaTime;
        
        //thunder's diminishing
        if (ThunderLifeTimeTimer > 0) Thunder.transform.localScale = new Vector3(initialThunderXScale * ThunderLifeTimeTimer / ThunderLifeTime, 20, initialThunderZScale * ThunderLifeTimeTimer / ThunderLifeTime);

        if (ThunderLifeTimeTimer >= 0 && ThunderTimeBeforeHitTimer <0)
        {
            if (Thunder.transform.position.y > Thunder.transform.localScale.y / 2 + 1) Thunder.transform.Translate(new Vector3(0, -100F * Time.deltaTime, 0), Space.Self);    
        }
        
        if (ThunderTimeBeforeHitTimer < 0)
        {
            GodSpeed = GodStartingSpeed;             
        }

        if (ThunderLifeTimeTimer < 0)
        {
            Thunder.SetActive(false);
        }
    }

    
    // Update is called once per frame
    void Update ()
    {
        
    }

    void FixedUpdate()
    {
        //god movement
        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");
        gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * GodSpeed * Time.deltaTime);

        ChooseThunderSKill();
        AcceptThunderSkill();
        ThunderControl();
        

    }
}
