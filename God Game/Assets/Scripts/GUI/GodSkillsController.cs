using UnityEngine;
using System.Collections;

public class GodSkillsController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        _thunderSkill = transform.FindChild("Thunder").GetComponent<SkillIcon>();
        _waterGeyserSkill = transform.FindChild("WaterGeyser").GetComponent<SkillIcon>();
        _globalWindSkill = transform.FindChild("GlobalWind").GetComponent<SkillIcon>();
    }
    public float ThunderCooldown
    {
        set
        {
            if (value >= 0 && value <= 100)
                _thunderSkill.SetCooldownPercent(value);
            else if (value > 100)
                _thunderSkill.SetCooldownPercent(100);
            else
                _thunderSkill.SetCooldownPercent(0);
        }
    }

    public float WaterGeyserCooldown
    {
        set
        {
            if (value >= 0 && value <= 100)
                _waterGeyserSkill.SetCooldownPercent(value);
            else if (value > 100)
                _waterGeyserSkill.SetCooldownPercent(100);
            else
                _waterGeyserSkill.SetCooldownPercent(0);
        }
    }

    public float GlobalWindCooldown
    {
        set
        {
            if (value >= 0 && value <= 100)
                _globalWindSkill.SetCooldownPercent(value);
            else if (value > 100)
                _globalWindSkill.SetCooldownPercent(100);
            else
                _globalWindSkill.SetCooldownPercent(0);
        }
    }
    // Update is called once per frame
    void Update ()
    {
	
	}

    public SkillIcon ThunderSkill
    { get { return _thunderSkill; } }
    public SkillIcon WaterGeyserSkill
    { get { return _waterGeyserSkill; } }
    public SkillIcon GlobalWindSkill
    { get { return _globalWindSkill; } }

    private SkillIcon _thunderSkill;
    private SkillIcon _waterGeyserSkill;
    private SkillIcon _globalWindSkill;
}
