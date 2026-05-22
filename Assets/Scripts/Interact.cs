using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float range = 4f;
    [SerializeField] private LayerMask interactLayer;
    private IInteractable interactable;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Start()
    {
        GameInput.Instance.OnInteractStarted += CheckInteractable;
    }
    private void OnDestroy()
    {
            GameInput.Instance.OnInteractStarted -= CheckInteractable;
    }

    private void CheckInteractable(object sender, System.EventArgs e)
    {
        Vector2 vec = transform.localScale.x > 0 ? Vector2.right : Vector2.left; 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vec, range, interactLayer);
        Debug.DrawRay(transform.position, vec * range, Color.red, 1f);

        if (hit.collider != null)
        {
            interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null) { interactable.Interact(player); }
        }
        else
        {
            interactable = null;
        }
    }
}
