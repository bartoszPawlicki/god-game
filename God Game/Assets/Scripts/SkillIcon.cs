using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour {

    public Image skillHighlight;
    public Image skillSelectionOverlay;

	void Start ()
    {
        SetSelected(false);
	}

    public void SetSelected(bool isSelected)
    {
        skillSelectionOverlay.enabled = isSelected;
    }

    public void SetCooldownPercent(float percent)
    {
        skillHighlight.fillAmount = percent / 100.0f;
    }

    /// <summary>
    /// Accepts arguments from 0 to 1
    /// </summary>
    /// <param name="value"></param>
    public void SetCooldownValue(float value)
    {
        skillHighlight.fillAmount = value;
    }
}