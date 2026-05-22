using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : MonoBehaviour
{
    
    [SerializeField] Manager_Score _manager;
    private UniversalAudioClip _audioClip;

    [SerializeField] private int _coinValue = 1;
    [SerializeField] private int _scoreValue = 10;

    private void Awake()
    {
        _audioClip = GetComponent<UniversalAudioClip>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _manager.Take(_coinValue, _scoreValue);
            _audioClip.Play();
            Destroy(gameObject);
        }
    }
}

