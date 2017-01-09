using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintScript : MonoBehaviour
{
    private PlayerController _player;

    public float SpeedMagnifier;
    public float SprintDuration;
    public float SprintCooldown;
    public GameObject SprintEffect;

    void Start ()
    {
        _player = GetComponentInParent<PlayerController>();

    }
	
    public IEnumerator StartSprint()
    {
        _player.Speed = _player.StartingSpeed * SpeedMagnifier;
        SprintEffect.SetActive(true);

        yield return new WaitForSeconds(SprintDuration);

        _player.Speed = _player.StartingSpeed;
        SprintEffect.SetActive(false);
    }

    
}
