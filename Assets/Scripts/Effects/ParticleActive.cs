using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ParticleActions
{
    public string particleActionName;
    public UnityEvent particleAction;
}
public class ParticleActive : MonoBehaviour
{
    public ParticleActions[] particleActions;
    public void ActiveParticle(ParticleSystem particleObject) => particleObject.Play();
    public void ActiveParticleAction(int index) => particleActions[index].particleAction.Invoke();
}
