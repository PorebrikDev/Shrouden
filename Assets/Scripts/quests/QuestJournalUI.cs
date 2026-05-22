using TMPro;
using UnityEngine;

public class QuestJournalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private QuestSingleSO quest;

    private void Start()
    {
        var manager = QuestManagerWorld.Instance;

        manager.OnQuestStarted += OnStart;
        manager.OnQuestChanged += OnChange;
    }
    private void OnDestroy()
    {
        var manager = QuestManagerWorld.Instance;

        if (manager == null) return;

        manager.OnQuestStarted -= OnStart;
        manager.OnQuestChanged -= OnChange;
    }

    private void OnStart(QuestSingleSO q)
    {
        if (q != quest) return;

        title.text = quest.quest_name;
        description.text = QuestManagerWorld.Instance.GetDestination(quest);
    }

    private void OnChange(QuestSingleSO q)
    {
        if (q != quest) return;

        var manager = QuestManagerWorld.Instance;

        if (manager.completed.Contains(quest))
        {
            description.text = quest.quest_completed_description;
            return;
        }

        description.text = manager.GetDestination(quest);
    }
}