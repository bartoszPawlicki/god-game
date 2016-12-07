using UnityEngine;
using System.Collections;

public class CatapultController : MonoBehaviour
{
    public float SlowPower;
    public float SlowDuration;
    public float CatapultVerticalStrength;
    public float CatapultHorizontalStrength;
    public Vector3 CatapultDirection;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController slow = collider.GetComponent<PlayerController>();
            slow.ApplySlow(SlowPower, SlowDuration);

            collider.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3 (0, CatapultDirection.y, 0).normalized * CatapultVerticalStrength);
            collider.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(CatapultDirection.x, 0, CatapultDirection.z).normalized * CatapultHorizontalStrength);
        }
    }
}
