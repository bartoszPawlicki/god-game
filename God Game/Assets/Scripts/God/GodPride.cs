using UnityEngine;
using System.Collections;
using System;

public class GodPride : MonoBehaviour
{

    public int godPride = 200;
    // Use this for initialization
    void Start()
    {
        _player1 = transform.parent.FindChild("Player1").gameObject;
        _playerControler1 = _player1.GetComponent<PlayerController>();
        _playerControler1.OnInflictDamage += GodPride_OnInflictDamage;

        _player2 = transform.parent.FindChild("Player2").gameObject;
        _playerControler2 = _player1.GetComponent<PlayerController>();
        _playerControler2.OnInflictDamage += GodPride_OnInflictDamage;
    }
    // Update is called once per frame
    public void GodPride_OnInflictDamage(object sender, int dmg)
    {
        if(godPride > 0)
        {
            godPride -= dmg;
            Debug.Log("god pride = " + godPride);
        }
        else
        {
            Debug.Log("gameOver");
        }
    }
    private PlayerController _playerControler1;
    private GameObject _player1;
    private PlayerController _playerControler2;
    private GameObject _player2;
}