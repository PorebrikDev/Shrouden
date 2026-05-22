using UnityEngine;

public class DamagebleObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IContactable>(out var contactable))
        {
            contactable.Contact();
        }
    }
}
