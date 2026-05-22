using System;
using UnityEngine;

public class StoneWizardQuest : MonoBehaviour
{
    private BoxCollider2D box;
    public event Action OnGiveStone;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            OnGiveStone?.Invoke();
            Destroy(gameObject);
        }
    }
}
