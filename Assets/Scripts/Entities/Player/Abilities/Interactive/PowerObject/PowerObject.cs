using UltEvents;
using UnityEngine;

public class PowerObject : MonoBehaviour
{
    [SerializeField] private GameObject interactText;
    [SerializeField] private PlayerSkills.SkillType skillType;

    [TextArea(3, 10)]
    [SerializeField] private string tutorialSkillText;
    [SerializeField] private UltEvent<string> changeTutorialTextEvent;

    public void PlayerSkillActive(PlayerSkills playerSkills)
    {
        playerSkills.UnlockSkill(skillType);
        changeTutorialTextEvent.Invoke(tutorialSkillText);
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
