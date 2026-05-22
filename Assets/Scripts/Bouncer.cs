using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 12f;
    [SerializeField] private bool _onlyPlayer = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_onlyPlayer && !collision.gameObject.CompareTag("Player"))
            return;

        Rigidbody2D rb = collision.rigidbody;
        if (rb == null) return;


        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;


        rb.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
    }
}