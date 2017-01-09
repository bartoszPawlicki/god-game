using UnityEngine;
using System.Collections;

public class CatapultController : MonoBehaviour
{
    public float SlowPower;
    public float SlowDuration;
    public float CatapultVerticalStrength;
    public float CatapultHorizontalStrength;
    public Vector3 CatapultDirection;
    public float PlankRotationInterval;
    public int TotalRotationIntervals;

    void Start()
    {
        _catapultPlank = transform.FindChild("catapult_plank").gameObject;
        _catapultSoundEffect = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _catapultSoundEffect.Play();

            PlayerController slow = collider.GetComponent<PlayerController>();
            slow.ApplySlow(SlowPower, SlowDuration);

            collider.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3 (0, CatapultDirection.y, 0).normalized * CatapultVerticalStrength);
            collider.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(CatapultDirection.x, 0, CatapultDirection.z).normalized * CatapultHorizontalStrength);

            StartCoroutine(RotatePlank());
        }
    }

    IEnumerator RotatePlank()
    {
        for (int i=0; i< TotalRotationIntervals; i++)
        {
            _catapultPlank.transform.Rotate(new Vector3(80/ TotalRotationIntervals, 0, 0));
            yield return new WaitForSeconds(PlankRotationInterval/5);
        }

        for (int i = 0; i < TotalRotationIntervals; i++)
        {
            _catapultPlank.transform.Rotate(new Vector3(-80 / TotalRotationIntervals, 0, 0));
            yield return new WaitForSeconds(PlankRotationInterval/2);
        }

    }

    private GameObject _catapultPlank;
    private AudioSource _catapultSoundEffect;
}
