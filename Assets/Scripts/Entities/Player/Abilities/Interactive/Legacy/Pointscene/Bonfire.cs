using UltEvents;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private GameObject interactPanel;
    [SerializeField] private UltEvent saveEvent;

    private TriggerDetector triggerDetector;
    private Player player;
    private void Start()
    {
        triggerDetector = GetComponent<TriggerDetector>();
    }
    public void SaveGame(PlayerData playerData)
    {
        SaveSystem.Save(playerData);
        player.FullHealth();
        saveEvent.Invoke();
    }
    public void PlayerDetected(bool detected)
    {
        interactPanel.SetActive(detected);
        if (detected)
        {
            player = triggerDetector.LastElementDetected.GetComponent<Player>();
        }
        else
        {
            player = null;
        }
    }
}

