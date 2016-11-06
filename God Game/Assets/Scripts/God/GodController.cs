using UnityEngine;
using System.Collections;

public class GodController : MonoBehaviour
{

    public float GodSpeed;
    public float GodStartingSpeed;

    public GameObject ThunderIndicatorPrefab;

    public GameObject ThunderIndicator { get; set; }
    public float ThunderCooldownTimer = -1F;
    public float ThunderCooldown = 5F;
    public float ThunderSpeed;
    public float ThunderStartingSpeed;

    public float ThunderIndicatorLifetimeTimer { get; set; }
    public float ThunderIndicatorLifeTime { get; set; }
    void Start()
    {
        ThunderIndicator = (GameObject)Instantiate(ThunderIndicatorPrefab, new Vector3(gameObject.transform.position.x, 0.01F, gameObject.transform.position.z), Quaternion.identity);
        ThunderIndicator.transform.SetParent(gameObject.transform);
        ThunderIndicator.SetActive(false);

        GodStartingSpeed = 15;
        GodSpeed = GodStartingSpeed;

        ThunderIndicatorLifeTime = 5F;
        ThunderStartingSpeed = 8f;
        ThunderSpeed = ThunderStartingSpeed;
    }
    void UseThunderSkill()
    {
        ThunderIndicator.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //god movement
        float moveHorizontal = Input.GetAxis("Horizontal_God");
        float moveVertical = Input.GetAxis("Vertical_God");
        gameObject.transform.Translate(new Vector3(moveHorizontal, 0, moveVertical) * GodSpeed * Time.deltaTime);

        ThunderCooldownTimer -= Time.deltaTime;
        if (Input.GetAxis("Fire_Thunder") == 1)
        {
            if (ThunderCooldownTimer < 0)
            {
                UseThunderSkill();
                ThunderCooldownTimer = ThunderCooldown;
            }
        }
         
    }
    
}
