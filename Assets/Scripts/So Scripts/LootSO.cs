using UnityEngine;


[CreateAssetMenu(menuName = "Game/Loot")]
public class LootSO : ScriptableObject
{
    public string itemName;
    public int amount;
    public Sprite icon;
    public GameObject prefab;
}
