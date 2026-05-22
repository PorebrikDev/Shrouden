using UnityEngine;

public class QuestFindStone : MonoBehaviour
{
    public QuestSingleSO data;

    public void Evaluate()
    {
        var world = WorldState.Instance;
        var manager = QuestManagerWorld.Instance;
        if (manager.completed.Contains(data)) return;

        if (world.talkedWithWizard && !world.hasStone && !manager.active.Contains(data))
        {
            manager.StartQuest(data);
            manager.step[data] = 0;
            manager.Notify(data);
            Debug.Log($"камень {manager.step[data]}");
            return;
        }

        if (world.hasStone && !world.talkedWithWizard)
        {
            manager.StartQuest(data);
            manager.step[data] = 1;
            manager.Notify(data);
            Debug.Log($"камень {manager.step[data]}");
            return;
        }

        if (world.hasStone && world.talkedWithWizard)
        {
            manager.StopQuest(data);
            world.questStoneCompled = true;
            manager.Notify(data);
            Debug.Log($"камень {manager.step[data]}");
            return;
        }
    }
}
