using UnityEngine;
using UnityEngine.UI;

public class GodIndicator : MonoBehaviour {
    public GameObject god;
    public Image indicatorImage;

    private Camera mainCamera;
    private RectTransform rectTransform;
	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 godScreenPosition = mainCamera.WorldToViewportPoint(god.transform.position);
        if (godScreenPosition.x >= 0 && godScreenPosition.x <= 1 && godScreenPosition.y >= 0 && godScreenPosition.y <= 1)
        {
            indicatorImage.enabled = false;
            return;
        }
        indicatorImage.enabled = true;
        Vector2 newPosition = new Vector2(godScreenPosition.x - 0.5f, godScreenPosition.y - 0.5f) * 2;
        float maximum = Mathf.Max(Mathf.Abs(newPosition.x), Mathf.Abs(newPosition.y));
        newPosition = (newPosition / (maximum * 2)) + new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = newPosition;
        rectTransform.anchorMin = newPosition;
    }
}
