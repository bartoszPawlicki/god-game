using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour {

    // Use this for initialization
    void Start () {
        _respawnTutorialManager = GameObject.Find("Game").GetComponent<RespawnTutorialManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _respawnTutorialManager.StartRespawn(collision.gameObject);
        }
    }

    private RespawnTutorialManager _respawnTutorialManager;
}
