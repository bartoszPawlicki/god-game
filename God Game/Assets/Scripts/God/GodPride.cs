using UnityEngine;
using System.Collections;
using System;

public class GodPride : MonoBehaviour
{
    public float godPride = 200;
    private float maxPride = 200;
    private ProgressBar progressBar;

    // Use this for initialization
    void Start()
    {
        _player1 = transform.parent.FindChild("Player1").gameObject;
        _playerControler1 = _player1.GetComponent<PlayerController>();
        _playerControler1.OnInflictDamage += GodPride_OnInflictDamage;

        _player2 = transform.parent.FindChild("Player2").gameObject;
        _playerControler2 = _player1.GetComponent<PlayerController>();
        _playerControler2.OnInflictDamage += GodPride_OnInflictDamage;

        _groundGodController = GetComponent<GroundGodController>();

        progressBar = GUIController.pBar;
        maxPride = godPride;
        progressBar.SetProgress(1);
    }
    // Update is called once per frame
    public void GodPride_OnInflictDamage(object sender, int dmg)
    {
        if(godPride > 0)
        {
            godPride -= dmg;

            progressBar.SetProgress(godPride / maxPride);

            _groundGodController.VibraionTimer = 0.5f;
        }
        else
        {
            Debug.Log("gameOver");
        }
    }
    public void ApplyDamage(float dmg)
    {
        godPride -= dmg;
        progressBar.SetProgress(godPride / maxPride);
        Debug.Log(godPride);
    }
    public void ApplySpecialAbility(int sa)
    {
        Debug.Log("pale sie1 xD");
        //zmienic zeby to nie bylo zahardkodowane
        if (sa == 1)
        {
            Debug.Log("pale sie2 xD");
            for (int i =0; i < 5; i++)
            {
               StartCoroutine(StartBurning());
            }
        }
    }
    public IEnumerator StartBurning()
    {
        // i to też
        Debug.Log("pale sie3 xD");
        godPride -= 2;
        progressBar.SetProgress(godPride / maxPride);
        yield return new WaitForSeconds(1);
    }


    private PlayerController _playerControler1;
    private GameObject _player1;
    private PlayerController _playerControler2;
    private GameObject _player2;
    private GroundGodController _groundGodController;
}