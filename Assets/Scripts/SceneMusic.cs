using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _music;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(_music);
    }
}