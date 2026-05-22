
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Quest")]
public class QuestData: ScriptableObject

{
    public string id;
    public string title;

    [TextArea(2,5)]
    public string[] descriptions;

    
}
