using UnityEngine;
using System.Collections;

public class SkillsController : MonoBehaviour
{
    public float ThrowCooldown
    {
        set
        {
            if (value >= 0 && value <= 100)
                _throwSkill.SetCooldownPercent(value);
            else if (value > 100)
                _throwSkill.SetCooldownPercent(100);
            else
                _throwSkill.SetCooldownPercent(0);
        }
    }
    public float RopeCooldown
    {
        set
        {
            if (value >= 0 && value <= 100)
                _ropeSkill.SetCooldownPercent(value);
            else if (value > 100)
                _ropeSkill.SetCooldownPercent(100);
            else
                _ropeSkill.SetCooldownPercent(0);
        }
    }
    public float SlingshotCooldown
    {
        set
        {
            if (value >= 0 && value <= 100)
                _slingshotSkill.SetCooldownPercent(value);
            else if (value > 100)
                _slingshotSkill.SetCooldownPercent(100);
            else
                _slingshotSkill.SetCooldownPercent(0);
        }
    }
    void Start ()
    {
        _throwSkill = transform.FindChild("Throw").GetComponent<SkillIcon>();
        _ropeSkill = transform.FindChild("Rope").GetComponent<SkillIcon>();
        _slingshotSkill = transform.FindChild("Slingshot").GetComponent<SkillIcon>();
    }

    private SkillIcon _throwSkill;
    private SkillIcon _ropeSkill;
    private SkillIcon _slingshotSkill;
}
