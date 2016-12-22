using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleController : MonoBehaviour {

    public float _startingHp;
    public GameObject God;
    public GameObject GroundGod;


	void Start ()
    {
        HP = _startingHp;
        _godController = God.GetComponent<GodController>();
        _groundGodController = GroundGod.GetComponent<GroundGodController>();
	}
	
	public void ApplyDamage(float damage)
    {
        HP -= damage;
    }

	void Update ()
    {
		if (HP <= 0)
        {
            StartCoroutine(TempleDestroyed());
        }
	}

    IEnumerator TempleDestroyed()
    {
        Debug.Log("Umieram");
        _godController.silenceGodSkills();
        yield return new WaitForSeconds(3);
        _godController.enabled = false;
        God.SetActive(false);
        gameObject.SetActive(false);
        GroundGod.transform.position = new Vector3(transform.position.x, 10, transform.position.z);
    }

    private float HP;
    private GodController _godController;
    private GroundGodController _groundGodController;
}
