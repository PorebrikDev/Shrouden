using UnityEngine;

public class PlayerQuestTriggerFind : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private LayerMask questTriggerLayer;
    private float range = 0.7f;

    private void Start()
    {
        GameInput.Instance.OnInteractStarted += TryQuestInteract;
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnInteractStarted -= TryQuestInteract;
    }

    public void TryQuestInteract(object sender, System.EventArgs e)
    {
        Vector2 origin =  transform.position;
        //Vector2 dir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 dir =  Player.Instance.FacingDirection;
        Debug.DrawRay(origin, dir.normalized * range, Color.red, 1f);
        RaycastHit2D hit = Physics2D.Raycast(origin, dir.normalized, range, questTriggerLayer);

        if (hit.collider == null)  return;

        Debug.Log(hit.collider.name);
        AudioManager.Instance.PlaySFX(clip);
        var trigger = hit.collider.GetComponent<ITriggeredQuest>();
        trigger?.TriggerQuest();
    }
}
