using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public enum SkillType
    {
        Dash,
        WallJump,
        FastFall,
        Bash,
        Attack
    }

    private List<SkillType> unlockedSkillTypeList;
    public PlayerSkills()
    {
        unlockedSkillTypeList = new();
    }
    public void UnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType))
            unlockedSkillTypeList.Add(skillType);
    }
    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);
    }
    public List<SkillType> UnlockedSkillTypeList()
    {
        return unlockedSkillTypeList;
    }
}
