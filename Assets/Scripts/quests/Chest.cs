using UnityEngine;
using System;


public class Chest : MonoBehaviour, ITriggeredQuest
{
    public event Action OnTriggerChest;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerQuest()
    {
        OnTriggerChest?.Invoke();
    }
    public void OpenChast()
    {
        animator.SetTrigger("ChestOpen");
    }
}
