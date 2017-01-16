using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour {
    public int speed;
	// Use this for initialization
	void Start () {
        GetComponent<Animator>().SetInteger("Speed", speed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
