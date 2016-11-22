using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{
    
    // Use this for initialization
    void Start ()
    {
        _player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        _player2 = GameObject.Find("Player2").GetComponent<PlayerController>();

        _player1Skills = GameObject.Find("Skills Player1").GetComponent<SkillsController>();
        _player2Skills = GameObject.Find("Skills Player2").GetComponent<SkillsController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        _player1Skills.ThrowCooldown = _player1.ThrowCooldownValue;
        _player1Skills.RopeCooldown = _player1.RopeCooldownValue;
        _player2Skills.ThrowCooldown = _player2.ThrowCooldownValue;
        _player2Skills.RopeCooldown = _player2.RopeCooldownValue;
    }

    private PlayerController _player1;
    private PlayerController _player2;

    private SkillsController _player1Skills;
    private SkillsController _player2Skills;
}
