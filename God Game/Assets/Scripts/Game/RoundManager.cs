using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {


    private GameObject[] _players;

    void Start ()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        short i = 0;
        foreach (var player in _players)
        {
            player.transform.position = new Vector3(-5 + i, 1, 0);
            i += 10;
        }
    }
	
	
	void Update ()
    {
	
	}
}
