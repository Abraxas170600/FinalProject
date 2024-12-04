using UnityEngine;

public class PowerObject : MonoBehaviour
{
    [SerializeField] private GameObject interactText;
    [SerializeField] private PlayerSkills.SkillType skillType;

    public void PlayerSkillActive(PlayerSkills playerSkills)
    {
        playerSkills.UnlockSkill(skillType);
    }
    public PlayerSkills.SkillType GetSkillType()
    {
        return skillType;
    }
    public void InteractText(bool isActive)
    {
        interactText.SetActive(isActive);
    }
}
