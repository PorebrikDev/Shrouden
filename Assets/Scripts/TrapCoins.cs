using UnityEngine;

public class TrapCoins : MonoBehaviour
{
    private int value = 1;
    [SerializeField] private Catch _catch;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _catch.AddCoin(value);
        }

    }
}