using UnityEngine;

[CreateAssetMenu(menuName ="Quests/New_Quest")]
public class QuestSingleSO : ScriptableObject
{

    public string quest_name;
    public string[] quest_description;
    public string quest_completed_description;
}
