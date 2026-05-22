using UnityEngine;

public class PlayerContactable : MonoBehaviour, IContactable
{
    public void Contact()
    {
        RespawnManager.Instance.GetPlayerPosition();
    }
}
