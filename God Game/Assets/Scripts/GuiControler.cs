using UnityEngine;
using System.Collections;

public class GuiControler : MonoBehaviour {
    public ProgressBar progressBar;
    public static ProgressBar pBar;
	// Use this for initialization
	void Awake()
    {
        pBar = progressBar;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
