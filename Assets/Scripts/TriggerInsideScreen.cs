using System;
using UnityEngine;

public class TriggerInsideScreen : MonoBehaviour
{
    private string _playerTag = "Player";

    public event Action OnPlayerIsDropped;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(_playerTag))
        {
            OnPlayerIsDropped?.Invoke();
        }
    }
    private void Start()
    {
        RespawnManager.Instance?.RegisterTrigger(this);
    }
}
