using UnityEngine;
using System.Collections;
using System;

public class PortalColliderController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        _gameOver = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<RoundManager>().OnNewRoundStarted += GameOver_OnNewRoundStarted;
    }

    private void GameOver_OnNewRoundStarted(object sender, EventArgs e)
    {
        GetComponent<Collider>().enabled = true;
    }
    // Update is called once per frame
    void Update ()
    {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            _gameOver.PlayerColliding[collision.gameObject] = true;
            collision.gameObject.SetActive(false);
        }
    }

    private GameOver _gameOver; 
}
