using UnityEngine;

public class Respawnposition : MonoBehaviour
{
    [SerializeField] private string _locationName;
    public string LocationName => _locationName;
    private Vector3 _transformPos;
    private UniversalAudioClip _clip;
    
    private string _playerName = "Player";


    private bool _isActiveited = false;

    private void Awake()
    {
        _transformPos = transform.position;
        _clip = GetComponent<UniversalAudioClip>();
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag(_playerName))
        {
            RespawnManager.Instance.SetRespawnPlase(_locationName, _transformPos, _isActiveited);
            _clip.Play();
            _isActiveited = true;
        }
    }
}
