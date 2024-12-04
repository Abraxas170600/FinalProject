using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObject : MonoBehaviour
{
    [SerializeField] private PlayerSkills.SkillType skillType;

    public void PlayerSkillActive(PlayerSkills playerSkills)
    {
        playerSkills.UnlockSkill(skillType);
    }
    public PlayerSkills.SkillType GetSkillType()
    {
        return skillType;
    }
}
