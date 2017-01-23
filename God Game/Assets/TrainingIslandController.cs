using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingIslandController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        _gameController = GameObject.Find("Game").GetComponent<GameController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            _gameController.StartPosition = new Vector3(-3,1,-31);
    }

    private GameController _gameController;
}
