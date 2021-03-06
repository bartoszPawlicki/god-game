﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintScript : MonoBehaviour
{
    public AudioSource SprintAudioSource;

    private PlayerController _player;

    public float SpeedMagnifier;
    public float SprintDuration;
    public float SprintCooldown;
    public GameObject SprintEffect;

    void Start ()
    {
        _player = GetComponentInParent<PlayerController>();
        SprintEffect.SetActive(false);
    }

    public void EndSprint()
    {
        _player.Speed = _player.StartingSpeed;
        SprintAudioSource.Stop();
        SprintEffect.SetActive(false);
        _player._isRunning = false;
    }
	
    public IEnumerator StartSprint()
    {
        _player.Speed = _player.StartingSpeed * SpeedMagnifier;
        SprintEffect.SetActive(true);
        SprintAudioSource.Play();
        _player._isRunning = true;

        yield return new WaitForSeconds(SprintDuration);

        EndSprint();
    }

    
    
}
