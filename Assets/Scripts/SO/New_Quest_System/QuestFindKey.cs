using UnityEngine;

public class QuestFindKey : MonoBehaviour
{
    public QuestSingleSO data;

    public void Evaluate()
    {
        var world = WorldState.Instance;
        var manager = QuestManagerWorld.Instance;

        if (manager.completed.Contains(data)) return;

        if (!manager.active.Contains(data))
        {
            manager.StartQuest(data);
        }

        if (!world.takeDoor && !world.talkedWithWizard)
        {
            manager.step[data] = 0;
            manager.Notify(data);
        }

        if (!world.takeDoor && world.talkedWithWizard)
        {
            manager.step[data] = 1;
            manager.Notify(data);
        }

        if (!world.hasKey && world.questStoneCompled)
        {
            manager.step[data] = 2;
            manager.Notify(data);
        }

        if (world.hasKey && world.questStoneCompled)
        {
            manager.step[data] = 3;
            manager.Notify(data);
        }

        if (world.doorOpen)
        {
            manager.StopQuest(data);
            manager.Notify(data);
        }
    }
}