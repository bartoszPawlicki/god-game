using UnityEngine;

public class UV_Scroll : MonoBehaviour
{
    private Material mat;
    private float scroll;
    public float speed;
	// Use this for initialization
	void Start ()
    {
        mat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        scroll = (scroll - Time.deltaTime * speed) % 1.0f;
        mat.mainTextureOffset = new Vector2(scroll, 0);
	}
}
