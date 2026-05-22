using UnityEngine;
using System;

public class QuestDoorTrigger : MonoBehaviour, ITriggeredQuest
{
    public event Action OnTakeDoor;
    private Animator animator;
    private BoxCollider2D box2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        box2D = GetComponent<BoxCollider2D>();
    }
    public void TriggerQuest()
    {
        OnTakeDoor?.Invoke();

    }

    public void OpenDoor()
    {
        animator.SetTrigger("UseKey");
        box2D.enabled = false;
        WorldState.Instance.doorOpen = true;
        QuestManagerWorld.Instance.EvaluateKey();

    }
}