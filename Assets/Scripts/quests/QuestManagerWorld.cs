using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerWorld : MonoBehaviour
{
    public static QuestManagerWorld Instance;

    [SerializeField] private QuestFindStone findStone;
    [SerializeField] private QuestFindKey findKey;

    public event Action<QuestSingleSO> OnQuestStarted;
    public event Action<QuestSingleSO> OnQuestChanged;
    public event Action OnUpdate;

    public Dictionary<QuestSingleSO, int> step = new Dictionary<QuestSingleSO, int>();
    public HashSet<QuestSingleSO> active = new HashSet<QuestSingleSO>();
    public HashSet<QuestSingleSO> completed = new HashSet<QuestSingleSO>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Notify(QuestSingleSO quest)
    {
        OnQuestChanged?.Invoke(quest);
    }

    public void NotifyQuestChanged(QuestSingleSO quest)
    {
        OnQuestChanged?.Invoke(quest);
    }

    public void StartQuest(QuestSingleSO quest)
    {
        if (completed.Contains(quest) || active.Contains(quest)) { return; }

        active.Add(quest);
        step[quest] = 0;
        OnUpdate?.Invoke();
        OnQuestStarted?.Invoke(quest);
    }

    public void ContinueQuest(QuestSingleSO quest)
    {
        if (!active.Contains(quest)) return;

        step[quest]++;
        OnQuestChanged?.Invoke(quest);
        OnUpdate?.Invoke();
    }

    public void StopQuest(QuestSingleSO quest)
    {
        active.Remove(quest);
        completed.Add(quest);
        OnUpdate?.Invoke();
        Debug.Log($"квест {quest} завершен");
        OnQuestChanged?.Invoke(quest);
    }

    public string GetDestination(QuestSingleSO quest)
    {
        if (completed.Contains(quest))
            return quest.quest_completed_description;

        return quest.quest_description[step[quest]];
    }

    public void EvaluateStone()
    {
        findStone.Evaluate();
    }

    public void EvaluateKey()
    {
        findKey.Evaluate();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
