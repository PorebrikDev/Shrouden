using UnityEngine;
using System;

public class WizardQuest : MonoBehaviour, ITriggeredQuest
{
    public event Action OnWizardTrigger;
    public void TriggerQuest()
    { 
    OnWizardTrigger?.Invoke();
    }
}
