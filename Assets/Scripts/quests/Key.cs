using UnityEngine;
using System;
using DG.Tweening;

public class Key : MonoBehaviour
{
    public event Action OnKeyCollected;

    private BoxCollider2D _box;
    private Tween _appearTween;

    private void Awake()
    {
        _box = GetComponent<BoxCollider2D>();
        _box.enabled = false;
        gameObject.SetActive(false);
    }

    public void Appear()
    { 
        gameObject.SetActive(true);
        _appearTween = transform.DOMoveY(0.6f, 1f).SetEase(Ease.InOutBack).OnComplete(()=>_box.enabled = true);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true)
        {
            OnKeyCollected?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _appearTween?.Kill();
        transform?.DOKill();
    }
}

