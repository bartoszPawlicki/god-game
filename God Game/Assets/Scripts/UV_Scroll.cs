using UnityEngine;

public class UV_Scroll : MonoBehaviour
{
    private Material mat;
    private float scroll;
	// Use this for initialization
	void Start ()
    {
        mat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        scroll = (scroll - Time.deltaTime * 0.4f) % 1.0f;
        mat.mainTextureOffset = new Vector2(scroll, 0);
	}
}
