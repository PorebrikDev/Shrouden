using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public static WorldState Instance;

    [SerializeField] private StoneWizardQuest stone;
    [SerializeField] private WizardQuest wizard;
    [SerializeField] private QuestDoorTrigger doorTrigger;
    [SerializeField] private Key key;
    [SerializeField] private Chest chest;

    public bool questStoneCompled;
    public bool hasStone;
    public bool talkedWithWizard;
    public bool takeDoor;
    public bool doorOpen;
    public bool hasKey;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        stone.OnGiveStone += TriggerStone;
        wizard.OnWizardTrigger += TriggerWizard;
        doorTrigger.OnTakeDoor += TriggerDoor;
        key.OnKeyCollected += TriggerKey;
        chest.OnTriggerChest += TriggerChest;
    }

    private void OnDisable()
    {
        stone.OnGiveStone -= TriggerStone;
        wizard.OnWizardTrigger -= TriggerWizard;
        doorTrigger.OnTakeDoor -= TriggerDoor;
        key.OnKeyCollected -= TriggerKey;
        chest.OnTriggerChest -= TriggerChest;
    }

    private void TriggerChest()
    {
        if (questStoneCompled)
        {
            chest.OpenChast();
            key.Appear();
        }
    }

    private void TriggerStone()
    {
        hasStone = true;
        talkedWithWizard = false;
        QuestManagerWorld.Instance.EvaluateStone();
    }

    private void TriggerWizard()
    {
        talkedWithWizard = true;
        QuestManagerWorld.Instance.EvaluateStone();
        QuestManagerWorld.Instance.EvaluateKey();
    }

    private void TriggerDoor()
    {
        takeDoor = true;
        QuestManagerWorld.Instance.EvaluateKey();

        if (hasKey == true)
        {
            doorTrigger.OpenDoor();
            doorOpen = true;
        }
    }

    private void TriggerKey()
    {
        hasKey = true;
        QuestManagerWorld.Instance.EvaluateKey();
    }
}