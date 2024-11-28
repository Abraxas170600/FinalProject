using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TriggerDetector : MonoBehaviour
{
    [System.Serializable]
    public class TagAction
    {
        public string tag;
        public UnityEvent<bool> enterAction;
        public UnityEvent<bool> stayAction;
        public UnityEvent<bool> exitAction;
    }
    [SerializeField] private TagAction[] tagActions;
    private GameObject lastElementDetected;
    public GameObject LastElementDetected { get => lastElementDetected; set => lastElementDetected = value; }

    private void OnTriggerEnter2D(Collider2D collision) => ExecutionAction(collision.gameObject, (tagAction) => tagAction.enterAction.Invoke(true));
    private void OnTriggerStay2D(Collider2D collision) => ExecutionAction(collision.gameObject, (tagAction) => tagAction.stayAction.Invoke(true));
    private void OnTriggerExit2D(Collider2D collision) => ExecutionAction(collision.gameObject, (tagAction) => tagAction.exitAction.Invoke(false));

    private void ExecutionAction(GameObject tagObject, UnityAction<TagAction> action)
    {
        foreach (TagAction tagAction in tagActions)
        {
            if (tagObject.CompareTag(tagAction.tag))
            {
                LastElementDetected = tagObject;
                action.Invoke(tagAction);
            }
        }
    }
}
