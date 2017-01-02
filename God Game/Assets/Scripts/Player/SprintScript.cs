using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintScript : MonoBehaviour
{
    private PlayerController _player;

    public float SpeedMagnifier;
    public float SprintDuration;
    public float SprintCooldown;

	void Start ()
    {
        _player = GetComponentInParent<PlayerController>();
	}
	
    public IEnumerator StartSprint()
    {
        _player.Speed = _player.StartingSpeed * SpeedMagnifier;

        yield return new WaitForSeconds(SprintDuration);

        _player.Speed = _player.StartingSpeed;
    }
}
