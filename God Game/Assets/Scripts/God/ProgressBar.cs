using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {
    public RectTransform progressBarTransform;

	/// <summary>
    /// Accepts values from 0 to 1
    /// </summary>
    /// <param name="value"></param>
    public void SetProgress(float value)
    {
        progressBarTransform.anchorMax = new Vector2(0.5f + value / 2, 1);
        progressBarTransform.anchorMin = new Vector2((1 - value) / 2, 0);
    }
}
