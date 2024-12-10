using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector2 position;
    public List<PlayerSkills.SkillType> unlockPowers = new();
    public PlayerData()
    {
        position = new Vector2(-7, -3);
        unlockPowers = new();
    }
    public PlayerData(Vector2 position, List<PlayerSkills.SkillType> unlockPowers)
    {
        this.position = position;
        this.unlockPowers = unlockPowers;
    }
}
