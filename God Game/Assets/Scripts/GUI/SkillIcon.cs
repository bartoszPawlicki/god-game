using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour {

    public Image skillHighlight;

	void Start ()
    {
        SetSelected(false);
	}

    public void SetSelected(bool isSelected)
    {
        if (isSelected)
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);
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